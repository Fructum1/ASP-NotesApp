﻿using ASP_NotesApp.DAL;
using ASP_NotesApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASP_NotesApp.DAL.Repository
{
    public class NotesRepository : IGenericRepository<Note>
    {
        private readonly NoteAppDBContext _context;
        public NotesRepository(NoteAppDBContext context)
        {
            _context = context;
        }
        public void Create(Note item)
        {
            _context.Notes.Add(item);
            _context.SaveChanges();
        }

        public async Task<Note> CreateAsync(Note item)
        {
            var result = await _context.Notes.AddAsync(item);
            _context.SaveChanges();
            return result.Entity;
        }

        public void Delete(int id)
        {
            Note noteToDelete =  Get(id);

            if (noteToDelete != null)
            {
                _context.Notes.Remove(noteToDelete);
                _context.SaveChanges();
            }
        }

        public void DeleteAll()
        {
            _context.Notes.RemoveRange(Get());
        }

        public async Task<Note> DeleteAsync(int id)
        {
            Note noteToDelete = await GetAsync(id);

            if (noteToDelete != null)
            {
                _context.Notes.Remove(noteToDelete);
                _context.SaveChanges();
            }
            return noteToDelete;
        }

        public IEnumerable<Note> Get()
        {
            return _context.Notes;
        }

        public Note Get(int id)
        {
            return _context.Notes.FirstOrDefault(u => u.Id == id);
        }

        public async Task<Note> GetAsync(int id)
        {
            return await _context.Notes.FirstOrDefaultAsync(u => u.Id == id);
        }

        public void Update(Note item)
        {
            _context.Notes.Update(item);
            _context.SaveChanges();
        }
    }
}
