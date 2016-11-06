using Ammo.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ammo.Domain.Entities;
using Ammo.Domain.Repositories.Abstract;

namespace Ammo.Domain.Services.Concrete
{
    public class JournalTemplateService : BaseService, IJournalTemplateService
    {
        IJournalTemplateRepository _templateRepository;
        IBulletRepository _bulletRepository;

        public JournalTemplateService(IBulletRepository bulletRepository,
                                      IJournalTemplateRepository templateRepository)
        {
            _bulletRepository = bulletRepository;
            _templateRepository = templateRepository;
        }

        public bool AddUpdate(IEnumerable<JournalTemplate> Templates, string SessionUserId)
        {
            if(Templates != null && Templates.Count() > 0 && string.IsNullOrEmpty(SessionUserId) == false)
            {
                foreach(JournalTemplate template in Templates)
                {
                    AddUpdate(template, SessionUserId);
                }

                return true;
            }

            return false;
        }

        public int AddUpdate(JournalTemplate Template, string SessionUserId)
        {
            if(Template == null || string.IsNullOrEmpty(SessionUserId))
            {
                return 0;
            }

            return _templateRepository.AddUpdate(Template, SessionUserId);
        }

        public int Delete(int JournalTemplateId, string SessionUserId)
        {
            // TODO Belt and brace check if SessionUser is an Admin only allow deletion if they are Admin or the Creator of this JournalTemplate
            if(JournalTemplateId == 0 || string.IsNullOrEmpty(SessionUserId))
            {
                return 0;
            }

            return _templateRepository.Delete(JournalTemplateId, SessionUserId);
        }

        public IEnumerable<JournalTemplate> Get(int? JournalTemplateId, bool? PremiumFlag)
        {
            IEnumerable<JournalTemplate> templates = _templateRepository.Get(JournalTemplateId, PremiumFlag);

            foreach(JournalTemplate template in templates)
            {
                if(template.TemplateBulletCollection != null && template.TemplateBulletCollection.BulletCollectionId != 0)
                {
                    template.TemplateBulletCollection.Bullets = _bulletRepository.GetByCollection(template.TemplateBulletCollection.BulletCollectionId);
                }
            }

            return templates;
        }
    }
}
