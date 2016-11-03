using Ammo.Domain.Entities;
using Ammo.Domain.Repositories.Abstract;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Repositories.Concrete
{
    public class SubscriptionRepository : BaseRepository, ISubscriptionRepository
    {
        private string _connection;

        public SubscriptionRepository(string connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Returns a collection of all Subscription types or individually by Id
        /// </summary>
        /// <param name="SubscriptionId"></param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<Subscription> Get(int? SubscriptionId)
        {
            using (var connection = new SqlConnection(_connection))
            {
                return connection.Query<Subscription>("spSubscriptionGet", new { @SUBSCRIPTIONID = SubscriptionId }, null, true, null, CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Adds a new class of subscription to the Ammo application
        /// </summary>
        /// <param name="Subscription"></param>
        /// <param name="SessionUserId"></param>
        /// <returns></returns>
        public int Add(Subscription Subscription, Guid SessionUserId)
        {
            using (var connection = new SqlConnection(_connection))
            {
                return 
                   connection.Execute(
                   "spSubscriptionCreate",
                   new
                   {
                        @SUBSCRIPTIONID = Subscription.SubscriptionId,
                        @NAME = Subscription.Name,
                        @PRICE = Subscription.Price
                    },
                    null,
                    null,
                    CommandType.StoredProcedure
                 );
            }
        }

        /// <summary>
        /// Logical deletion of Subscription class
        /// </summary>
        /// <param name="SubscriptionId"></param>
        /// <param name="SessionUserId"></param>
        /// <returns></returns>
        public int Delete(int SubscriptionId, Guid SessionUserId)
        {
            using (var connection = new SqlConnection(_connection))
            {
                return
                      connection.Execute(
                   "spSubscriptionDelete",
                   new
                   {
                      @SUBSCRIPTIONID = SubscriptionId,
                      @SESSIONUSERID = SessionUserId
                   },
                    null,
                    null,
                    CommandType.StoredProcedure
                 );
            }
        }
    }
}
