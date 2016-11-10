using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class ActivityLogActivity : BaseEntity
    {
        public int ActivityLogId { get; set; }
        public int ActivityId { get; set; }
        public string Description { get; set; }
    }
}
