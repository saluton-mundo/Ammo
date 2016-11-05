using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class JournalIndex : BaseEntity
    {
        public int JournalId { get; set; }
        public Guid OwnerId { get; set; }
        public BulletCollection Bullets { get; set; }
        public IEnumerable<JournalTag> Tags { get; set; }
        public IEnumerable<JournalBookmark> Bookmarks { get; set; }
    }
}
