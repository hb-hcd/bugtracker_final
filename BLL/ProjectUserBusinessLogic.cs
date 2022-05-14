using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.BLL
{
    public class ProjectUserBusinessLogic
    {
        public ProjectUserRepository projectUserRepo;
        public ProjectUserBusinessLogic(ProjectUserRepository repoArg)
        {
            projectUserRepo = repoArg;
        }

        public void Add(ProjectUser projectUser)
        {
            projectUserRepo.Add(projectUser);
            projectUserRepo.Save();
        }

        public List<ProjectUser> GetList(string id)
        {
           return projectUserRepo.GetList(pu => pu.UserId == id).ToList();
        }
    }
}
