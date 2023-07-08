namespace WalletGraphs.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Adress { get; set; }

        public ICollection<Expenditure> Expenditures { get; set; }

        public User(String Name,String Surname)
        {
                this.Name = Name;
                this.Surname = Surname;
                Expenditures = new List<Expenditure>();
        }
        public User(String Name, String Surname,String Adress)
        {
                this.Name = Name;
                this.Surname = Surname;
                this.Adress = Adress;
                Expenditures = new List<Expenditure>();
        }
    }
}
