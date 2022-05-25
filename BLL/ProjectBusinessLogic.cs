
using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.BLL
{
    public class ProjectBusinessLogic
    {
        public IRepository<Project> projectRepo;
     

        public ProjectBusinessLogic(IRepository<Project> projectRepoArg)
        {
            projectRepo = projectRepoArg;
        }

        public void Add(Project project)
        {
            projectRepo.Add(project);
            projectRepo.Save();
        }

        public virtual Project? Get(int? id)
        {
            Project? project = projectRepo.Get(p => p.Id == id);
            return project;
        }
        public List<Project> GetAllProjects()
        {
           return projectRepo.GetList(_ => true).ToList();

        }

        public List<Project> GetUserProjects(List<int> projectIds)
        {
           
            return projectRepo.GetList(p=>projectIds.Contains(p.Id)).ToList();
        }

        public void Edit(Project project)
        {
            projectRepo.Update(project);
            projectRepo.Save();
        }

    }
}
