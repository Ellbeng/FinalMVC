namespace FinalMVC.Models
{
    public class History
    {
      
        public string PaymentType { get; set; }
        
        public string Currency { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public decimal Amount { get; set; }

    }
}
