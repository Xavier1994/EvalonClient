using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalonClient.Lib
{

    //该类为静态类，提供一些静态方法来检测所选课程是否和已选课程存在冲突
    public  static  class CheckCourse
    {
        #region 检测所选课程是否和已选课程冲突
        public static bool CheckCourseSame(string courseid, string studentid)
        {
            using (var context = new EvalonEntities())
            {
                var mycourses = (from c in context.选课信息表 where c.学号 == studentid select c).ToList();
                return mycourses.Any(mycourse => mycourse.课程号 == courseid);
            }
        }
        #endregion

        #region 检测所选课程的时间是否和已选课程冲突
        public static bool CheckCourseTime(string courseid, string studentid)
        {
            using (var context = new EvalonEntities())
            {
                var mycourses = (from c in context.选课信息表 where c.学号 == studentid select c).ToList();
                var coursetime = (from c in context.课程信息表 where c.课程号 == courseid select c.上课时间).FirstOrDefault();

                return mycourses.Any(i => i.课程信息表.上课时间 == coursetime);

                //return mycourses.All(mc => mc.课程信息表.上课时间 == coursetime);
            }
        }
        #endregion
    }
}
