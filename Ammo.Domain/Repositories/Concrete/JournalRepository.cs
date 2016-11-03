using Ammo.Domain.Repositories.Abstract;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ammo.Domain.Entities;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Ammo.Domain.Repositories.Concrete
{
    public class JournalRepository : BaseRepository, IJournalRepository
    {
        /// <summary>
        /// Constructor with global 
        /// </summary>
        /// <param name="connection"></param>
        public JournalRepository() : base() {}

        /// <summary>
        /// Overload for multiple insert / update operations
        /// </summary>
        /// <param name="Journals"></param>
        /// <returns></returns>
        public bool AddUpdate(IEnumerable<Journal> Journals, string UserId)
        {
            if(Journals == null || Journals.Count() < 1)
            {
                return false;
            }

            foreach(Journal journal in Journals)
            {
                AddUpdate(journal, UserId);
            }

            return true;
        }

        public int AddUpdate(Journal Journal, string UserId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.Execute(
                     "spJournalAddUpdate",
                     new
                     {
                         @JournalId = Journal.JournalId,
                         @OwnerId = Journal.OwnerId,
                         @Title = Journal.Title,
                         @Description = Journal.Description,
                         @CoverUrl = Journal.CoverUrl,
                         @UserId = UserId
                     },
                     null,
                     null,
                     System.Data.CommandType.StoredProcedure
                 )).Result;
            }
        }

        /// <summary>
        /// Carries out a logical deletion of a journal
        /// </summary>
        /// <param name="JournalId"></param>
        /// <returns></returns>
        public int Delete(int JournalId, Guid SessionUserId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.Execute(
                    "spJournalDelete",
                    new
                    {
                        @JournalId = JournalId,
                        @SessionUserId = SessionUserId
                    },
                    null, 
                    null,
                    System.Data.CommandType.StoredProcedure
                )).Result;
            }
        }


        /// <summary>
        /// Retrieves all Journals if null supplied 
        /// Else return a collection with a single journal
        /// </summary>
        /// <param name="JournalId"></param>
        /// <returns></returns>
        public IEnumerable<Journal> Get(int? JournalId, string User)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.Query<Journal>("spJournalGet", 
                                                                 new
                                                                 {
                                                                     @JournalId = JournalId,
                                                                     @UserName = User
                                                                 }, 
                                                                 null, 
                                                                 true, 
                                                                 null, 
                                                                 CommandType.StoredProcedure)).Result;
            }
        }
    }
}
