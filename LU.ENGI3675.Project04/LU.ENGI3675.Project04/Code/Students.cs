// <copyright file="Students.cs" company="ENGI3675">
// The Database data struct.
// </copyright>
namespace LU.ENGI3675.Proj04.App_Code
{
    /// <summary>
    /// This structure stores each column of one row from table Students.
    /// </summary>
    public struct Students
    {
        /// <summary>
        /// this stores an integer. it is the primary key ID
        /// </summary>
        public int ID;

        /// <summary>
        /// This stores a string for the Students Name. cannot be null.
        /// </summary>
        public string Name;

        /// <summary>
        /// this stores a floating point value, for Students GPA. cannot be null. must be between 0 and 4 inclusive.
        /// </summary>
        public double GPA;
    }
}