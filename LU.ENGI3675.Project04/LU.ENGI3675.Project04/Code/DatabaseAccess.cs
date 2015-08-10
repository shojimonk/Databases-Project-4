// <copyright file="DatabaseAccess.cs" company="ENGI3675">
// The Database Access class, with connect, create, and read functionality.
// </copyright>
namespace LU.ENGI3675.Proj04.App_Code
{
    using System.Collections.Generic;
    using System.Data;
    using Npgsql;

    /// <summary>
    /// The Database Access class, with connect, create, and read functionality.
    /// </summary>
    public class DatabaseAccess
    {
        /// <summary>
        /// Adds a new row of data to the students database
        /// </summary>
        /// <param name="newName"> student's full name</param>
        /// <param name="newGPA"> students GPA</param>
        public static void Create(string newName, string newGPA)
        {
            using (NpgsqlConnection conn = Connect())
            {
                using (NpgsqlCommand command = new NpgsqlCommand("insert into students (name, gpa) values (:name1, :gpa1)", conn))
                {
                    float tryout;
                    if ((newName == string.Empty) || (newName == "Name"))
                    {
                        //command.Parameters.AddWithValue(":name1", null);
                        throw new System.ApplicationException("Name Cannot be Null");
                    }
                    else
                    {
                        command.Parameters.AddWithValue(":name1", newName);
                    }

                    if ((newGPA == string.Empty) || (newGPA == "GPA"))
                    {
                        throw new System.ApplicationException("GPA Cannot be Null");
                        //return "Error: GPA value cannot be Null";
                    }
                    else
                    {
                        if (!float.TryParse(newGPA, out tryout))
                        {
                            throw new System.ApplicationException("Invalid input for GPA");
                            //return "Error: Invalid input for GPA";
                        }

                        if ((tryout < 0.0) || (tryout > 4.0))
                        {
                            throw new System.ApplicationException("GPA is out of bounds");
                            //return "Error: GPA value is out of bounds.";
                        }

                        command.Parameters.AddWithValue(":gpa1", tryout);
                    }

                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }

            return; // "Row successfully added.";
        }

        /// <summary>
        /// Inserts a new row into table Students, but does not protect from SQL injection attacks.
        /// </summary>
        /// <param name="newName"> new students full name</param>
        /// <param name="newGPA"> new students GPA</param>
        public static void UnsafeCreate(string newName, string newGPA)
        {
            using (NpgsqlConnection conn = Connect())
            {
                float tryout;
                if (!float.TryParse(newGPA, out tryout))
                {
                    throw new System.ApplicationException("Invalid entry for GPA");
                }

                NpgsqlCommand command = new NpgsqlCommand("insert into students (name, gpa) values ('" + (string)newName + "', " + (string)newGPA + ")", conn);
                conn.Open();
                command.ExecuteNonQuery();
            }
            return;
        }

        /// <summary>
        /// This function reads all rows from database and stores in list of student structs.
        /// </summary>
        /// <returns> List of Student structs</returns>
        public static List<Students> Read()
        {
            using (NpgsqlConnection conn = Connect())
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("select ID, name, gpa from students order by id ASC", conn);
                NpgsqlDataReader table = command.ExecuteReader();

                List<Students> output = new List<Students>();
                foreach (IDataRecord row in table)
                {
                    Students currentStudent = new Students();
                    currentStudent.Name = (string)row["name"];
                    currentStudent.GPA = (double)row["gpa"];
                    currentStudent.ID = (int)row["id"];
                    output.Add(currentStudent);
                }

                return output;
            }
        }

        /// <summary>
        /// builds connection to database and returns npgsql connection object
        /// </summary>
        /// <returns> Npgsql connection object</returns>
        private static NpgsqlConnection Connect()
        {
            NpgsqlConnectionStringBuilder myBuilder = new NpgsqlConnectionStringBuilder()
            {
                Host = "127.0.0.1",
                Port = 5432,
                Database = "LU.ENGI3675.Proj04",
                IntegratedSecurity = true
            };

            NpgsqlConnection conn = new NpgsqlConnection(myBuilder);
            return conn;
        }
    }
}