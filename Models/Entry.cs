using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("AddressID")]
        public Address Address { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [StringLength(10)]
        public string Sex { get; set; }
    }

    public class Address
    {
        public int AddressID { get; set; }

        [Required]
        [StringLength(4)]
        public string PostCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Town { get; set; }

        [Required]
        [StringLength(100)]
        public string Street { get; set; }

        [StringLength(50)]
        public string StreetType { get; set; }

        [Required]
        public int HouseNumber { get; set; }

        public int? Floor { get; set; }

        public int? Door { get; set; }

        public int? RingNumber { get; set; }
    }
}