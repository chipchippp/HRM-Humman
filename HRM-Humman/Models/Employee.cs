using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM_Humman.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please enter your email address.")]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Invalid phone number.")]
        [Column(TypeName = "nvarchar(11)")]
        public string Phone { get; set; }
    }
}
