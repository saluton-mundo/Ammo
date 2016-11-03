using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class JournalBookmark : BaseEntity
    {
        public int BookmarkId { get; set; }
        public int JournalId { get; set; }
        public int PageId { get; set; }
    }
}
