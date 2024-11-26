using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ekozigPersonEntryDemo.Models
{
    public class Entry
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A keresztnév megadása kötelező.")]
        [StringLength(50, ErrorMessage = "A keresztnév maximum 50 karakter lehet.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "A vezetéknév megadása kötelező.")]
        [StringLength(50, ErrorMessage = "A vezetéknév maximum 50 karakter lehet.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "A cím megadása kötelező.")]
        [ForeignKey("AddressID")]
        public Address Address { get; set; }

        [Required(ErrorMessage = "Az email megadása kötelező.")]
        [EmailAddress(ErrorMessage = "Az email formátuma helytelen.")]
        [StringLength(100, ErrorMessage = "Az email maximum 100 karakter lehet.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "A telefonszám formátuma helytelen.")]
        [StringLength(50, ErrorMessage = "A telefonszám maximum 50 karakter lehet.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "A nem megadása kötelező.")]
        [StringLength(10, ErrorMessage = "A nem maximum 10 karakter lehet.")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "A létrehozás dátuma megadása kötelező.")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "A módosítás dátuma megadása kötelező.")]
        public DateTime ModifiedAt { get; set; }
    }

    public class Address
    {
        public int AddressID { get; set; }

        [Required(ErrorMessage = "Az irányítószám megadása kötelező.")]
        [StringLength(4, ErrorMessage = "Az irányítószám pontosan 4 karakter legyen.")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "A település megadása kötelező.")]
        [StringLength(100, ErrorMessage = "A település neve maximum 100 karakter lehet.")]
        public string Town { get; set; }

        [Required(ErrorMessage = "Az utca megadása kötelező.")]
        [StringLength(100, ErrorMessage = "Az utca neve maximum 100 karakter lehet.")]
        public string Street { get; set; }

        [StringLength(50, ErrorMessage = "A közterület jellege maximum 50 karakter lehet.")]
        public string StreetType { get; set; }

        [Required(ErrorMessage = "A házszám megadása kötelező.")]
        public int HouseNumber { get; set; }

        public int? Floor { get; set; }

        public int? Door { get; set; }

        public int? RingNumber { get; set; }
    }
}