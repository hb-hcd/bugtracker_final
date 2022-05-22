using SD_125_BugTracker.DAL;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.BLL
{
    public class AssignedProjectBusinessLogic
    {
        public AssignedProjectRepository repo;

        public AssignedProjectBusinessLogic(AssignedProjectRepository repoArg)
        {
            repo = repoArg;
        }

        public void Add(AssignedProject assignedProject)
        {
            repo.Add(assignedProject);
            repo.Save();
        }

        public AssignedProject Get(int id)
        {
            return repo.Get(ap => ap.ProjectId == id);
        }

        public List<AssignedProject> GetList(string userId)
        {
            return repo.GetList(ap => ap.UserId == userId).ToList();
        }
        public void Delete(int id)
        {
            AssignedProject project = repo.Get(ap=>ap.ProjectId == id);
            repo.Delete(project);
            repo.Save();
        }
    }
}
