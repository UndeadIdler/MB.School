using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MB.School.Application.ViewModels
{
    public class EnrollmentDateGroup
    {
        /// <summary>
        /// 学生数量
        /// </summary>
        [DisplayName("学生数量")]
        public int StudentCount { get; set; }

        /// <summary>
        /// 注册日期
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayName("注册日期")]
        public DateTime? EnrollmentDate { get; set; }

    }
}