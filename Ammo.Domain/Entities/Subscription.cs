using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class Subscription : BaseEntity
    {
        public int SubscriptionId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
