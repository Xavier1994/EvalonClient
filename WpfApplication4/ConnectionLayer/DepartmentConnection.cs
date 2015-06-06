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
    //定义院系类
    public class Department
    {
        public int DepartmentID { set; get; }
        public string CollegeName { set; get; }
        public string DepartmentName { set; get; }
    }

    public class DepartmentConnection
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



        #region
        public List<Department> GetAllDepartmentAsList()
        {
            List<Department> departments = new List<Department>();

            string sql = "Select * From dbo.院系信息表";
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    departments.Add(new Department
                    {
                        DepartmentID = (int)dr["系号"],
                        CollegeName = (string)dr["院名"],
                        DepartmentName = (string)dr["系名"],
                    });
                }
                dr.Close();
            }
            return departments;
        }
        #endregion

        #region 根据系号查找相应的院系名字
        public Department FindDepartment(int departmentid)
        {
            Department department;
            string sql = string.Format("Select * from dbo.院系信息表 where 系号 = {0}", departmentid);
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                department = new Department
                {
                    DepartmentID=(int)dr["系号"],
                    CollegeName=(string)dr["院名"],
                    DepartmentName=(string)dr["系名"],
                };
                dr.Close();
            }
            return department;
        }
        #endregion

    }
}
