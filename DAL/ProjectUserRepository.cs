using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.DAL
{
    public class ProjectUserRepository : IRepository<ProjectUser>
    {
        public ApplicationDbContext _db
        {
            get; set;
        }

        public ProjectUserRepository(ApplicationDbContext context)
        {
            _db = context;
        }
        public void Add(ProjectUser entity)
        {
            _db.ProjectUsers.Add(entity);
        }

        public void Delete(ProjectUser entity)
        {
            _db.ProjectUsers.Remove(entity);
        }

        public ProjectUser Get(int id)
        {
            return  _db.ProjectUsers.Find(id);
        }

        public ProjectUser Get(Func<ProjectUser, bool> firstFunction)
        {
            return _db.ProjectUsers.First(firstFunction);
        }

        public ICollection<ProjectUser> GetAll()
        {
            return _db.ProjectUsers.ToList();
        }

        public ICollection<ProjectUser> GetList(Func<ProjectUser, bool> whereFunction)
        {
            return _db.ProjectUsers.Where(whereFunction).ToList();
        }

        public void Save()
        {
           _db.SaveChanges();
        }

        public void Update(ProjectUser entity)
        {
           _db.ProjectUsers.Update(entity);
        }
    }
}
