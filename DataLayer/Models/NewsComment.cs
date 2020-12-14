using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class NewsComment
    {
        [Key]
        public int CommentID { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "کد خبر")]
        public int NewsID { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام")]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "ایمیل")]
        [MaxLength(350)]
        public string Email { get; set; }
        [Display(Name = "وبسایت")]
        [MaxLength(200)]
        public string Website { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نظر")]
        public string Comment { get; set; }
        [Display(Name ="تاریخ ثبت")]
        public DateTime SubmitDate { get; set; }

        public virtual News News { get; set; }
        public NewsComment()
        {

        }
    }


}
