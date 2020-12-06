using System;
namespace Web.ViewModels {
    public class UserDataVM {
        public UserDataVM (DateTime created, DateTime expiration, string token,
            string message, string name, string lastName, string profile, bool authenticated = true)
        {
            this.Authenticated = authenticated;
            this.Created = created.ToString("yyyy-MM-dd HH:mm:ss");
            this.Expiration = expiration.ToString("yyyy-MM-dd HH:mm:ss");
            this.Token = token;
            this.Message = message;
            this.Name = name;
            this.LastName = lastName;
            this.Profile = profile;
        }

        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Profile { get; set; }
    }
}