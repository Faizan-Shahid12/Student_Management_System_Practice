namespace Student_Management_System.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string key { get; set; }
        public int Expiry { get; set; }

    }
}
