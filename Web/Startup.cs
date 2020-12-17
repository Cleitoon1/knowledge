using System;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Data;
using DI;
using Domain;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Shared.Utils;
using VueCliMiddleware;
using Web.Security;

namespace Web {
    public class Startup {

        public IConfiguration Configuration { get; }
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            string connectionString = Configuration.GetConnectionString ("SQLDataBase");
            var tokenConfigurations = Configuration.GetSection (nameof (TokenConfigurations));
            services.AddSingleton<TokenConfigurations> (tokenConfigurations.Get<TokenConfigurations> ());
            services.AddSingleton<SmtpConfig> (Configuration.GetSection (nameof (SmtpConfig)).Get<SmtpConfig> ());
            services.AddAuthentication (x => {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer (x => {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes (tokenConfigurations["PrivateKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

                services.AddScoped<LoggedUserDTO> (provider => {
                    LoggedUserDTO data = null;
                    HttpContext ctx = provider.GetService<IHttpContextAccessor> ()?.HttpContext;
                    if (ctx != null && ctx.User != null) {
                        data = new LoggedUserDTO (
                            long.Parse(ctx.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? "0"),
                            ctx.User.FindFirst(x => x.Type == ClaimTypes.Name)?.Value,
                            ctx.User.FindFirst(x => x.Type == ClaimTypes.Email)?.Value,
                            ctx.User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value
                        );
                    }
                    return data;
                });

                services.AddAuthorization (x => {

                    x.AddPolicy ("Administrator", a => { a.RequireRole ("Administrator"); });
                    x.AddPolicy ("User", a => { a.RequireRole ("User"); });
                });

                services.AddDbContext<KnowledgeContext> (options => {
                    options.UseSqlServer (connectionString, x => {
                        x.MigrationsAssembly ("Data");
                        x.CommandTimeout ((int) TimeSpan.FromMinutes (10).TotalSeconds);
                    });
                    // options.EnableSensitiveDataLogging(true);
                });

                services.AddMediatR (typeof (Startup).GetTypeInfo ().Assembly, typeof (Application).GetTypeInfo ().Assembly);

                services.AddControllers (); services.AddMvc ()
                .AddNewtonsoftJson (options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                ); services.AddHttpContextAccessor (); services.AddScoped<KnowledgeContext, KnowledgeContext> (); ModuleIOC.RegisterServices (services); services.AddSwaggerGen (c => {
                    c.SwaggerDoc ("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Knowledge", Version = "v1" });
                });

                services.AddSpaStaticFiles (configuration => {
                    configuration.RootPath = "clientApp";
                });
            }

            public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
                if (env.IsDevelopment ()) {
                    app.UseDeveloperExceptionPage ();
                }

                app.UseSwagger ();
                app.UseSwaggerUI (c => {
                    c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Knowledge API - V1");
                });

                app.UseCors (x => {
                    x.AllowAnyHeader ();
                    x.AllowAnyMethod ();
                    x.AllowAnyOrigin ();
                });

                app.UseRouting ();
                app.UseSpaStaticFiles ();
                app.UseCookiePolicy ();
                app.UseAuthentication ();
                app.UseAuthorization ();
                app.UseResponseCaching ();

                app.UseEndpoints (endpoints => {
                    endpoints.MapControllerRoute (
                        name: "default",
                        pattern: "{controller}/{action=Index}/{id?}"
                    );
                    endpoints.MapControllers ();
                });

                app.UseSpa (spa => {
                    spa.Options.StartupTimeout = new TimeSpan (days: 0, hours: 0, minutes: 1, seconds: 30);
                    if (env.IsDevelopment ())
                        spa.Options.SourcePath = "clientApp";
                    else
                        spa.Options.SourcePath = "dist";

                    if (env.IsDevelopment ())
                        spa.UseVueCli (npmScript: "serve");
                });
            }
        }
    }