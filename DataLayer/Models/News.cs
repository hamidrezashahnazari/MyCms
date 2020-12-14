using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer
{
    public class News
    {
        [Key]
        public int NewsID { get; set; }

        [Display(Name = "گروه خبری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryID { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250)]
        public string Tittle { get; set; }

        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350)]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "متن خبر")]
        [AllowHtml]
        public string Text { get; set; }

        [Display(Name = "کلمات کلیدی")]
        public string Tags { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نویسنده")]
        [MaxLength(150)]
        public string AuthorName { get; set; }

        [Display(Name = "بازدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Visit { get; set; }

        [Display(Name = "تصویر خبر")]
        public string ImageName { get; set; }
        [Display(Name = "اسلایدر")]
        public bool ShowInSlider { get; set; }
        [Display(Name = "تاریخ بارگزاری")]
        public DateTime UploadDate { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<NewsComment> NewsComment { get; set; }

        public News()
        {

        }

    }
}
