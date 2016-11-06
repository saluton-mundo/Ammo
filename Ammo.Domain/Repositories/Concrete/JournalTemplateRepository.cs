using Ammo.Domain.Repositories.Abstract;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ammo.Domain.Entities;
using System.Data.SqlClient;

namespace Ammo.Domain.Repositories.Concrete
{
    public class JournalTemplateRepository : BaseRepository, IJournalTemplateRepository
    {
        public int AddUpdate(JournalTemplate Template, string SessionUserId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spJournalTemplateAddUpdate",
                                                                     new
                                                                     {
                                                                         @JOURNALTEMPLATEID = Template.TemplateId,
                                                                         @NAME = Template.Name,
                                                                         @TEMPLATEURI = Template.TemplateUri,
                                                                         @ISPREMIUM = Template.IsPremium,
                                                                         @PRICE = Template.Price,
                                                                         @SESSIONUSERID = SessionUserId
                                                                     },
                                                                     null,
                                                                     null,
                                                                     CommandType.StoredProcedure)).Result;
            }
        }

        public int Delete(int JournalTemplateId, string SessionUserId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spJournalTemplateDelete",
                                                                   new
                                                                   {
                                                                       @JOURNALTEMPLATEID = JournalTemplateId,
                                                                       @SESSIONUSERID = SessionUserId
                                                                   },
                                                                   null,
                                                                   null,
                                                                   CommandType.StoredProcedure)).Result;
            }
        }

        public IEnumerable<JournalTemplate> Get(int? JournalTemplateId, bool? PremiumFlag)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.Query<JournalTemplate>("spJournalTemplateGet", 
                                                                         new
                                                                         {
                                                                             @JOURNALTEMPLATEID = JournalTemplateId,
                                                                             @PREMIUMFLAG = PremiumFlag
                                                                         }, 
                                                                         null,
                                                                         true,
                                                                         null,
                                                                         CommandType.StoredProcedure)).Result;
            }
        }
    }
}
