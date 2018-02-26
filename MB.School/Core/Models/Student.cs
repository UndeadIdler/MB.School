using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MB.School.Core.Models
{
    /// <summary>
    /// 学生
    /// </summary>
    public class Student
    {
        public int StudentId { get; set; }

        [DisplayName("学生姓名")]
        public string RealName { get; set; }

        [DisplayName("注册时间")]
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
