using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;
using EmployeeSystem.Pages.Models;
namespace EmployeeSystem.Pages
{
    public class IndexModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public List<EmployeeModel> Employees { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        
        public void OnGet()
        {
            Employees = GetEmployees(Name);
        }

        private List<EmployeeModel> GetEmployees(string name)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "DESKTOP-RFNME1A"; // Replace with the server name or address
            builder.InitialCatalog = "Employeesystem"; // Replace with the database name
            builder.IntegratedSecurity = true;

            string connectionString = builder.ConnectionString;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Employee WHERE (@name IS NULL OR Name = @name) ORDER BY BornDate ASC";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    //If there is no name, then the parameter is set to dbnull
                    if (string.IsNullOrEmpty(name))
                    {
                        command.Parameters.AddWithValue("@name", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@name", name ?? string.Empty);
                    }


                    List<EmployeeModel> employees = new List<EmployeeModel>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployeeModel employee = new EmployeeModel
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                LastName = reader.GetString(2),
                                RFC = reader.GetString(3),
                                BornDate = reader.GetDateTime(4),
                                Status = reader.GetString(5)
                            };
                            employees.Add(employee);
                        }
                    }
                    return employees;
                }
            }
        }
    }
}
