namespace FinalMVC.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public WalletModel WalletModel { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
}