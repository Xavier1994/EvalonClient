using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace EvalonClient
{
    //定义学生类
    public class Student
    {
        public string StudentID { get; set; }
        public string Name { get; set; }
        public string Sexuality { get; set; }
        public int Age { get; set; }
        public int DepartmentID { get; set; }
        public string OriginPlace { get; set; }
        public string Nationality { get; set; }
    }

    public class StudentConnection
    {

        //定义数据库连接
        private SqlConnection sqlCn = null;

        #region Open / Close methods
        public void OpenConnection(string connectionString)
        {
            sqlCn = new SqlConnection();
            sqlCn.ConnectionString = connectionString;
            sqlCn.Open();
        }

        public void CloseConnection()
        {
            sqlCn.Close();
        }
        #endregion

        #region Insert method (no param-query)
        public void InsertAuto(Student student)
        {
            //使用类成员来生产SQL语句
            string sql = string.Format("Insert Into dbo.学生信息表" +
              "(学号,姓名,性别,年龄,系号,籍贯,民族) Values" +
              "('{0}', '{1}', '{2}', '{3}','{4}','{5}','{6}')", student.StudentID, student.Name, student.Sexuality, student.Age, student.DepartmentID, student.OriginPlace, student.Nationality);

            //执行SQL语句，插入数据库无返回值
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        //不使用类直接插入，作为重载函数
        public void InsertAuto(int id, string name, string sexuality, int age, int departmentid, string originpalce, string natinality)
        {
            string sql = string.Format("Insert Into dbo.学生信息表" +
               "(学号,姓名,性别,年龄,系号,籍贯,民族) Values" +
               "('{0}', '{1}', '{2}', '{3}','{4}','{5}','{6}')", id, name, sexuality, age, departmentid, originpalce, natinality);

            //执行SQL语句，插入数据库无返回值
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                cmd.ExecuteNonQuery();
            }
        }
        #endregion



        /*
        //使用参数插入，可作为补充实现
        #region Insert method (param-query
        public void InsertAuto(int id, string color, string make, string petName)
        {
            // Note the "placeholders" in the SQL query.
            string sql = string.Format("Insert Into Inventory" +
                "(CarID, Make, Color, PetName) Values" +
                "(@CarID, @Make, @Color, @PetName)");

            // This command will have internal parameters.
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                // Fill params collection.
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@CarID";
                param.Value = id;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Make";
                param.Value = make;
                param.SqlDbType = SqlDbType.Char;
                param.Size = 10;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Color";
                param.Value = color;
                param.SqlDbType = SqlDbType.Char;
                param.Size = 10;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@PetName";
                param.Value = petName;
                param.SqlDbType = SqlDbType.Char;
                param.Size = 10;
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion
         * */


        #region Delete method
        //使用学生的学号来删除学生的对应信息
        public void DeleteStudent(string id)
        {

            string sql = string.Format("Delete from dbo.学生信息表 where 学号 = {0}",
              id);
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Exception error = new Exception("Sorry! That car is on order!", ex);
                    throw error;
                }
            }
        }
        #endregion

        #region Find student
        //使用学号来查找获取当前学生
        public Student FindStudent(string studentid)
        {
            Student student;
            string sql = string.Format("Select * from dbo.学生信息表 where 学号 = {0}", studentid);
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                student = new Student
                {
                    StudentID = (string)dr["学号"],
                    Name = (string)dr["姓名"],
                    Sexuality = (string)dr["性别"],
                    Age = (int)dr["年龄"],
                    DepartmentID = (int)dr["系号"],
                    OriginPlace = (string)dr["籍贯"],
                    Nationality = (string)dr["民族"],
                };
                dr.Close();
            }
            return student;
        }
        #endregion



        /*
        #region Update method
        public void UpdateCarPetName(int id, string newPetName)
        {
            // Get ID of car to modify and new pet name.
            string sql =
              string.Format("Update Inventory Set PetName = '{0}' Where CarID = '{1}'",
              newPetName, id);
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                cmd.ExecuteNonQuery();
            }
        }
        #endregion
         * */

        /*
        #region Select methods
        public DataTable GetAllStudentsAsDataTable()
        {
            // This will hold the records.
            DataTable students = new DataTable();

            // Prep command object.
            string sql = "Select * From dbo.学生信息表";
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                // Fill the DataTable with data from the reader and clean up.
                students.Load(students);
                dr.Close();
            }
            return students;
        }
        */

        #region
        public List<Student> GetAllStudentsAsList()
        {
            // This will hold the records.
            List<Student> students = new List<Student>();

            // Prep command object.
            string sql = "Select * From dbo.学生信息表";
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    students.Add(new Student
                    {
                        StudentID = (string)dr["学号"],
                        Name = (string)dr["姓名"],
                        Sexuality = (string)dr["性别"],
                        Age = (int)dr["年龄"],
                        DepartmentID = (int)dr["系号"],
                        OriginPlace = (string)dr["籍贯"],
                        Nationality = (string)dr["民族"],
                    });
                }
                dr.Close();
            }
            return students;
        }
        #endregion

        /*
        #region Look up pet name
        public string LookUpPetName(int carID)
        {
            string carPetName = string.Empty;

            // Establish name of stored proc.
            using (SqlCommand cmd = new SqlCommand("GetPetName", this.sqlCn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input param.
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@carID";
                param.SqlDbType = SqlDbType.Int;
                param.Value = carID;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                // Output param.
                param = new SqlParameter();
                param.ParameterName = "@petName";
                param.SqlDbType = SqlDbType.Char;
                param.Size = 10;
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                // Execute the stored proc.
                cmd.ExecuteNonQuery();

                // Return output param.
                carPetName = (string)cmd.Parameters["@petName"].Value;
            }
            return carPetName;
        }
        #endregion
         * */

        /*
        #region Tx method
        // A new member of the InventoryDAL class.
        public void ProcessCreditRisk(bool throwEx, int custID)
        {
            // First, look up current name based on customer ID.
            string fName = string.Empty;
            string lName = string.Empty;
            SqlCommand cmdSelect = new SqlCommand(
              string.Format("Select * from Customers where CustID = {0}", custID), sqlCn);
            using (SqlDataReader dr = cmdSelect.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    dr.Read();
                    fName = (string)dr["FirstName"];
                    lName = (string)dr["LastName"];
                }
                else
                    return;
            }

            // Create command objects that represent each step of the operation.
            SqlCommand cmdRemove = new SqlCommand(
              string.Format("Delete from Customers where CustID = {0}", custID), sqlCn);

            SqlCommand cmdInsert = new SqlCommand(string.Format("Insert Into CreditRisks" +
                             "(CustID, FirstName, LastName) Values" +
                             "({0}, '{1}', '{2}')", custID, fName, lName), sqlCn);

            // We will get this from the connection object.
            SqlTransaction tx = null;
            try
            {
                tx = sqlCn.BeginTransaction();

                // Enlist the commands into this transaction.
                cmdInsert.Transaction = tx;
                cmdRemove.Transaction = tx;

                // Execute the commands.
                cmdInsert.ExecuteNonQuery();
                cmdRemove.ExecuteNonQuery();

                // Simulate error.
                if (throwEx)
                {
                    throw new Exception("Sorry!  Database error! Tx failed...");
                }

                // Commit it!
                tx.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Any error will roll back transaction.
                tx.Rollback();
            }
        }
         * */

    }
}
