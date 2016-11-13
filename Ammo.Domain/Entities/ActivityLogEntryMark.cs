using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class ActivityLogEntryMark : BaseEntity
    {
        public int ActivityEntryLogMarkId { get; set; }
        public int ActivityLogId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Descriptions must be less than 100 characters long...")]
        public string Description { get; set; }
        [Required]
        [StringLength(7, ErrorMessage = "Selection must be a valid HEX color", MinimumLength = 7)]
        public string Color { get; set; }
    }
}
