using System;

namespace Web.ViewModels
{
    public class UserVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string MobilePhone { get; set; }
        public DateTime? BirthDate { get; set; }
        public long ProfileId { get; set; }
    }
}