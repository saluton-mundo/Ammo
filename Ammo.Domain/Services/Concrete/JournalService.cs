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
    public class JournalService : BaseService, IJournalService
    {
        private IJournalRepository _journalRepository;

        public JournalService(IJournalRepository journalRepository)
        {
            _journalRepository = journalRepository;
        }

        /// <summary>
        /// Overload to cater for multiple operations
        /// </summary>
        /// <param name="Journals"></param>
        /// <returns></returns>
        public bool AddUpdate(IEnumerable<Journal> Journals, string UserId)
        {
            if(Journals == null || Journals.Count() < 1)
            {
                return false;
            }

            if(string.IsNullOrEmpty(UserId))
            {
                return false;
            }

            foreach(Journal journal in Journals)
            {
                AddUpdate(journal, UserId);
            }

            return true;
        }

        /// <summary>
        /// Single Add/Update journal operation
        /// </summary>
        /// <param name="Journal"></param>
        /// <returns></returns>
        public int AddUpdate(Journal Journal, string UserId)
        {
            if(string.IsNullOrEmpty(Journal.CoverUrl)) {
                Journal.CoverUrl = "blank-cover.png";
            }

            if(string.IsNullOrEmpty(UserId))
            {
                return 0;
            }

            return _journalRepository.AddUpdate(Journal, UserId);
        }

        /// <summary>
        /// Logical deletion of the specified Journal.
        /// If an invalid value is bound to JournalId don't allow the operation to continue
        /// </summary>
        /// <param name="JournalId"></param>
        /// <returns></returns>
        public int Delete(int JournalId, string UserId)
        {
            Guid SessionUserId;
            if(JournalId == 0)
            {
                return JournalId;
            }
            if(string.IsNullOrEmpty(UserId) == false)
            {
                Guid.TryParse(UserId, out SessionUserId);
            } else
            {
                SessionUserId = Guid.NewGuid();
            }

            return _journalRepository.Delete(JournalId, SessionUserId);
        }

        /// <summary>
        /// Calls rep and retrieves either a single Journal or a collection
        /// </summary>
        /// <param name="JournalId"></param>
        /// <returns></returns>
        public IEnumerable<Journal> Get(int? JournalId, string User)
        {
            // Get Journal
            return _journalRepository.Get(JournalId, User);
        }
    }
}
