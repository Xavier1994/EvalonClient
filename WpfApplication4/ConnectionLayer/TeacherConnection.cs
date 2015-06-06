using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace EvalonClient.ConnectionLayer
{
    //构建教师类
    class Teacher
    {
        string TeacherID { get; set; }
        string Name { get; set; }
        string Sexuality { get; set; }
        string Position { get; set; }
    }

    class TeacherConnection
    {

    }
}
