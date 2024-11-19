using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectFinal
{
    internal class DB_connection
    {
        //  string connectionString = "Server=DESKTOP-44IF56K;Database=FinalCSharp;User Id=FinalProject;Password=123;TrustServerCertificate=true";
        string connectionString = "Server=DESKTOP-44IF56K;Database=FinalCSharp;Integrated Security=True;TrustServerCertificate=True";

        public void GetConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show("Sucess");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        public DataTable GetEmployees()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();



                string query = "SELECT Employees.*, Salaries.SalaryAmount FROM Employees JOIN Salaries ON Employees.EmployeeId = Salaries.EmployeeId";

                using (SqlCommand selectCommand = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand))
                    {
                        DataTable dataTable = new DataTable();
                        sqlDataAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }

        }
        public void Login(string userName, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM Admin WHERE username = @username AND password = @password";
                    using (SqlCommand loginCmd = new SqlCommand(sql, connection))
                    {
                        loginCmd.Parameters.AddWithValue("@username", userName);
                        loginCmd.Parameters.AddWithValue("@password", password);
                        using (SqlDataReader reader = loginCmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                MessageBox.Show("Login successful!");
                                Employee employee = new Employee();
                                employee.Show();
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Username or Password!");
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        //public void Insert(string FirstName, string LastName, DateTime DOB,string Address ,int DepartmentID, decimal salary)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        // Insert employee data and get the new employee's ID
        //        string employeeSql = "INSERT INTO Employees (FirstName, LastName, DateOfBirth, Address, DepartmentID) " +
        //                             "VALUES (@FirstName, @LastName, @DateOfBirth, @Address, @DepartmentID);" +
        //                             "SELECT SCOPE_IDENTITY();";
        //        using (SqlCommand EmpInsertcommand = new SqlCommand(employeeSql, connection))
        //        {
        //            EmpInsertcommand.Parameters.AddWithValue("@FirstName", FirstName);
        //            EmpInsertcommand.Parameters.AddWithValue("@LastName", LastName);
        //            EmpInsertcommand.Parameters.AddWithValue("@DateOfBirth", DOB);
        //            EmpInsertcommand.Parameters.AddWithValue("@Address", Address);
        //            EmpInsertcommand.Parameters.AddWithValue("@DepartmentID", DepartmentID);

        //            // Execute the command to insert the employee and get the employee ID
        //            int empId = Convert.ToInt32(EmpInsertcommand.ExecuteScalar());

        //            // Insert salary data for the employee
        //            string salarySql = "INSERT INTO Salary (EmployeeID, SalaryAmount) VALUES (@empId, @SalaryAmount)";
        //            using (SqlCommand salaryCmd = new SqlCommand(salarySql, connection))
        //            {
        //                salaryCmd.Parameters.AddWithValue("@EmployeeID", empId);
        //                salaryCmd.Parameters.AddWithValue("@SalaryAmount", salary);

        //                // Execute the salary insertion
        //                salaryCmd.ExecuteNonQuery();
        //                MessageBox.Show("Employee has been added successfully!");
        //            }
        //        }
        //    }
        //}
        public void Insert(string FirstName, string LastName, DateTime DOB, string Address, int DepartmentID, decimal salary)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string employeeSql = "INSERT INTO Employees (FirstName, LastName, DateOfBirth, Address, DepartmentID) " +
                                     "VALUES (@FirstName, @LastName, @DateOfBirth, @Address, @DepartmentID);" +
                                     "SELECT SCOPE_IDENTITY();";

                using (SqlCommand EmpInsertCommand = new SqlCommand(employeeSql, connection))
                {

                    EmpInsertCommand.Parameters.AddWithValue("@FirstName", FirstName);
                    EmpInsertCommand.Parameters.AddWithValue("@LastName", LastName);
                    EmpInsertCommand.Parameters.AddWithValue("@DateOfBirth", DOB);
                    EmpInsertCommand.Parameters.AddWithValue("@Address", Address);
                    EmpInsertCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID);


                    int empId = Convert.ToInt32(EmpInsertCommand.ExecuteScalar());


                    string salarySql = "INSERT INTO Salaries (EmployeeID, SalaryAmount) VALUES (@EmployeeID, @SalaryAmount)";
                    using (SqlCommand salaryCmd = new SqlCommand(salarySql, connection))
                    {

                        salaryCmd.Parameters.AddWithValue("@EmployeeID", empId);
                        salaryCmd.Parameters.AddWithValue("@SalaryAmount", salary);
                        salaryCmd.ExecuteNonQuery();
                    }
                }

            }
            MessageBox.Show("Employee has been added successfully!");
        }

        ///update
        public void Update(int employeeID, string firstName, string lastName, DateTime dob, string address, int departmentID, decimal salary)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateEmployeeSql = "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, " +
                                           "Address = @Address, DepartmentID = @DepartmentID WHERE EmployeeID = @EmployeeID";

                using (SqlCommand updateEmployeeCmd = new SqlCommand(updateEmployeeSql, connection))
                {
                    updateEmployeeCmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                    updateEmployeeCmd.Parameters.AddWithValue("@FirstName", firstName);
                    updateEmployeeCmd.Parameters.AddWithValue("@LastName", lastName);
                    updateEmployeeCmd.Parameters.AddWithValue("@DateOfBirth", dob);
                    updateEmployeeCmd.Parameters.AddWithValue("@Address", address);
                    updateEmployeeCmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                    updateEmployeeCmd.ExecuteNonQuery();
                }

                string updateSalarySql = "UPDATE Salaries SET SalaryAmount = @SalaryAmount WHERE EmployeeID = @EmployeeID";
                using (SqlCommand updateSalaryCmd = new SqlCommand(updateSalarySql, connection))
                {
                    updateSalaryCmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                    updateSalaryCmd.Parameters.AddWithValue("@SalaryAmount", salary);

                    updateSalaryCmd.ExecuteNonQuery();
                }
            }
            //MessageBox.Show("Employee details updated successfully!");
        }

        ///delete 
        public void Delete(int employeeID)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteSalarySql = "DELETE FROM Salaries WHERE EmployeeID = @EmployeeID";
                using (SqlCommand deleteSalaryCmd = new SqlCommand(deleteSalarySql, connection))
                {
                    deleteSalaryCmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                    deleteSalaryCmd.ExecuteNonQuery();
                }

                string deleteEmployeeSql = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
                using (SqlCommand deleteEmployeeCmd = new SqlCommand(deleteEmployeeSql, connection))
                {
                    deleteEmployeeCmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                    deleteEmployeeCmd.ExecuteNonQuery();
                }
            }
            // MessageBox.Show("Employee has been deleted successfully!");
        }
        public void clearDate()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlClearAllSalaries = "DELETE FROM Salaries";
                using (SqlCommand clearSalaries = new SqlCommand(sqlClearAllSalaries, connection))
                {
                    clearSalaries.ExecuteNonQuery();
                }
                string sqlClearEmp = "DELETE FROM Employees";
                using (SqlCommand clearEmp = new SqlCommand(sqlClearEmp, connection))
                {
                    clearEmp.ExecuteNonQuery();
                }
            }

        }
        public DataTable GetEmployeesInSalaryForm()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT Employees.EmployeeID, Employees.FirstName, Employees.LastName, Salaries.SalaryAmount
            FROM Employees
            JOIN Salaries ON Employees.EmployeeID = Salaries.EmployeeID";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable employeeData = new DataTable();
                adapter.Fill(employeeData);
                return employeeData;
            }
        }


        public DataTable GetEmployeeByID(string employeeID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT Employees.EmployeeID, Employees.FirstName, Employees.LastName, Salaries.SalaryAmount
                FROM Employees
                JOIN Salaries ON Employees.EmployeeID = Salaries.EmployeeID
                WHERE Employees.EmployeeID = @EmployeeID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeID", employeeID);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable employeeData = new DataTable();
                adapter.Fill(employeeData);
                return employeeData;
            }
        }
        public DataTable GetReport()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT Employees.EmployeeID, Employees.FirstName, Employees.LastName, Salaries.SalaryAmount
        FROM Employees
        JOIN Salaries ON Employees.EmployeeID = Salaries.EmployeeID";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable employeeData = new DataTable();
                adapter.Fill(employeeData);
                return employeeData;
            }
        }

        //public DataTable LoadReport()
        //{

        //    string query = "SELECT * FROM Report"; // Query the view

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
        //        DataTable table = new DataTable();
        //        adapter.Fill(table);

        //        return table;
        //    }

        //}
        public DataTable LoadReport()
        {
            // Create a new SQL connection and command
            string query = "SELECT * FROM Report";  // Data query for report
            string countQuery = "SELECT COUNT(*) AS TotalEmployees FROM Report"; // Count query for total employees

            // Your existing DB connection code
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Load the report data
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);

                // Fetch total employees count
                SqlDataAdapter countAdapter = new SqlDataAdapter(countQuery, connection);
                DataTable countTable = new DataTable();
                countAdapter.Fill(countTable);

                // Get the total employee count
                int totalEmployees = (int)countTable.Rows[0]["TotalEmployees"];

                // Add the total employees count to each row in the report
                table.Columns.Add("TotalEmployees", typeof(int));
                foreach (DataRow row in table.Rows)
                {
                    row["TotalEmployees"] = totalEmployees; // Add total count to each row
                }

                return table;
            }
        }
        public int count()
        {
            string query = "SELECT COUNT(*) FROM Employees";

            // Connection string (replace with your actual connection string)
  

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
      
                    connection.Open();


                    SqlCommand command = new SqlCommand(query, connection);

                    int employeeCount = (int)command.ExecuteScalar();

                    return employeeCount;
                }
                catch (Exception ex)
                {
                    // Handle any errors by showing a message box
                    MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;  
                }
            }
        }

    }

}
