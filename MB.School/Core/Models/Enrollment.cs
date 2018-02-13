using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MB.School.Application.EnumType;

namespace MB.School.Core.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }

        public CourseGrade? Grade { get; set; }
        public int CourseId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Course Course { get; set; }
    }
}
