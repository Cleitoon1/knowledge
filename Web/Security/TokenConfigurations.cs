namespace Web.Security
{
    public class TokenConfigurations
    {
        public TokenConfigurations()
        {
            
        }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
        public string PrivateKey { get; set; }
    }
}
