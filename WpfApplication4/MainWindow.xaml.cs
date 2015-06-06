using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Reflection;
using System.Configuration;


namespace EvalonClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //字符串赋值:用户名 密码 用户类型
            string username = textBox1.Text.Trim();

            string password = textBox2.Password.Trim();
            string type = "";
            if (radiobutton1.IsChecked == true)
            {
                type = "学生";
            }
            else if (radiobutton2.IsChecked == true)
            {
                type = "教师";
            }



            //定义数据库连接语句:服务器=.(本地) 数据库名=master
            string consqlserver = ConfigurationManager.AppSettings["cnStr"];
            

            //定义SQL查询语句:用户名 密码  用户类型
            string sql = "select * from dbo.登录信息表 where 用户名='" + username + "' and 密码='" + password + "'and 用户类型='"+type+"'";

        

            //定义SQL Server连接对象 打开数据库  
            SqlConnection con = new SqlConnection(consqlserver);
            con.Open();

            //定义查询命令:表示对数据库执行一个SQL语句或存储过程  
            SqlCommand com = new SqlCommand(sql, con);

            //执行查询:提供一种读取数据库行的方式  
            SqlDataReader sread = com.ExecuteReader();

            try
            {
                //如果存在用户名和密码正确数据执行进入系统操作  
                if (sread.Read())
                {
                    MessageBox.Show("登录成功");

                    if (type == "学生")
                    {
                        var sw = new StudentWindow(username);
                        sw.Show();
                    }
                    else
                    {
                        var tw = new TeacherWindow(username);
                        tw.Show();
                    }
                    Close();
                    return;  
                }
                else
                {
                    MessageBox.Show("请输入正确的用户名和密码");
                }
            }
            catch (Exception msg)
            {
                throw new Exception(msg.ToString());  //处理异常信息  
            }
            finally
            {
                con.Close();                    //关闭连接  
                con.Dispose();                  //释放连接  
                sread.Dispose();                //释放资源  
            }  
            

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
