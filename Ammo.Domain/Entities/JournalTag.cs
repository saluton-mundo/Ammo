using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class JournalTag : BaseEntity
    {
        public int JournalId { get; set; }
        public int TagId { get; set; }
        public string TagContent { get; set; }
    }
}
