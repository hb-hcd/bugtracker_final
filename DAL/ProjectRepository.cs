using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.DAL
{
    public class ProjectRepository : IRepository<Project>
    {
        public ApplicationDbContext _db { get; set; }
        public ProjectRepository()
        {

        }
        public ProjectRepository(ApplicationDbContext context)
        {
            _db = context;
        }
        public void Add(Project entity)
        {
            _db.Projects.Add(entity);
        }

        public void Delete(Project entity)
        {
            _db.Projects.Remove(entity);
        }

        public virtual Project Get(int id)
        {
           return _db.Projects.Find(id);
        }

        public virtual Project? Get(Func<Project, bool> firstFunction)
        {
            return _db.Projects.First(firstFunction);
        }

        public virtual ICollection<Project> GetAll()
        {
            return _db.Projects.ToList();
        }

        public virtual ICollection<Project> GetList(Func<Project, bool> whereFunction)
        {
            
            return _db.Projects.Where(whereFunction).ToList();
        }

        public void Update(Project entity)
        {
            _db.Projects.Update(entity);
        }
        public void Save()
        {
            _db.SaveChanges();
        }

       
    }
}
