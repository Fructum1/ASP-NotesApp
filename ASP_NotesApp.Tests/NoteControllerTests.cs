using Xunit;
using ASP_NotesApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using ASP_NotesApp.Services;
using Moq;
using ASP_NotesApp.Models.Domain;
using ASP_NotesApp.DAL;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ASP_NotesApp.Models.ViewModels.Note;

namespace ASP_NotesApp.Tests
{
  public class NoteControllerTests
    {
        public Mock<IGenericRepository<Note>> _mockNoteRepository = new();
        public Mock<IGenericRepository<User>> _mockUserRepository = new();
        public Mock<IHttpContextAccessor> _mockIhttpContext = new();

        [Fact]
        public void IndexReturnsAViewResult()
        {
            var _mockRepository = new Mock<IGenericRepository<Note>>();
            _mockRepository.Setup(m => m.GetAll(1).Result).Returns(GetNotesTest(1));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);
            var controller = new NoteController(_noteManager, _userManager);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void GetNotesListReturnAPartialViewResultWithAListOfNotes()
        {
            _mockNoteRepository.Setup(m => m.GetAll(1).Result).Returns(GetNotesTest(1));
            _mockIhttpContext.Setup(m => m.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("name", "1"));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);
            var controller = new NoteController(_noteManager, _userManager);

            var result = await controller.GetNotesList("");

            var viewResult = Assert.IsType<PartialViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Note>>(viewResult.Model);
            Assert.Equal(GetNotesTest(1).Count, model.Count());
        }
        [Fact]
        public async void CreateRedirectWhenSuccess()
        {
            _mockNoteRepository.Setup(m => m.GetAll(1).Result).Returns(GetNotesTest(1));
            _mockIhttpContext.Setup(m => m.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("name", "1"));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);
            var controller = new NoteController(_noteManager, _userManager);
            CreateViewModel viewModel = new() { NoteBody = "Hi", Title = "Hello", Pined = false };

            var result = await controller.Create(viewModel);

            Assert.IsType<PartialViewResult>(result);
        }

        private static List<Note> GetNotesTest(int id)
        {
            var notes = new List<Note>{
                new Note { Id = 1, Pined = true, UserId = id, Status = 0},
                new Note { Id = 2, Title = "fdsa", UserId = id, Status = 0},
                new Note { Id = 3, NoteBody = "123", UserId = id, Status = 0},
            };
            return notes;
        }
    }
}