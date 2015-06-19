namespace EvalonClient.Window
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Windows;

    using EvalonClient.Lib;

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
            this.Scon=new StudentConnection();
            this.Lcon = new LoginConnection();
            InitializeComponent();
            this.StudentID = studentid;
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


  
        private void buttonreturn_Click(object sender, RoutedEventArgs e)
        {

        }

        
        #region 修改密码按钮 
        private void buttonchangepassword_Click(object sender, RoutedEventArgs e)
        {
            this.ClearWindow();
            this.ChangePassword.Visibility = Visibility.Visible;
        }
        #endregion

      

        
        #region 窗口加载
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.InitialContent();

        }
        #endregion


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
            this.Scon.OpenConnection(ConfigurationManager.AppSettings["cnStr"]);
            this.student = this.Scon.FindStudent(this.StudentID);
            this.SN.Text += this.student.Name + "同学";
            this.Scon.CloseConnection();
        }
        #endregion

        #region  清理窗口
        private void ClearWindow()
        {
            this.PersonalInfo.Visibility = Visibility.Hidden;
            this.ChangePassword.Visibility = Visibility.Hidden;
            this.CourseInfo.Visibility = Visibility.Hidden;
            this.TrainingPlan.Visibility = Visibility.Hidden;
            this.MyTestView.Visibility=Visibility.Hidden;
            this.SScoreSearchInfoView.Visibility = Visibility.Hidden;
        }
        #endregion

        #region 修改密码
        //确认修改密码
        private void ConfirmChange_Click(object sender, RoutedEventArgs e)
        {
            this.ClearWindow();
            string oldpwd = this.OldPwd.Password.Trim();
            string newpwd = this.NewPwd.Password.Trim();
            string conpwd = this.ConPwd.Password.Trim();
            if (newpwd != conpwd) MessageBox.Show("两次输入的新密码不相同");
            else
            {
                this.Lcon.OpenConnection(ConfigurationManager.AppSettings["cnStr"]);
                string pwd = this.Lcon.FindPwd(this.StudentID);
                if (pwd != oldpwd) MessageBox.Show("输入的密码有误");
                else
                {
                    this.Lcon.ChangePwd(this.StudentID, newpwd);
                    this.ChangePassword.Visibility = System.Windows.Visibility.Hidden;
                }
                this.Lcon.CloseConnection();
            }
            this.ChangePassword.Visibility = Visibility.Hidden;
           
        }
        #endregion
    
        #region 显示个人信息
        private void PersonalInfoBtn(object sender, RoutedEventArgs e)
        {
            this.ClearWindow();

            this.Myname.Text = "姓名：";
            this.Myid.Text = "学号: ";
            this.Myage.Text = "年龄: ";
            this.Mymajor.Text = "专业：";
            this.Mycollege.Text = "学院：";
            this.Mysex.Text = "性别：";
            this.Myplace.Text = "籍贯：";
            this.Mynation.Text = "国籍：";

            this.PersonalInfo.Visibility = Visibility.Visible;
            this.Scon.OpenConnection(ConfigurationManager.AppSettings["cnStr"]);
            Student student = this.Scon.FindStudent(this.StudentID);
            this.Scon.CloseConnection();
            DepartmentConnection dcon = new DepartmentConnection();
            dcon.OpenConnection(ConfigurationManager.AppSettings["cnStr"]);
            Department department = dcon.FindDepartment(student.DepartmentID);

            this.Myname.Text += student.Name;
            this.Myid.Text += student.StudentID;
            this.Myage.Text += string.Format("{0}",student.Age);
            this.Mymajor.Text += department.DepartmentName;
            this.Mycollege.Text += department.CollegeName;
            this.Mysex.Text += student.Sexuality;
            this.Myplace.Text += student.OriginPlace;
            this.Mynation.Text += student.Nationality;
            
        }
        #endregion

        #region 显示课程信息
        private void CourseChooseBtn(object sender, RoutedEventArgs e)
        {
            this.ClearWindow();
            this.CourseInfo.Visibility = Visibility.Visible;

            List<ChoosedCourse> C = new List<ChoosedCourse>();       //课程信息
            List<MyCourse> MC=new List<MyCourse>();        //我选择的课程信息
            using (EvalonEntities context = new EvalonEntities())
            {
                var courses = (from c in context.课程信息表 select new { 选择 = false, c.课程号, c.课程名称, c.上课地点, c.上课时间, c.学分, c.学时,c.预定人数,c.已选人数 }).ToList();
                var mycourses = (from mc in context.选课信息表 select new { 选择 = false, mc.课程信息表.课程号, mc.课程信息表.课程名称, mc.课程信息表.上课地点,  mc.课程信息表.上课时间, 学分 = (int)mc.课程信息表.学分,学时= (int)mc.课程信息表.学时 }).ToList();
             
                MC.AddRange(mycourses.Select(c => new MyCourse() { 选择 = c.选择, 课程号 = c.课程号, 课程名称 = c.课程名称, 学分 = c.学分, 学时 = c.学时, 上课地点 = c.上课地点, 上课时间 = c.上课时间 }));
                C.AddRange(courses.Select(c => new ChoosedCourse() {选择 = c.选择, 课程号 = c.课程号, 课程名称 = c.课程名称, 学分 = (int) c.学分, 学时 = (int) c.学时, 上课地点 = c.上课地点, 上课时间 = c.上课时间,预定人数=c.预定人数, 选课人数= c.已选人数}));
                this.dataGrid.ItemsSource = C;
                this.dataGrid1.ItemsSource = MC;
            }
        }
        #endregion

        #region  显示培养计划
        private void TrainingPlanBtn(object sender,RoutedEventArgs e)
        {
            this.ClearWindow();
            this.TrainingPlan.Visibility = Visibility.Visible;
            using(var context = new EvalonEntities())
            {
                int departmentid =(int)((from s in context.学生信息表 where s.学号 == this.StudentID select s.系号).FirstOrDefault());
                var trainingplan = (from t in context.培养计划表 orderby t.学期 where t.系号 == departmentid select new {t.课程信息表.课程名称,t.系号,t.院系信息表.系名,t.院系信息表.院名 ,t.学期}).ToList();
                this.PlanTable.ItemsSource = trainingplan;
            }
        }
        #endregion

        #region 确认选课
        //选课确认提交按钮触发的事件
        private void courseconfirm_Click(object sender, RoutedEventArgs e)
        {
            var courses = this.dataGrid.Items;
            var choosedCourseId = (from object i in courses where ((ChoosedCourse) i).选择  select ((ChoosedCourse) i).课程号).ToList();
            using (var context = new EvalonEntities())
            {
                foreach (var id in choosedCourseId)
                {
                    var mc = (from c in context.课程信息表 where c.课程号 == id select c).FirstOrDefault();
                    if (CheckCourse.CheckCourseSame(id, this.StudentID))
                    {
                         MessageBox.Show("你已经选了该课程");
                    }
                    else if (CheckCourse.CheckCourseTime(id, this.StudentID))
                    {
                        MessageBox.Show("当前课程的时间与所选课程的时间存在冲突");
                    }
                    else if(mc.已选人数 >= mc.预定人数)
                    {
                        MessageBox.Show("选课人数已达到上限");
                    }
                    else
                    {
                        context.选课信息表.Add(new 选课信息表 { 学号 = this.StudentID, 课程号 = id });
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
            var courses = this.dataGrid1.Items;
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
            var mycoursetable= new CourseWindow(this.StudentID);
            mycoursetable.Show();
        }
        #endregion

        #region 查看考试信息
        private void testViewClick(object sender, RoutedEventArgs e)
        {
            this.ClearWindow();
            this.MyTestView.Visibility = Visibility.Visible;
            using (EvalonEntities context = new EvalonEntities())
            {
                var mycourses = (from c in context.选课信息表 where c.学号 == this.StudentID select c.课程号).ToList();
                var mytest = (from t in context.考试信息表 where mycourses.Contains(t.课程号) select t).ToList();
                this.testGrid.ItemsSource = mytest;
            }
        }
        #endregion

        #region 退出按钮 
        private void buttonquit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion


        #region 显示成绩查询窗口
        private void ScoreSearchInfoViewbtnClick(object sender, RoutedEventArgs e)
        {
            this.ClearWindow();
            this.SScoreSearchInfoView.Visibility = Visibility.Visible;
            
        }
        #endregion 


        #region 查询成绩
        private void scoreSearchConfirmBtnClick(object sender, RoutedEventArgs e)
        {
            using (var context = new EvalonEntities())
            {
                var trainingplan =
                    (context.培养计划表.Where(t => t.学期 == this.ScoreSearchNumericBox.Value)
                        .Select(t => t.课程号))
                        .ToList();
 
                var scores = (context.选课信息表.Where(t => t.学号 == this.StudentID && trainingplan.Contains(t.课程号))
                        .Select(t => new { t.学号, t.课程号, t.课程信息表.课程名称, t.课程成绩 })).ToList();
                this.SScoreSearchGrid.ItemsSource = scores;
            }

        }
        #endregion

    }
}
