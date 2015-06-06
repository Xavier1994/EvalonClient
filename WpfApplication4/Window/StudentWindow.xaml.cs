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
using System.Windows.Threading;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using EvalonClient.Lib;


namespace EvalonClient
{
    /// <summary>
    /// SubWindow1.xaml 的交互逻辑
    /// </summary>
    public partial class StudentWindow : Window
    {

        private string StudentID;   //登陆账户的ID
        //private DispatcherTimer ShowTimer;  //作为时间显示
        private StudentConnection Scon;   //使用对象来管理数据库的连接
        private LoginConnection Lcon;   
        private Student student;  //当前登陆的学生的信息


        public StudentWindow(string studentid)
        {  
            Scon=new StudentConnection();
            Lcon = new LoginConnection();
            InitializeComponent();
            StudentID = studentid;
        }



        //每隔1秒刷新一次
        public void ShowTime(object sender, EventArgs e)
        {
            this.TM.Text = "";
            //获得年月日 
            this.TM.Text += DateTime.Now.ToString("yyyy年MM月dd日");   //yyyy年MM月dd日 

            //获得当前的时分秒
            //this.TM.Text += " ";
            //this.TM.Text += DateTime.Now.ToString("HH:mm:ss:ms");
        
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

  
        private void buttonreturn_Click(object sender, RoutedEventArgs e)
        {
            this.ChangePassword.Visibility = System.Windows.Visibility.Visible;

        }

        private void buttonchangepassword_Click(object sender, RoutedEventArgs e)
        {
            ClearWindow();
            this.ChangePassword.Visibility = System.Windows.Visibility.Visible;
        }

        private void buttonreflesh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonquit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonreflesh_Click_1(object sender, RoutedEventArgs e)
        {
            InitialContent();
        }

        private void buttonquit_Click_1(object sender, RoutedEventArgs e)
        {
            Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitialContent();

        }


        #region 初始化窗口内容
        //初始化窗口的内容
        private void InitialContent()
        {
            this.TM.Text = string.Empty;
            this.WK.Text = string.Empty;
            this.SN.Text = string.Empty;


            //显示当前时间
            this.TM.Text += DateTime.Now.ToString("yyyy年MM月dd日");   //yyyy年MM月dd日 

            //支持秒的显示，每秒钟更新
            /*
            ShowTimer = new System.Windows.Threading.DispatcherTimer();
            ShowTimer.Tick += new EventHandler(ShowTime);
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();
             * */

            //显示今天时星期几

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
            this.WK.Text += strWeek;

            //显示g学当前生的名字
            Scon.OpenConnection(ConfigurationManager.AppSettings["cnStr"]);
            student = Scon.FindStudent(StudentID);
            this.SN.Text += student.Name + "同学";
            Scon.CloseConnection();
        }
        #endregion


        #region  清理窗口
        private void ClearWindow()
        {
            PersonalInfo.Visibility = Visibility.Hidden;
            ChangePassword.Visibility = Visibility.Hidden;
            CourseInfo.Visibility = Visibility.Hidden;
            TrainingPlan.Visibility = Visibility.Hidden;
            MyTestView.Visibility=Visibility.Hidden;
        }
        #endregion



        #region 修改密码
        //确认修改密码
        private void ConfirmChange_Click(object sender, RoutedEventArgs e)
        {
            ClearWindow();
            string oldpwd = this.OldPwd.Password.Trim();
            string newpwd = this.NewPwd.Password.Trim();
            string conpwd = this.ConPwd.Password.Trim();
            if (newpwd != conpwd) MessageBox.Show("两次输入的新密码不相同");
            else
            {
                Lcon.OpenConnection(ConfigurationManager.AppSettings["cnStr"]);
                string pwd = Lcon.FindPwd(StudentID);
                if (pwd != oldpwd) MessageBox.Show("输入的密码有误");
                else
                {
                    Lcon.ChangePwd(StudentID, newpwd);
                    this.ChangePassword.Visibility = System.Windows.Visibility.Hidden;
                }
                Lcon.CloseConnection();
            }
            ChangePassword.Visibility = Visibility.Hidden;
           
        }
        #endregion
    

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        #region 显示个人信息
        private void PersonalInfoBtn(object sender, RoutedEventArgs e)
        {
            ClearWindow();

            Myname.Text = "姓名：";
            Myid.Text = "学号: ";
            Myage.Text = "年龄: ";
            Mymajor.Text = "专业：";
            Mycollege.Text = "学院：";
            Mysex.Text = "性别：";
            Myplace.Text = "籍贯：";
            Mynation.Text = "国籍：";

            PersonalInfo.Visibility = Visibility.Visible;
            Scon.OpenConnection(ConfigurationManager.AppSettings["cnStr"]);
            Student student = Scon.FindStudent(StudentID);
            Scon.CloseConnection();
            DepartmentConnection dcon = new DepartmentConnection();
            dcon.OpenConnection(ConfigurationManager.AppSettings["cnStr"]);
            Department department = dcon.FindDepartment(student.DepartmentID);

            Myname.Text += student.Name;
            Myid.Text += student.StudentID;
            Myage.Text += string.Format("{0}",student.Age);
            Mymajor.Text += department.DepartmentName;
            Mycollege.Text += department.CollegeName;
            Mysex.Text += student.Sexuality;
            Myplace.Text += student.OriginPlace;
            Mynation.Text += student.Nationality;
            
        }
        #endregion



        #region 显示课程信息
        private void CourseChooseBtn(object sender, RoutedEventArgs e)
        {
            ClearWindow();
            CourseInfo.Visibility = Visibility.Visible;

            List<ChoosedCourse> C = new List<ChoosedCourse>();       //课程信息
            List<MyCourse> MC=new List<MyCourse>();        //我选择的课程信息
            using (EvalonEntities context = new EvalonEntities())
            {
                var courses = (from c in context.课程信息表 select new { 选择 = false, c.课程号, c.课程名称, c.上课地点, c.上课时间, c.学分, c.学时,c.预定人数,c.已选人数 }).ToList();
                var mycourses = (from mc in context.选课信息表 select new { 选择 = false, mc.课程信息表.课程号, mc.课程信息表.课程名称, mc.课程信息表.上课地点,  mc.课程信息表.上课时间, 学分 = (int)mc.课程信息表.学分,学时= (int)mc.课程信息表.学时 }).ToList();
             
                MC.AddRange(mycourses.Select(c => new MyCourse() { 选择 = c.选择, 课程号 = c.课程号, 课程名称 = c.课程名称, 学分 = c.学分, 学时 = c.学时, 上课地点 = c.上课地点, 上课时间 = c.上课时间 }));
                C.AddRange(courses.Select(c => new ChoosedCourse() {选择 = c.选择, 课程号 = c.课程号, 课程名称 = c.课程名称, 学分 = (int) c.学分, 学时 = (int) c.学时, 上课地点 = c.上课地点, 上课时间 = c.上课时间,预定人数=c.预定人数, 选课人数= c.已选人数}));
                dataGrid.ItemsSource = C;
                dataGrid1.ItemsSource = MC;
            }
        }
        #endregion

        #region  显示培养计划
        private void TrainingPlanBtn(object sender,RoutedEventArgs e)
        {
            ClearWindow();
            TrainingPlan.Visibility = Visibility.Visible;
            using(var context = new EvalonEntities())
            {
                int departmentid =(int)((from s in context.学生信息表 where s.学号 == StudentID select s.系号).FirstOrDefault());
                var trainingplan = (from t in context.培养计划表 orderby t.学期 where t.系号 == departmentid select new {t.课程信息表.课程名称,t.系号,t.院系信息表.系名,t.院系信息表.院名 ,t.学期}).ToList();
                PlanTable.ItemsSource = trainingplan;
            }
        }
        #endregion

        #region 确认选课
        //选课确认提交按钮触发的事件
        private void courseconfirm_Click(object sender, RoutedEventArgs e)
        {
            var courses = dataGrid.Items;
            var choosedCourseId = (from object i in courses where ((ChoosedCourse) i).选择  select ((ChoosedCourse) i).课程号).ToList();
            using (var context = new EvalonEntities())
            {
                foreach (var id in choosedCourseId)
                {
                    var mc = (from c in context.课程信息表 where c.课程号 == id select c).FirstOrDefault();
                    if (CheckCourse.CheckCourseSame(id, StudentID))
                    {
                         MessageBox.Show("你已经选了该课程");
                    }
                    else if (CheckCourse.CheckCourseTime(id, StudentID))
                    {
                        MessageBox.Show("当前课程的时间与所选课程的时间存在冲突");
                    }
                    else if(mc.已选人数 >= mc.预定人数)
                    {
                        MessageBox.Show("选课人数已达到上限");
                    }
                    else
                    {
                        context.选课信息表.Add(new 选课信息表 { 学号 = StudentID, 课程号 = id });
                        (from c in context.课程信息表 where c.课程号 == id select c).FirstOrDefault().已选人数 += 1;
                        context.SaveChanges();
                        MessageBox.Show("选课成功，刷新页面查看选课结果");
                    }
                }
            }
        }
        #endregion

        #region 确认退课
        private void coursedelete_click(object sender, RoutedEventArgs e)
        {
            var courses = dataGrid1.Items;
            var choosedCourseId = (from object i in courses where ((MyCourse)i).选择 select ((MyCourse)i).课程号).ToList();
            using (var context = new EvalonEntities())
            {
                foreach (var mycourse in choosedCourseId.Select(mc => (from myc in context.选课信息表 where myc.课程号 == mc select myc).FirstOrDefault()))
                {
                    context.选课信息表.Remove(mycourse);
                    (from c in context.课程信息表 where c.课程号 == mycourse.课程号 select c).FirstOrDefault().已选人数 -= 1;
                    context.SaveChanges();
                }
                MessageBox.Show("已成功退课,刷新页面查看退课结果");
            }
        }
        #endregion

        #region 显示我的课表
        private void ShowMyCourseTable(object sender, RoutedEventArgs e)
        {
            var mycoursetable= new CourseWindow(StudentID);
            mycoursetable.Show();
        }
        #endregion


        #region 查看考试信息
        private void testViewClick(object sender, RoutedEventArgs e)
        {
            ClearWindow();
            MyTestView.Visibility = Visibility.Visible;
            using (EvalonEntities context = new EvalonEntities())
            {
                var mycourses = (from c in context.选课信息表 where c.学号 == StudentID select c.课程号).ToList();
                var mytest = (from t in context.考试信息表 where mycourses.Contains(t.课程号) select t).ToList();
                testGrid.ItemsSource = mytest;
            }
        }
        #endregion
    }
}
