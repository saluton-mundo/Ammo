using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Services.Abstract
{
    public interface IJournalService
    {
        IEnumerable<Journal> Get(int? JournalId, string User);
        int AddUpdate(Journal Journal, string UserId);
        bool AddUpdate(IEnumerable<Journal> Journals, string UserId);
        int Delete(int JournalId, string UserId);
    }
}
