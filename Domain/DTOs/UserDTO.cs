using System;

namespace Domain.DTOs
{
    public class UserDTO
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string MobilePhone { get; set; }
        public DateTime? BirthDate { get; set; }
        public int ProfileId { get; set; }
        public string ProfileName { get; set; }
    }
}
