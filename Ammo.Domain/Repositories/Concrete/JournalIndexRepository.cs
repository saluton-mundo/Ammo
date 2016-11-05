using Ammo.Domain.Repositories.Abstract;
using System.Linq;
using Ammo.Domain.Entities;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using static Dapper.SqlMapper;

namespace Ammo.Domain.Repositories.Concrete
{
    public class JournalIndexRepository : BaseRepository, IJournalIndexRepository
    {
        /// <summary>
        /// Retrieves the Journal Index page data 
        ///     -- Bullets 
        ///     -- Tags
        ///     -- Bookmarks
        /// </summary>
        /// <param name="JournalId"></param>
        /// <returns></returns>
        public JournalIndex Get(int JournalId)
        {
            JournalIndex index;

            using (var connection = new SqlConnection(base.ConnectionString))
            {
                using (GridReader reader = connection.QueryMultiple("spJournalIndexGet", new { @JOURNALID = JournalId }, null, null, CommandType.StoredProcedure))
                {
                    index = reader.Read<JournalIndex>().SingleOrDefault();

                    index.Bookmarks = reader.Read<JournalBookmark>().ToList();

                    index.Bullets = reader.Read<BulletCollection>().SingleOrDefault();

                    index.Tags = reader.Read<JournalTag>().ToList();
                }
            }

            return index;
        }
    }
}
