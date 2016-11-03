using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Repositories.Abstract
{
    public interface IJournalRepository
    {
        IEnumerable<Journal> Get(int? JournalId, string User);
        int AddUpdate(Journal Journal, string UserId);
        bool AddUpdate(IEnumerable<Journal> Journals, string UserId);
        int Delete(int JournalId, Guid SessionUserId);
    }
}
