using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalonClient.Lib
{
    public enum Weekday
    {
        星期一,
        星期二,
        星期三,
        星期四,
        星期五,
        星期六,
        星期日
    }

    class CourseTime
    {
       

        public Weekday Day { set; get; }
        public int BeginCourse { set; get; }
        public int EndCourse { set; get; }

        public CourseTime(string coursetime)
        {
            var day = coursetime.Substring(0, 2);
            switch (day)
            {
                case "周一":
                    Day = Weekday.星期一;
                    break;
                case "周二":
                    Day = Weekday.星期二;
                    break;
                case "周三":
                    Day = Weekday.星期三;
                    break;
                case "周四":
                    Day = Weekday.星期四;
                    break;
                case "周五":
                    Day = Weekday.星期五;
                    break;
                case "周六":
                    Day = Weekday.星期六;
                    break;
                case "周日":
                    Day = Weekday.星期日;
                    break;
            }
            int i ;
            for (i=0; i <= coursetime.Length; i++)
            {
                if (coursetime[i] == '-')
                {
                    break;
                }
            }
            if (i == 3)
            {
                BeginCourse = int.Parse(coursetime[2].ToString());
                EndCourse = int.Parse(coursetime.Substring(4));
            }
            else
            {
                BeginCourse = int.Parse(coursetime.Substring(2,2));
                EndCourse = int.Parse(coursetime.Substring(5));
            }
        }

    }
}
