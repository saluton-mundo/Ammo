using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class JournalPage : BaseEntity
    {
        public int JournalId { get; set; }
        public int PageId { get; set; }
        public int SortOrder { get; set; }
        public string Title { get; set; }
        public DateTime LastUpdated { get; set; }
        public IEnumerable<JournalTag> Tags { get; set; }
        public JournalPageTemplate Template { get; set; }
    }
}
