using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainBookingSystemSem3Remake.Models.ViewModel
{
    public class UserViewModel
    {
        [DisplayName("Tên tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập Tên tài khoản")]
        public string UserName { get; set; }

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài tối thiểu là 6, lớn nhất là 20")]
        public string Password { get; set; }
    }
}