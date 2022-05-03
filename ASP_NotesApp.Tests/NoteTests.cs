using ASP_NotesApp.Models.Domain;
using ASP_NotesApp.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ASP_NotesApp.Tests
{
    public class NoteTests
    {
        [Fact]
        public void CanChangeNoteBody()
        {
            Note note = new Note() { Id = 1, CreationDate = DateTime.Today, Title = "Hello", NoteBody = "Hello", Pined = true, Status = (int)StatusNote.Active, UserId = 1 };

            note.NoteBody = "Hello world!";

            Assert.Equal("Hello world!", note.NoteBody);
        }
        [Fact]
        public void CanChangeTitle()
        {
            Note note = new Note() { Id = 1, CreationDate = DateTime.Today, Title = "Hello", NoteBody = "Hello", Pined = true, Status = (int)StatusNote.Active, UserId = 1 };

            note.Title = "Hello world!";

            Assert.Equal("Hello world!", note.Title);
        }
        [Fact]
        public void CanChangePinned()
        {
            Note note = new Note() { Id = 1, CreationDate = DateTime.Today, Title = "Hello", NoteBody = "Hello", Pined = true, Status = (int)StatusNote.Active, UserId = 1 };

            note.Pined = false;

            Assert.False(note.Pined);
        }
        [Fact]
        public void CanChangeStatus()
        {
            Note note = new Note() { Id = 1, CreationDate = DateTime.Today, Title = "Hello", NoteBody = "Hello", Pined = true, Status = (int)StatusNote.Active, UserId = 1 };

            note.Status = (int)StatusNote.Deleted;

            Assert.True(note.Status == (int)StatusNote.Deleted);
        }
    }
}
