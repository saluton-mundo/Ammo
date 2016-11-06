﻿using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Services.Abstract
{
    public interface IJournalTemplateService
    {
        IEnumerable<JournalTemplate> Get(int? JournalTemplateId, bool? PremiumFlag);
        int AddUpdate(JournalTemplate Template, string SessionUserId);
        bool AddUpdate(IEnumerable<JournalTemplate> Templates, string SessionUserId);
        int Delete(int JournalTemplateId, string SessionUserId);
    }
}
