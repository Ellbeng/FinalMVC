using FinalMVC.Areas.Identity.Data;

namespace FinalMVC.Models
{
    public class TransactionInfo
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public string PaymentType { get; set; }
    }
}
