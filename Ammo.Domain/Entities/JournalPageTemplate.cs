using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class JournalPageTemplate : BaseEntity
    {
        public int PageTemplateId { get; set; }
        public string FileName { get; set; }
        public BaseEntity PageModel { get; set; }
    }
}
