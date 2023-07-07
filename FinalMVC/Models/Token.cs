namespace FinalMVC.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PublicToken { get; set; }
        public int PublicTokenStatus { get; set; }
        public string PrivateToken { get; set; }
        public int PrivateTokenStatus { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
