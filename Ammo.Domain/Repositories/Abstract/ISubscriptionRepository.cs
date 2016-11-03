using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Repositories.Abstract
{
    public interface ISubscriptionRepository
    {
        IEnumerable<Subscription> Get(int? SubscriptionId);
        int Add(Subscription Subscription, Guid SessionUserId);
        int Delete(int SubscriptionId, Guid SessionUserId);
    }
}
