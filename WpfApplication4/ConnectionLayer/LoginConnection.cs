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
  

    public class LoginConnection
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

        #region 查找登陆账户的登陆密码
        public string FindPwd(string studentid)
        {
            string pwd = string.Empty;
            string sql = "select * from dbo.登录信息表 where 用户名='" + studentid + "'";
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                pwd = (string)dr["密码"];
                dr.Close();
            }
            return pwd;
        }
        #endregion


        #region 查找登陆账户的类型
        public string FindType(string studentid)
        {
            string type = string.Empty;
            string sql = "select * from dbo.登录信息表 where 用户名='" + studentid + "'";
            using(SqlCommand cmd = new SqlCommand(sql,this.sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                type = (string)dr["用户类型"];
                dr.Close();
            }
            return type;
        }
        #endregion


        #region 支持用户修改密码
        public void ChangePwd(string studentid, string newpwd)
        {
            string sql = "update dbo.登录信息表 set 密码='"+ newpwd+"'"+"  where 用户名='" + studentid + "'";
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

    }
}
