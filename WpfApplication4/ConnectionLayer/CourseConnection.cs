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
    //待选的课
    public class ChoosedCourse
    {
        public bool 选择 { get; set; }
        public string 课程号 { get; set; }
        public string 课程名称 { get; set; }
        public Nullable<int> 学分 { get; set; }
        public Nullable<int> 学时 { get; set; }
        public string 上课地点 { get; set; }
        public string 上课时间 { get; set; }
        public  Nullable<int> 选课人数 { get; set; }
        public  Nullable<int> 预定人数 { get; set; } 
    }

    //已经被选了的课
    public class MyCourse
    {
        public bool 选择 { get; set; }
        public string 课程号 { get; set; }
        public string 课程名称 { get; set; }
        public Nullable<int> 学分 { get; set; }
        public Nullable<int> 学时 { get; set; }
        public string 上课地点 { get; set; }
        public string 上课时间 { get; set; }

    }

    //定义课程类
    public class Course
    {
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public int CourseScore { get; set; }
        public int CourseTime { get; set; }
        public string Place { get; set; }
        public string Time { get; set; }
    }

    public class CourseConnection
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
        public List<Course> GetAllCourseAsList()
        {
            
            List<Course> courses = new List<Course>();

            string sql = "Select * From dbo.课程信息表";
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    courses.Add(new Course
                    {
                        CourseID= (string)dr["课程号"],
                        CourseName = (string)dr["课程名称"],
                        CourseScore = (int)dr["学分"],
                        CourseTime = (int)dr["学时"],
                        Place= (string)dr["上课地点"],
                        Time=(string)dr["上课时间"]
                    });
                }
                dr.Close();
            }
            return courses;
        }
        #endregion

    }
}
