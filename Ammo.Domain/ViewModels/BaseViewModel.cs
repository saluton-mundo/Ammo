using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.ViewModels
{
    public class BaseViewModel
    {
        public int JournalId { get; set; }
        public Guid PageGUID { get; set; }
    }
}
