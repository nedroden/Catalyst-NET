using Catalyst.Api.Main.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Catalyst.Api.Main.Services
{
    public class LanguageService : IService<Language>
    {
        private readonly ProgramContext _programContext;

        public LanguageService(ProgramContext programContext) => _programContext = programContext;

        public IEnumerable<Language> FetchAll()
        {
            return _programContext.Languages.Include(language => language.Project).ToList();
        }

        public Language FetchSingle(long id)
        {
            return _programContext.Languages
                .Where(language => language.Id == id)
                .Include(language => language.Project)
                .FirstOrDefault();
        }

        public Language Create(Language language)
        {
            _programContext.Languages.Add(language);
            _programContext.SaveChanges();

            return language;
        }

        public void Update(Language record)
        {
            if (!Exists(record.Id))
            {
                throw new KeyNotFoundException("Language not found.");
            }

            _programContext.Languages.Update(record);
            _programContext.SaveChanges();
        }

        public void Remove(long id)
        {
            Remove(new Language() { Id = id });
        }

        public void Remove(Language record)
        {
            if (!Exists(record.Id))
            {
                throw new KeyNotFoundException("Language not found.");
            }

            _programContext.Languages.Remove(record);
            _programContext.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _programContext.Languages.Find(id) != null;
        }
    }
}