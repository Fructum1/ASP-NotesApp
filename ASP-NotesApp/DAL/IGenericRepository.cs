﻿using ASP_NotesApp.Models.Domain;

namespace ASP_NotesApp.DAL
{
    public interface IGenericRepository<T>
    {
        void Create(T item);
        Task<T> CreateAsync(T item);
        void Update(T item);
        IEnumerable<T> Get();
        T Get(int id);
        Task<T> GetAsync(int id);
        void Delete(int id);
        Task<T> DeleteAsync(int id);
        void DeleteAll();
    }
}
