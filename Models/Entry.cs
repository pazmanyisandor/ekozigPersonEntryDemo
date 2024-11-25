using System.ComponentModel.DataAnnotations;

namespace ekozigPersonEntryDemo.Models
{
    public class Entry
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public int AddressID { get; set; } // Assuming AddressID is a required foreign key.

        [Required]
        [EmailAddress]
        [StringLength(100)] // Assuming a maximum length for email.
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(20)] // Assuming a reasonable max length for phone numbers.
        public string Phone { get; set; }

        [Required]
        [StringLength(10)] // Assuming a fixed max length for "Sex" (e.g., "Male", "Female").
        public string Sex { get; set; }
    }
}