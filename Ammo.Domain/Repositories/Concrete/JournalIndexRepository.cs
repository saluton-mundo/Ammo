using Ammo.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ammo.Domain.Entities;

namespace Ammo.Domain.Repositories.Concrete
{
    public class JournalIndexRepository : BaseRepository, IJournalIndexRepository
    {
        public JournalIndex Get(int JournalId)
        {
            throw new NotImplementedException();
        }
    }
}
