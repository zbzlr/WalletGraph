namespace WalletGraphs.Models
{
    public class Expenditure
    {
        public int ExpenditureId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Reciever { get; set; }

        public Expenditure()
        {
                
        }
    }
}
