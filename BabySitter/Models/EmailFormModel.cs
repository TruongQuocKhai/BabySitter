using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabySitter.Models
{
    public class EmailFormModel
    {
        [Required, Display(Name = "Họ và Tên")]
        public string FromName { get; set; }
        [Required, Display(Name = "E-mail"), EmailAddress]
        public string FromEmail { get; set; }
        [Required, Display(Name = "Tiêu Đề")]
        public string Subject { get; set; }
        [Required, Display(Name = "Số Điện Thoại")]
        public string NumberPhone { get; set; }
        [Required]
        public string Message { get; set; }
    }
}