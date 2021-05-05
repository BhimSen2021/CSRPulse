using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CSRPulse.Model
{
    public class Menu:BaseModel
    {
        public int MenuId { get; set; }
        [Display(Name = "Menu Name :")]
        [Required(ErrorMessage = "Menu Name is required.")]
        public string MenuName { get; set; }
        [Display(Name = "Parent :")]
        public Nullable<int> ParentMenuId { get; set; }
        [Display(Name = "Sequence :")]
        public Nullable<int> SequenceNo { get; set; }
        [Display(Name = "URL :")]
        [Required(ErrorMessage = "URL is required.")]
        public string Url { get; set; }

        [Display(Name = "Icon Class :")]
        public string IconClass { get; set; }

        [Display(Name = "Active :")]
        public bool IsActive { get; set; }

        public string Area { get; set; }

        public string Help { get; set; }

    }
}
