using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;



namespace EvalonClient
{
    public class DisconnectionLayer
    {
        //连接字符串
        private string cnString = string.Empty;
        private DataSet MasterDS = new DataSet("Eduction System Database");

        //命令对象,只有三个表项，之后可根据需求扩展
        private SqlCommandBuilder sqlCBStudents;
        private SqlCommandBuilder sqlCBLogins;
        private SqlCommandBuilder sqlCBTeachers;
        private SqlCommandBuilder sqlCBCourses;


        //适配器对象
        private SqlDataAdapter studentTableAdapter;
        private SqlDataAdapter loginTableAdapter;
        private SqlDataAdapter teacherTableAdapter;
        private SqlDataAdapter courseTableAdapter;


        public DisconnectionLayer(string connectionString)
        {
            cnString = connectionString;
            ConfigureAdapter();
        }

        private void ConfigureAdapter()
        {
            //新建adapter并且配置SelectCommand.
            studentTableAdapter = new SqlDataAdapter("Select * From dbo.学生信息表", cnString);
            loginTableAdapter = new SqlDataAdapter("Select * From dbo.登录信息表", cnString);
            teacherTableAdapter = new SqlDataAdapter("Select * From dbo.教师信息表", cnString);
            courseTableAdapter = new SqlDataAdapter("Select * From dbo.课程信息表", cnString);

            // 使用SqlCommandBuilder.
            sqlCBStudents = new SqlCommandBuilder(studentTableAdapter);
            sqlCBLogins = new SqlCommandBuilder(loginTableAdapter);
            sqlCBTeachers = new SqlCommandBuilder(teacherTableAdapter);
            sqlCBCourses = new SqlCommandBuilder(courseTableAdapter);
            
            //将Datatable添加到Dataset中
            studentTableAdapter.Fill(MasterDS, "Students");
            loginTableAdapter.Fill(MasterDS, "Logins");
            teacherTableAdapter.Fill(MasterDS, "Teachers");
            courseTableAdapter.Fill(MasterDS, "Courses");
        }


        public void UpdateMasterDS()
        {
            MasterDS.AcceptChanges();
        }

        public DataTable getStudents()
        {
            return MasterDS.Tables["Students"];
        }

        public DataTable getLogins()
        {
            return MasterDS.Tables["Logins"];
        }

        public DataTable getTeachers()
        {
            return MasterDS.Tables["Teachers"];
        }

        public DataTable getCourses()
        {
            return MasterDS.Tables["Courses"];
        }
    }
}

