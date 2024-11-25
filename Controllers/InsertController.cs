using ekozigPersonEntryDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ekozigPersonEntryDemo.Controllers
{
    public class InsertController : Controller
    {
        private readonly IConfiguration _configuration;

        public InsertController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Display the form to create a new entry
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Handle form submission to insert a new entry with address
        [HttpPost]
        public IActionResult Create(Entry entry)
        {
            if (ModelState.IsValid)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string addressQuery = @"
                        INSERT INTO address (PostCode, Town, Street, StreetType, HouseNumber, Floor, Door, RingNumber) 
                        OUTPUT INSERTED.AddressID
                        VALUES (@PostCode, @Town, @Street, @StreetType, @HouseNumber, @Floor, @Door, @RingNumber)";

                    int newAddressId;

                    using (SqlCommand addressCommand = new SqlCommand(addressQuery, connection))
                    {
                        addressCommand.Parameters.AddWithValue("@PostCode", entry.Address.PostCode);
                        addressCommand.Parameters.AddWithValue("@Town", entry.Address.Town);
                        addressCommand.Parameters.AddWithValue("@Street", entry.Address.Street);
                        addressCommand.Parameters.AddWithValue("@StreetType", entry.Address.StreetType ?? (object)DBNull.Value);
                        addressCommand.Parameters.AddWithValue("@HouseNumber", entry.Address.HouseNumber);
                        addressCommand.Parameters.AddWithValue("@Floor", entry.Address.Floor ?? (object)DBNull.Value);
                        addressCommand.Parameters.AddWithValue("@Door", entry.Address.Door ?? (object)DBNull.Value);
                        addressCommand.Parameters.AddWithValue("@RingNumber", entry.Address.RingNumber ?? (object)DBNull.Value);

                        newAddressId = (int)addressCommand.ExecuteScalar();
                    }

                    string entryQuery = @"
                        INSERT INTO entry (FirstName, LastName, AddressID, Email, Phone, Sex) 
                        VALUES (@FirstName, @LastName, @AddressID, @Email, @Phone, @Sex)";

                    using (SqlCommand entryCommand = new SqlCommand(entryQuery, connection))
                    {
                        entryCommand.Parameters.AddWithValue("@FirstName", entry.FirstName);
                        entryCommand.Parameters.AddWithValue("@LastName", entry.LastName);
                        entryCommand.Parameters.AddWithValue("@AddressID", newAddressId);
                        entryCommand.Parameters.AddWithValue("@Email", entry.Email);
                        entryCommand.Parameters.AddWithValue("@Phone", entry.Phone);
                        entryCommand.Parameters.AddWithValue("@Sex", entry.Sex);
                        entryCommand.ExecuteNonQuery();
                    }
                }

                return RedirectToAction("Index", "Entry");
            }

            return View(entry);
        }
    }
}