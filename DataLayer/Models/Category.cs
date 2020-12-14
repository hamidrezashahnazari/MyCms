using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان گروه")]
        [MaxLength(150)]
        public string CatTittle { get; set; }
        public virtual ICollection<News> News { get; set; }
        public Category()
        {

        }
    }
}
