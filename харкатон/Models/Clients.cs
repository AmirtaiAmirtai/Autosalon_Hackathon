using System.ComponentModel.DataAnnotations;

namespace харкатон.Models
{
    public class Clients
    {
        public int Id { get; set; } 
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int IsActive { get; set; }
    }
}
