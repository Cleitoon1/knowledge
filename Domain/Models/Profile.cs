using Domain.Models.Base;
using Flunt.Validations;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Profile : BaseEntity
    {
        public Profile(string name) : base()
        {
            Name = name;
            Validate();
        }

        public string Name { get; private set; }
        public ICollection<User> Users { get; private set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .HasMaxLengthIfNotNullOrEmpty(Name, 50, "Name", "Name must not null, empty or exceed 50 chars"));
        }
    }
}
