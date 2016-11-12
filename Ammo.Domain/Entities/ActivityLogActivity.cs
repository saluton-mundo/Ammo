using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class ActivityLogActivity : BaseEntity
    {
        public int ActivityLogId { get; set; }
        public int ActivityId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Activities must have a minimum of 2 characters and maximum of 75")]
        public string Description { get; set; }
    }
}
