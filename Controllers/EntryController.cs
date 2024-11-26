using ekozigPersonEntryDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ekozigPersonEntryDemo.Controllers
{
    /// <summary>
    /// Controller part of the Entrz View
    /// </summary>
    public class EntryController : Controller
    {
        private readonly IConfiguration _configuration;

        public EntryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gathers the data to be listed in the Index page's table
        /// </summary>
        /// <returns>View object containing the gathered entries</returns>
        public IActionResult Index()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            List<Entry> entries = new List<Entry>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Updated query to join entry and address tables
                string query = @"
                    SELECT 
                        e.Id, e.FirstName, e.LastName, e.Email, e.Phone, e.Sex,
                        a.PostCode, a.Town, a.Street, a.StreetType, a.HouseNumber, a.Floor, a.Door, a.RingNumber, e.Modified_at, e.Created_at
                    FROM 
                        entry e
                    INNER JOIN 
                        address a ON e.AddressID = a.AddressID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(new Entry
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                Phone = reader.IsDBNull(4) ? (string?)null : reader.GetString(4),
                                Sex = reader.GetString(5),
                                Address = new Address
                                {
                                    PostCode = reader.GetString(6),
                                    Town = reader.GetString(7),
                                    Street = reader.GetString(8),
                                    StreetType = reader.GetString(9),
                                    HouseNumber = reader.GetInt32(10),
                                    Floor = reader.IsDBNull(11) ? (int?)null : reader.GetInt32(11),
                                    Door = reader.IsDBNull(12) ? (int?)null : reader.GetInt32(12),
                                    RingNumber = reader.IsDBNull(13)? (int?)null : reader.GetInt32(13)
                                },
                                ModifiedAt = reader.GetDateTime(14),
                                CreatedAt = reader.GetDateTime(15)
                            });
                        }
                    }
                }
            }

            return View(entries);
        }

        /// <summary>
        /// Deletes an entry from the database with a specific id
        /// </summary>
        /// <param name="id">: ID specifying the entry to be deleted</param>
        /// <returns>Redirects to the Index page</returns>
        public IActionResult Delete(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM entry WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }
    }
}