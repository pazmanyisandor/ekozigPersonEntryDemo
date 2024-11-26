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

                    //Insert
                    if (entry.Id == 0)
                    {
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

                    //Update
                    else
                    {
                        string addressQuery = @"
                        UPDATE address 
                        SET PostCode = @PostCode, Town = @Town,Street = @Street,StreetType = @StreetType,HouseNumber = @HouseNumber,Floor = @Floor,Door = @Door,RingNumber= @RingNumber
                        WHERE AddressID = (SELECT AddressID FROM entry WHERE id=@ID)";

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
                            addressCommand.Parameters.AddWithValue("@ID", entry.Id);
                        }

                        string entryQuery = @"
                        UPDATE entry 
                        SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, Sex = @Sex
                        WHERE ID=@ID";

                        using (SqlCommand entryCommand = new SqlCommand(entryQuery, connection))
                        {
                            entryCommand.Parameters.AddWithValue("@FirstName", entry.FirstName);
                            entryCommand.Parameters.AddWithValue("@LastName", entry.LastName);
                            entryCommand.Parameters.AddWithValue("@Email", entry.Email);
                            entryCommand.Parameters.AddWithValue("@Phone", entry.Phone);
                            entryCommand.Parameters.AddWithValue("@Sex", entry.Sex);
                            entryCommand.Parameters.AddWithValue("@ID", entry.Id);
                            entryCommand.ExecuteNonQuery();
                        }
                    }
                }

                return RedirectToAction("Index", "Entry");
            }

            return View(entry);
        }

        // Display the form to edit an entry
        [HttpGet]
        public IActionResult Edit()
        {
            Entry entry = new Entry();

            if (ModelState.IsValid)
            {
                string id = Request.Query["ID"];
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            e.Id, e.FirstName, e.LastName, e.Email, e.Phone, e.Sex,
                            a.PostCode, a.Town, a.Street, a.StreetType, a.HouseNumber, a.Floor, a.Door, a.RingNumber
                        FROM 
                            entry e
                        INNER JOIN 
                            address a ON e.AddressID = a.AddressID
                        WHERE
                            e.id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                entry = new Entry
                                {
                                    Id = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Email = reader.GetString(3),
                                    Phone = reader.GetString(4),
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
                                        RingNumber = reader.IsDBNull(13) ? (int?)null : reader.GetInt32(13)
                                    }
                                };
                            }
                        }
                    }
                }
            }

            return View("Create", entry);
        }
    }
}