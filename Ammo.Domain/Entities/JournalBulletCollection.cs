using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class JournalBulletCollection : BaseEntity
    {
        public int JournalId { get; set; }

        public int SelectedBulletCollectionId { get; set; }

        public IEnumerable<BulletCollection> Collections { get; set; }
    }
}
