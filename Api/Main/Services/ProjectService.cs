using System.Collections.Generic;
using System.Linq;
using Catalyst.Api.Main.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalyst.Api.Main.Services
{
    public class ProjectService : IService<Project>
    {
        private readonly ProgramContext _programContext;

        public ProjectService(ProgramContext programContext) => _programContext = programContext;

        public IEnumerable<Project> FetchAll()
        {
            return _programContext.Projects.Include(project => project.Languages).ToList();
        }

        public Project FetchSingle(long id)
        {
            return _programContext.Projects
                .Include(project => project.Languages)
                .Where(project => project.Id == id)
                .FirstOrDefault();
        }

        public Project Create(Project project)
        {
            _programContext.Projects.Add(project);
            _programContext.SaveChanges();

            return project;
        }

        public void Update(Project record)
        {
            if (!Exists(record.Id))
            {
                throw new KeyNotFoundException("Project not found.");
            }

            _programContext.Update(record);
            _programContext.SaveChanges();
        }

        public void Remove(long id)
        {
            Remove(new Project() { Id = id });
        }

        public void Remove(Project record)
        {
            if (!Exists(record.Id))
            {
                throw new KeyNotFoundException("Project not found.");
            }

            _programContext.Projects.Remove(record);
            _programContext.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _programContext.Projects.Find(id) != null;
        }
    }
}