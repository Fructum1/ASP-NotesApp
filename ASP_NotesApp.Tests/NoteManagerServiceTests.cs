using ASP_NotesApp.DAL;
using ASP_NotesApp.DTO;
using ASP_NotesApp.Extensions.Exceptions;
using ASP_NotesApp.Models.Domain;
using ASP_NotesApp.Models.Enum;
using ASP_NotesApp.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using System.Security.Claims;
using Xunit;

namespace ASP_NotesApp.Tests
{
    public class NoteManagerServiceTests
    {
        public Mock<IGenericRepository<Note>> _mockNoteRepository = new Mock<IGenericRepository<Note>>();
        public Mock<IGenericRepository<User>> _mockUserRepository = new Mock<IGenericRepository<User>>();
        public Mock<IHttpContextAccessor> _mockIhttpContext = new Mock<IHttpContextAccessor>();
        [Fact]
        public void GetNoteAsyncReturnValidData()
        {
            // Arrange
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(GetNoteTest(1));
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            // Act
            var result = _noteManager.GetNoteAsync(1);

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(GetNoteTest(1)), JsonConvert.SerializeObject(result.Result));
        }

        [Fact]
        public void GetNoteAsyncReturnNote()
        {
            // Arrange
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(GetNoteTest(1));
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            // Act
            var result = _noteManager.GetNoteAsync(1).Result;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Note>(result);
        }
     
        [Fact]
        public void GetAllAsyncValidData()
        {
            _mockIhttpContext.Setup(m => m.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("name", "1"));   
            _mockNoteRepository.Setup(repo => repo.GetAll(1).Result).Returns(GetNotesTest(1));  
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            var result = _noteManager.GetAllAsync().Result.ToList();
            var excepted = GetNotesTest(1);

            Assert.True(result[0].NoteBody == excepted[0].NoteBody &&
                        result[0].Title == excepted[0].Title &&
                        result[1].NoteBody == excepted[1].NoteBody &&
                        result[1].Title == excepted[1].Title &&
                        result[2].NoteBody == excepted[2].NoteBody &&
                        result[2].Title == excepted[2].Title);
        }
        [Fact]
        public void GetAllAsyncNotValidData()
        {
            _mockIhttpContext.Setup(m => m.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("name", "1"));
            _mockNoteRepository.Setup(repo => repo.GetAll(1).Result).Returns(GetNotesTest(1));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            var result = _noteManager.GetAllAsync().Result.ToList();
            var excepted = GetNotesTest(2);

            Assert.False(result[0].NoteBody != excepted[0].NoteBody ||
                        result[0].Title != excepted[0].Title ||
                        result[1].NoteBody != excepted[1].NoteBody ||
                        result[1].Title != excepted[1].Title ||
                        result[2].NoteBody != excepted[2].NoteBody ||
                        result[2].Title != excepted[2].Title);
        }
        [Fact]
        public void GetAllAsyncReturnIEnumerableNotNull()
        {
            _mockIhttpContext.Setup(m => m.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("name", "1"));
            _mockNoteRepository.Setup(repo => repo.GetAll(1).Result).Returns(GetNotesTest(1));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            var result = _noteManager.GetAllAsync().Result;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Note>>(result);
        }
        [Fact]
        public async void ArchiveAsyncSuccess()
        {
            Note noteToUpdate = GetNoteTest(1);
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.ArchiveAsync(noteToUpdate.Id);

            Assert.True(noteToUpdate.Status == (int)StatusNote.Archived);
        }
        [Fact]
        public async void ArchiveAsyncUnPinOnSuccess()
        {
            Note noteToUpdate = GetNoteTest(1);
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.ArchiveAsync(noteToUpdate.Id);

            Assert.True(noteToUpdate.Pined == false);
        }
        [Fact]
        public async void EditAsyncSuccess()
        {
            Note noteToUpdate = GetNoteTest(1);
            _mockIhttpContext.Setup(m => m.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("name", "1"));
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.EditAsync(new DTO.NoteEditDTO() {
                NoteBody = "World",
                Title = "hello",
                Pined = false,
                Status = (int)StatusNote.Active,
            }, 1);
            var excepted = new Note()
            {
                Id = 1,
                NoteBody = "World",
                Pined = false,
                Title = "hello",
                Status = 0,
                CreationDate = DateTime.Today,
                UserId = 1,
            };

            Assert.Equal(JsonConvert.SerializeObject(excepted), JsonConvert.SerializeObject(noteToUpdate));
        }
        [Fact]
        public void EditAsyncThrowExceptionNotFound()
        {
            Note noteToUpdate = GetNoteTest(1);
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            Assert.ThrowsAsync<NoteNotFoundOrDeletedException>(() => _noteManager.EditAsync(new DTO.NoteEditDTO()
            {
                NoteBody = "World",
                Title = "hello",
                Pined = false,
                Status = (int)StatusNote.Deleted,
            }, 1));
        }
        [Fact]
        public void EditAsyncThrowExceptionOwnedByAnotherUser()
        {
            Note noteToUpdate = GetNoteTest(1);
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            Assert.ThrowsAsync<NoteOwnedByAnotherUserException>(() => _noteManager.EditAsync(new DTO.NoteEditDTO()
            {
                NoteBody = "World",
                Title = "hello",
                Pined = false,
                Status = (int)StatusNote.Deleted,
            }, 1));
        }
        [Fact]
        public async void CreateAsyncSuccess()
        {
            Note note = new Note(){ Id = 1, CreationDate = DateTime.Today, UserId = 1, NoteBody = "Hello", Title = "world!", Pined = true, Status = (int)StatusNote.Active };
            NoteCreateDTO noteCreateDTO = new NoteCreateDTO() { Id = 1, CreationDate = DateTime.Today, Creator = 1, NoteBody = "Hello", Title = "world!", Pined = true };
            _mockNoteRepository.Setup(repo => repo.CreateAsync(It.IsAny<Note>()));
            _mockIhttpContext.Setup(m => m.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("name", "1"));
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(note);
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.CreateAsync(noteCreateDTO);
            var except = await _noteManager.GetNoteAsync(1);

            Assert.Equal(JsonConvert.SerializeObject(note), JsonConvert.SerializeObject(except));
        }
        [Fact]
        public void CreateAsyncNoteNotValid()
        {
            NoteCreateDTO noteCreateDTO = new NoteCreateDTO() { Id = 1, CreationDate = DateTime.Today, Creator = 1, Pined = true };
            _mockNoteRepository.Setup(repo => repo.CreateAsync(It.IsAny<Note>()));
            _mockIhttpContext.Setup(m => m.HttpContext.User.FindFirst(It.IsAny<string>())).Returns(new Claim("name", "1"));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            Assert.ThrowsAsync<NoteNotValid>(async () => await _noteManager.CreateAsync(noteCreateDTO));
        }
        [Fact]
        public async void PinSuccess()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Active};
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.PinAsync(noteToUpdate.Id);

            Assert.True(noteToUpdate.Pined == true);
        }
        [Fact]
        public async void PinFailure()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Archived};
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.PinAsync(noteToUpdate.Id);

            Assert.True(noteToUpdate.Pined == false);
        }
        [Fact]
        public void PinThrowException()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Archived };
            _mockNoteRepository.Setup(repo => repo.GetAsync(2).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            Assert.ThrowsAsync<NoteNotFoundOrDeletedException>(async () => await _noteManager.PinAsync(noteToUpdate.Id));
        }
        [Fact]
        public async void UnPinSuccess()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = true, Title = "123", Status = (int)StatusNote.Active };
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.UnPinAsync(noteToUpdate.Id);

            Assert.True(noteToUpdate.Pined == false);
        }
        [Fact]
        public async void UnPinFailure()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = true, Title = "123", Status = (int)StatusNote.Archived };
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.UnPinAsync(noteToUpdate.Id);

            Assert.True(noteToUpdate.Pined == true);
        }
        [Fact]
        public void UnPinThrowException()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Archived };
            _mockNoteRepository.Setup(repo => repo.GetAsync(2).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            Assert.ThrowsAsync<NoteNotFoundOrDeletedException>(async () => await _noteManager.UnPinAsync(noteToUpdate.Id));
        }
        [Fact]
        public async void RecoverFromTrashCanSuccess()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Deleted };
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.RecoverFromTrashCanAsync(noteToUpdate.Id);

            Assert.True(noteToUpdate.Status == (int)StatusNote.Active);
        }
        [Fact]
        public async void RecoverFromTrashCanFailure()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Archived };
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.RecoverFromTrashCanAsync(noteToUpdate.Id);

            Assert.True(noteToUpdate.Status != (int)StatusNote.Active);
        }
        [Fact]
        public void RecoverFromTrashCanThrowException()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Archived };
            _mockNoteRepository.Setup(repo => repo.GetAsync(2).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            Assert.ThrowsAsync<NoteNotFoundOrDeletedException>(async () => await _noteManager.RecoverFromTrashCanAsync(noteToUpdate.Id));
        }
        [Fact]
        public async void UnArchiveAsyncSuccess()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Archived };
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.UnArchiveAsync(noteToUpdate.Id);

            Assert.True(noteToUpdate.Status == (int)StatusNote.Active);
        }
        [Fact]
        public async void UnArchiveAsyncFailure()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Deleted };
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.UnArchiveAsync(noteToUpdate.Id);

            Assert.True(noteToUpdate.Status != (int)StatusNote.Archived);
        }
        [Fact]
        public void UnArchiveAsyncThrowException()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Archived };
            _mockNoteRepository.Setup(repo => repo.GetAsync(2).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            Assert.ThrowsAsync<NoteNotFoundOrDeletedException>(async () => await _noteManager.UnArchiveAsync(noteToUpdate.Id));
        }
        [Fact]
        public async void DeleteAsyncSuccess()
        {
            Note noteToUpdate1 = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Active };
            Note noteToUpdate2 = new Note() { Id = 2, Pined = false, Title = "123", Status = (int)StatusNote.Archived };
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate1);
            _mockNoteRepository.Setup(repo => repo.GetAsync(2).Result).Returns(noteToUpdate2);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.DeleteAsync(noteToUpdate1.Id);
            await _noteManager.DeleteAsync(noteToUpdate2.Id);

            Assert.True(noteToUpdate1.Status == (int)StatusNote.Deleted);
            Assert.True(noteToUpdate2.Status == (int)StatusNote.Deleted);
        }
        [Fact]
        public async void DeleteAsyncFailure()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Deleted };
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.DeleteAsync(noteToUpdate.Id);

            Assert.True(noteToUpdate.Status != (int)StatusNote.Archived);
        }
        [Fact]
        public void DeleteAsyncThrowException()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Archived };
            _mockNoteRepository.Setup(repo => repo.GetAsync(2).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            Assert.ThrowsAsync<NoteNotFoundOrDeletedException>(async () => await _noteManager.DeleteAsync(noteToUpdate.Id));
        }
        [Fact]
        public async void RemoveAsyncSuccess()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Deleted };
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.RemoveAsync(noteToUpdate.Id);

            _mockNoteRepository.Verify(r => r.DeleteAsync(noteToUpdate.Id));
        }
        [Fact]
        public async void RemoveAsyncFailure()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Active };
            _mockNoteRepository.Setup(repo => repo.GetAsync(1).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            await _noteManager.DeleteAsync(noteToUpdate.Id);

            _mockNoteRepository.Verify(r => r.DeleteAsync(noteToUpdate.Id), Times.Never());
        }
        [Fact]
        public void RemoveAsyncThrowException()
        {
            Note noteToUpdate = new Note() { Id = 1, Pined = false, Title = "123", Status = (int)StatusNote.Archived };
            _mockNoteRepository.Setup(repo => repo.GetAsync(2).Result).Returns(noteToUpdate);
            _mockNoteRepository.Setup(repo => repo.Update(It.IsAny<Note>()));
            var _userManager = new UserManagerService(_mockUserRepository.Object, _mockIhttpContext.Object);
            var _noteManager = new NoteManagerService(_mockNoteRepository.Object, _userManager);

            Assert.ThrowsAsync<NoteNotFoundOrDeletedException>(async () => await _noteManager.DeleteAsync(noteToUpdate.Id));
        }

        private List<Note> GetNotesTest(int id)
        {
            var notes = new List<Note>{
                new Note { Id = 1, Pined = true, UserId = id},
                new Note { Id = 2, Title = "fdsa", UserId = id},
                new Note { Id = 3, NoteBody = "123", UserId = id},
            };
            return notes;
        }
        private Note GetNoteTest(int id)
        {
            var note = new Note
            {
                Id = id,
                NoteBody = "Hello",
                Pined = true,
                Title = "worldASDF",
                Status = 0,
                CreationDate = DateTime.Today,
                UserId = 1,
            };
            return note;
        }
    }
}