using Domain.Extensions;
using Domain.Models.Base;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class User : BaseEntity
    {
        public User(string name, string lastName, string mail, string password, string mobilePhone,
            long profileId, DateTime? birthDate = null) : base()
        {
            Name = name;
            LastName = lastName;
            FullName = $"{Name} {LastName}";
            Mail = mail;
            MobilePhone = mobilePhone;
            BirthDate = birthDate;
            ProfileId = profileId;
            Validate();
            if (!string.IsNullOrEmpty(password) && !string.IsNullOrWhiteSpace(password))
                Password = password.ConvertToMD5();
        }

        public User(string name, string lastName, string mail, string mobilePhone,
            long profileId, DateTime? birthDate = null) : base()
        {
            Name = name;
            LastName = lastName;
            FullName = $"{Name} {LastName}";
            Mail = mail;
            MobilePhone = mobilePhone;
            BirthDate = birthDate;
            ProfileId = profileId;
            Validate();
        }

        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string Mail { get; private set; }
        public string Password { get; private set; }
        public string MobilePhone { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public long ProfileId { get; private set; }
        public Profile Profile { get; private set; }
        public ICollection<Article> Articles { get; private set; }

        public void CleanPassword() => Password = "";

        public override void Validate()
        {
            AddNotifications(new Contract()
                .HasMaxLengthIfNotNullOrEmpty(Name, 100, "Name", "Name must not null, empty or exceed 100 chars")
                .HasMaxLengthIfNotNullOrEmpty(LastName, 100, "LastName", "LastName must not null, empty or exceed 100 chars")
                .IsEmail(Mail, "Mail", "Invalid Mail")
                .IsGreaterThan(ProfileId, 0, "ProfileId", "ProfileId must be valid")
            );

            if(!string.IsNullOrEmpty(Password) && !string.IsNullOrWhiteSpace(Password))
            {
                AddNotifications(new Contract()
                    .HasMinLengthIfNotNullOrEmpty(Password, 6, "Password", "Password must not null, empty or less 6 chars")
                    .HasMaxLengthIfNotNullOrEmpty(Password, 30, "Password", "Password must not null, empty or exceed 100 chars")
                );
            }
            if(BirthDate.HasValue)
            {
                AddNotifications(new Contract()
                    .IsBetween(BirthDate.Value, new DateTime(1900, 01, 01), DateTime.Now.AddYears(-10),
                        "BirthDate", "Invalid Birth Date")
                );
            }
        }
    }
}
