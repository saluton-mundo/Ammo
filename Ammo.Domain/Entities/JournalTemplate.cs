using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class JournalTemplate : BaseEntity
    {
        public int TemplateId { get; set; }
        public string Name { get; set; }
        public string TemplateUri { get; set; }
        public bool IsPremium { get; set; }
        public float Price { get; set; }
        public Guid CreateUserId { get; set; }
    }
}
