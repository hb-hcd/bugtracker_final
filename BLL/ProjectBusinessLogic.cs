
using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.BLL
{
    public class ProjectBusinessLogic
    {
        public ProjectRepository projectRepo;
     

        public ProjectBusinessLogic(ProjectRepository projectRepoArg)
        {
            projectRepo = projectRepoArg;
        }

        public void Add(Project project)
        {
            projectRepo.Add(project);
            projectRepo.Save();
        }

        public List<Project> GetAllProjects()
        {
           return projectRepo.GetAll().ToList();

        }

        public List<Project> GetUserProjects(List<int> projectIds)
        {
           
            return projectRepo.GetList(p=>projectIds.Contains(p.Id)).ToList();
        }

    }
}
