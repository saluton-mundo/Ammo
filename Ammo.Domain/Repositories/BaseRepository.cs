using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Repositories
{
    public class BaseRepository
    {
        public string ConnectionString {
            get
            {
                return ConfigurationManager.ConnectionStrings["AmmoDbConnection"].ConnectionString;
            }
            set
            {
                this.ConnectionString = value;
            }
        } 
    }
}
