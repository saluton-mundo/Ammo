using Ammo.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ammo.Domain.Entities;
using Ammo.Domain.Repositories.Abstract;

namespace Ammo.Domain.Services.Concrete
{
    public class JournalIndexService : BaseService, IJournalIndexService
    {
        IJournalIndexRepository _indexRepository;

        public JournalIndexService(IJournalIndexRepository indexRepository)
        {
            _indexRepository = indexRepository;
        }

        public JournalIndex Get(int JournalId, string SessionUserId)
        {
            JournalIndex index = _indexRepository.Get(JournalId);

            if(index.OwnerId.ToUpper() == SessionUserId.ToUpper())
            {
                return index;
            }

            return null;
        }
    }
}
