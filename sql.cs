using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Connection string for connecting to the SQL Server database
        string connectionString = "Your_Connection_String_Here";

        // Name of the stored procedure
        string storedProcedureName = "Your_Stored_Procedure_Name_Here";

        // Create a SqlConnection to connect to the database
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                // Open the connection
                connection.Open();

                // Create a SqlCommand for the stored procedure
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    // Set the command type to StoredProcedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add input parameters and set their values
                    // Replace "ParameterName1" to "ParameterName4" with actual parameter names
                    // Add the first input parameter
                    command.Parameters.AddWithValue("@ParameterName1", "Value1");

                    // Add the second input parameter
                    command.Parameters.AddWithValue("@ParameterName2", "Value2");

                    // Add the third input parameter
                    command.Parameters.AddWithValue("@ParameterName3", "Value3");

                    // Add the fourth input parameter
                    command.Parameters.AddWithValue("@ParameterName4", "Value4");

                    // Execute the stored procedure
                    command.ExecuteNonQuery();

                    Console.WriteLine("Stored procedure executed successfully.");
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine("SQL exception occurred while executing the stored procedure:");
                Console.WriteLine($"Error Code: {ex.ErrorCode}");
                Console.WriteLine($"Error Message: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine("An unexpected error occurred:");
                Console.WriteLine($"Error Message: {ex.Message}");
            }
            finally
            {
                // The connection will be closed automatically when it goes out of scope due to the using statement
            }
        }
    }
}
