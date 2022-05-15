using SD_125_BugTracker.Data;
using SD_125_BugTracker.Models;

namespace SD_125_BugTracker.DAL
{
    public class AssignedProjectRepository : IRepository<AssignedProject>
    {
        public ApplicationDbContext _db
        {
            get; set;
        }

        public AssignedProjectRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public void Add(AssignedProject entity)
        {
            _db.AssignedProjects.Add(entity);
        }

        public AssignedProject Get(int id)
        {
          return  _db.AssignedProjects.Find(id);
        }

        public AssignedProject Get(Func<AssignedProject, bool> firstFunction)
        {
            return _db.AssignedProjects.FirstOrDefault(firstFunction);
        }

        public ICollection<AssignedProject> GetAll()
        {
            return _db.AssignedProjects.ToList();
        }

        public ICollection<AssignedProject> GetList(Func<AssignedProject, bool> whereFunction)
        {
          return _db.AssignedProjects.Where(whereFunction).ToList();
        }

        public void Update(AssignedProject entity)
        {
            _db.AssignedProjects.Update(entity);
        }

        public void Delete(AssignedProject entity)
        {
            _db.AssignedProjects.Remove(entity);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
