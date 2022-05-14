﻿namespace SD_125_BugTracker.DAL
{
    public interface IRepository<T> where T : class
    {
    
        void Add(T entity);

        //read
        T Get(int id);
        T Get(Func<T, bool> firstFunction);
        ICollection<T> GetAll();
        ICollection<T> GetList(Func<T, bool> whereFunction);

        //update
        void Update(T entity);

        //delete
        void Delete(T entity);

        void Save();
    }
}
