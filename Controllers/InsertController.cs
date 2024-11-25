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

        // Handle form submission to insert a new entry
        [HttpPost]
        public IActionResult Create(Entry entry)
        {
            if (ModelState.IsValid)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO entry (FirstName, LastName, AddressID, Email, Phone, Sex) VALUES (@FirstName, @LastName, @AddressID, @Email, @Phone, @Sex)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", entry.FirstName);
                        command.Parameters.AddWithValue("@LastName", entry.LastName);
                        command.Parameters.AddWithValue("@AddressID", entry.AddressID);
                        command.Parameters.AddWithValue("@Email", entry.Email);
                        command.Parameters.AddWithValue("@Phone", entry.Phone);
                        command.Parameters.AddWithValue("@Sex", entry.Sex);
                        command.ExecuteNonQuery();
                    }
                }

                return RedirectToAction("Index", "Entry");
            }

            return View(entry);
        }
    }
}