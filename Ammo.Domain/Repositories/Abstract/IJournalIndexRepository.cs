﻿using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Repositories.Abstract
{
    public interface IJournalIndexRepository
    {
        JournalIndex Get(int JournalId);
    }
}
