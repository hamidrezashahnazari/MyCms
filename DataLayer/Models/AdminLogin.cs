using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class AdminLogin
    {
        [Key]
        public int LoginID { get; set; }

        [Display(Name = "نام کاربری")] [Required(ErrorMessage = "لطفا {0} وارد کنید")] [MaxLength(100)]
        public string UserName { get; set; }


        [Display(Name = "پست الکترونیک")] [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            ErrorMessage ="لطفا ایمیل خود را درست وارد کنید")]
        [MaxLength(250)]
        public string Email { get; set; }

        [Display(Name ="رمز عبور")][Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",ErrorMessage ="لطفا رمز عبور خود را به درستی واردکنید")]
        [MaxLength(250)]
        public string Password { get; set; }
    }
}
