using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
using EvalonClient.Lib;



namespace EvalonClient
{
    /// <summary>
    /// Course.xaml 的交互逻辑
    /// </summary>
    public partial class CourseWindow : Window
    {

        private readonly string _studentid;

        public CourseWindow(string studentid)
        {
            _studentid = studentid;
            InitializeComponent();
        }

        private void Course_OnLoaded(object sender, RoutedEventArgs e)
        {
            InitialContent();
        }


        private void InitialContent()
        {
            using (var context = new EvalonEntities())
            {
                var mycourse =
                    (from mc in context.选课信息表 where mc.学号 == _studentid select new { mc.课程信息表.课程名称, mc.课程信息表.上课时间 })
                        .ToList();
                foreach (var mc in mycourse)
                {
                    var ct = new CourseTime(mc.上课时间);

                    for (var i = ct.BeginCourse; i <= ct.EndCourse; i++)
                    {
                        var t = new TextBlock();
                        t.Text = mc.课程名称;
                        t.TextAlignment = TextAlignment.Center;
                        Grid.SetColumn(t, (int)ct.Day);
                        Grid.SetRow(t, i);
                        CourseTableView.Children.Add(t);
                    }
                }
            }
        }

        private void RefleshClick(object sender, RoutedEventArgs e)
        {
            InitialContent();
        }

    }
}
