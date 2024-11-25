using ekozigPersonEntryDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ekozigPersonEntryDemo.Controllers
{
    public class EntryController : Controller
    {
        private readonly IConfiguration _configuration;

        public EntryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            List<Entry> products = new List<Entry>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, FirstName, LastName FROM entry";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Entry
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2)
                            });
                        }
                    }
                }
            }

            return View(products);
        }
    }
}
