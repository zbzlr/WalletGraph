using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WalletGraphs.Models
{
    public class User
    {

        public ICollection<Expenditure>? Expenditures { get; set; }
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }
        public string? Adress { get; set; }

        [Required]
        [Remote(action:"hasEmail",controller:"Home")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

       

        public User()
        {
                
        }
    }
}
