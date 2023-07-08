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

        public Expenditure(int UserId, string Description, decimal Amount, string Category)
        {
            Random random = new Random();
            ExpenditureId = random.Next();
            this.UserId = UserId;
            this.Description = Description;
            this.Amount = Amount;
            this.Category = Category;
            Date = DateTime.Now;

        }
    }
}
