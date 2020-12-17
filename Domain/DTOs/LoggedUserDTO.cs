namespace Domain.DTOs {
    public class LoggedUserDTO {
        public LoggedUserDTO (long id, string name, string mail, string profile) {
            this.Id = id;
            this.Name = name;
            this.Mail = mail;
            this.Profile = profile;

        }
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Mail { get; private set; }
        public string Profile { get; private set; }
    }
}