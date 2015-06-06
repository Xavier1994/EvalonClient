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
using System.Windows.Shapes;

namespace EvalonClient
{
    /// <summary>
    /// Teacher.xaml 的交互逻辑
    /// </summary>
    public partial class TeacherWindow : Window
    {
        private readonly string _teacherid;   //私有字段，保存教师的工号


        #region 构造函数，需要传递教师的工号作为参数
        public TeacherWindow(string teacherid)
        {
            _teacherid = teacherid;
            InitializeComponent();
        }
        #endregion



        #region 窗口载入时，初始化窗口的内容
        //初始化窗口的内容
        private void InitialContent()
        {
            //this.TWK.Text = string.Empty;
            this.TSN.Text = string.Empty;


            //显示当前时间
            //this.TTM.Text += DateTime.Now.ToString("yyyy年MM月dd日");   //yyyy年MM月dd日 

            //支持秒的显示，每秒钟更新
            /*
            ShowTimer = new System.Windows.Threading.DispatcherTimer();
            ShowTimer.Tick += new EventHandler(ShowTime);
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();
             * */

            //显示今天时星期几

            /*
            string str = DateTime.Now.DayOfWeek.ToString().Trim();
            string strWeek = "";
            switch (str)
            {
                case "Monday":
                    strWeek = "星期一";
                    break;
                case "Tuesday":
                    strWeek = "星期二";
                    break;
                case "Wednesday":
                    strWeek = "星期三";
                    break;
                case "Thursday":
                    strWeek = "星期四";
                    break;
                case "Friday":
                    strWeek = "星期五";
                    break;
                case "Saturday":
                    strWeek = "星期六";
                    break;
                case "Sunday":
                    strWeek = "星期日";
                    break;
            }
            this.TWK.Text += strWeek;
            */

            //显示当前教师的名字
            using (var context = new EvalonEntities())
            {
                var teacher = (from t in context.教师信息表 where t.工号 == _teacherid select t).FirstOrDefault();
                this.TSN.Text += teacher.姓名 + teacher.职称;
            }
        }
        #endregion


        #region 窗口启动时调用的事件函数
        private void TeacherWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            InitialContent();
        }
        #endregion


        #region 返回页面，主窗口，功能待实现
        private void TretuanHomepage_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion


        #region 显示当前改变密码的窗口
        private void TchangePassword_Click(object sender, RoutedEventArgs e)
        {
            TChangePwd.Visibility = Visibility.Visible;
        }
        #endregion


        #region 刷新当前窗口 待改进
        private void TrefleshPage_Click(object sender, RoutedEventArgs e)
        {
            InitialContent();

        }
        #endregion


        #region  退出当前交互窗口
        private void TquitPage_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("你确定要退出") == MessageBoxResult.Yes)
            {
                this.Close();
            }

        }
        #endregion


        #region 清理窗口
        private  void ClearWindow()
        {
            TTeachCourseView.Visibility=Visibility.Hidden;
            TPersonInfo.Visibility=Visibility.Hidden;
            TChangePwd.Visibility=Visibility.Hidden;
        }
        #endregion


        #region 教师修改密码 
        private void TConfirmChange_Click(object sender, RoutedEventArgs e)
        {
            ClearWindow();
            TChangePwd.Visibility=Visibility.Visible;
            var oldpwd = this.TOldPwd.Password.Trim();
            var newpwd = this.TNewPwd.Password.Trim();
            var conpwd = this.TConPwd.Password.Trim();
            if (newpwd != conpwd) MessageBox.Show("两次输入的新密码不相同");
            else
            {
                using (var context = new EvalonEntities())
                {
                    var teacher = (from t in context.登录信息表 where t.用户名 == _teacherid select t).FirstOrDefault();
                    if (teacher.密码 != oldpwd)
                    {
                        MessageBox.Show("原密码不正确");
                    }
                    else
                    {
                        teacher.密码 = newpwd;
                        context.SaveChanges();
                        MessageBox.Show("密码修改成功");
                    }
                }
            }
        }
        #endregion


        #region 显示教师的个人信息
        private void TPersonInfo_click(object sender, RoutedEventArgs e)
        {
            ClearWindow();
            TPersonInfo.Visibility = Visibility.Visible;
            TName.Text = "姓名:";
            TTID.Text = "工号:";
            TSex.Text = "性别:";
            TTitle.Text = "职称:";

            using (var context = new EvalonEntities())
            {
                var me = (from t in context.教师信息表 where t.工号 == _teacherid select t).FirstOrDefault();
                TName.Text += me.姓名;
                TTID.Text += me.工号;
                TSex.Text += me.性别;
                TTitle.Text += me.职称;
            }
        }
        #endregion


        #region 查看教师授课情况
        private void TCourseReview_click(object sender, RoutedEventArgs e)
        {
            ClearWindow();
            TTeachCourseView.Visibility = Visibility.Visible;
            using (EvalonEntities context = new EvalonEntities())
            {
                var myteachcourse = (from c in context.任课信息表 where c.工号 == _teacherid select new { c.课程信息表.课程号,c.课程信息表.课程名称,c.课程信息表.上课地点,c.课程信息表.上课时间, c.课程信息表.学分, c.课程信息表.学时, c.课程信息表.预定人数, c.课程信息表.已选人数}).ToList();
                teachCourseData.ItemsSource = myteachcourse;
            }

        }

        #endregion



        #region 查看我的授课课程表
        private void TCourseViewBtn_click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

    }
}
