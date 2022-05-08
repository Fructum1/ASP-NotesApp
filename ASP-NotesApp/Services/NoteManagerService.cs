﻿using ASP_NotesApp.DAL;
using ASP_NotesApp.DTO;
using ASP_NotesApp.Extensions.Exceptions;
using ASP_NotesApp.Models.Domain;
using ASP_NotesApp.Models.Enum;

namespace ASP_NotesApp.Services
{
    public class NoteManagerService
    {
        protected readonly IGenericRepository<Note> _noteRepository;
        private readonly UserManagerService _userManager;

        public NoteManagerService(IGenericRepository<Note> repo, UserManagerService userManager)
        {
            _userManager = userManager;
            _noteRepository = repo;
        }

        public async Task<Note> CreateAsync(NoteCreateDTO model)
        {
            Note note = new Note();
            note.Id = model.Id;
            note.Title = model.Title;
            note.NoteBody = model.NoteBody;
            note.CreationDate = DateTime.Now;
            note.Status = (int)StatusNote.Active;
            note.UserId = _userManager.CurrentUserId;
            note.Pined = model.Pined;

            if (NoteValid(note))
            {
                return await _noteRepository.CreateAsync(note);
            }

            else throw new NoteNotValid();
        }

        public async Task CreateDefault(string email)
        {
            Note note = new Note(){
                UserId = await _userManager.GetCurrentUserId(email),
                NoteBody = "Hello!",
                Title = "Welcome to my app",
                CreationDate = DateTime.Now,
                Pined = true,
                Status = (int)StatusNote.Active,
            };

            await _noteRepository.CreateAsync(note);
        }

        public async Task<Note> GetNoteAsync(int id)
        {
            Note note = await _noteRepository.GetAsync(id);
            if (note == null) 
            { 
                throw new NoteNotFoundOrDeletedException(); 
            }

            return note;
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            var notes = await _noteRepository.Get(_userManager.CurrentUserId);
            if(notes == null)
            {
                throw new NoteNotFoundOrDeletedException();
            }

            return notes;
        }

        public async Task EditAsync(NoteEditDTO model, int id)
        {
            var note = await _noteRepository.GetAsync(id); 

            if(note.UserId != _userManager.CurrentUserId)
            {
                throw new NoteOwnedByAnotherUserException();
            }

            if (model.Status != (int)StatusNote.Deleted && note != null)
            {
                note.Title = model.Title;
                note.NoteBody = model.NoteBody;
                note.Pined = model.Pined;
                if (note.Pined)
                {
                    note.Status = (int)StatusNote.Active;
                }

                _noteRepository.Update(note);
            }
            else
            {
                throw new NoteNotFoundOrDeletedException();
            }
        }   

        public async Task ArchiveAsync(int id)
        {
            var note = await GetNoteAsync(id);
            if(note == null)
            {
                throw new NoteNotFoundOrDeletedException();
            }

            if (note.Status != (int)StatusNote.Archived) 
            { 
                note.Status = (int)StatusNote.Archived;
                note.Pined = false;
                _noteRepository.Update(note);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var note = await GetNoteAsync(id);
            if (note == null)
            {
                throw new NoteNotFoundOrDeletedException();
            }

            if (note.Status != (int)StatusNote.Deleted) 
            { 
                note.Status = (int)StatusNote.Deleted;
                note.Pined = false;
                _noteRepository.Update(note);
            }
        }

        public async Task RemoveAsync(int id)
        {
            var note = await GetNoteAsync(id);
            if (note == null)
            {
                throw new NoteNotFoundOrDeletedException();
            }

            if (note.Status == (int)StatusNote.Deleted)
            {
                await _noteRepository.DeleteAsync(id);
            }
        }

        public async Task UnArchiveAsync(int id)
        {
            var note = await GetNoteAsync(id);
            if (note == null)
            {
                throw new NoteNotFoundOrDeletedException();
            }

            if (note.Status == (int)StatusNote.Archived)
            {
                note.Status= (int)StatusNote.Active;
                _noteRepository.Update(note);
            }
        }

        public async Task RecoverFromTrashCanAsync(int id)
        {
            var note = await GetNoteAsync(id);
            if (note == null)
            {
                throw new NoteNotFoundOrDeletedException();
            }

            if (note.Status == (int)StatusNote.Deleted)
            {
                note.Status = (int)StatusNote.Active;
                _noteRepository.Update(note);
            }
        }

        public async Task PinAsync(int id)
        {
            var note = await GetNoteAsync(id);
            if(note == null)
            {
                throw new NoteNotFoundOrDeletedException();
            }

            if(note.Status == (int)StatusNote.Active && note.Pined == false)
            {
                note.Pined = true;
                _noteRepository.Update(note);
            }
        }

        public async Task UnPinAsync(int id)
        {
            var note = await GetNoteAsync(id);
            if (note == null)
            {
                throw new NoteNotFoundOrDeletedException();
            }

            if (note.Status == (int)StatusNote.Active && note.Pined == true)
            {
                note.Pined = false;
                _noteRepository.Update(note);
            }
        }

        private bool NoteValid(Note note)
        {
            if(note.Status == (int)StatusNote.Active &&   
              (note.NoteBody != null || note.Title != null))
                return true;
            else return false;
        }
    }
}
