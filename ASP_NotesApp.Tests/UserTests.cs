using ASP_NotesApp.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ASP_NotesApp.Tests
{
    public class UserTests
    {
        [Fact]
        public void CanChangeName()
        {
            User user = new() { Id = 1,Email = "fruct@yandex.ru", Name = "Evgenii" , Password = "123", Patronymic = "Evgenevich", Surname = "Antonov" };

            user.Name = "Sergei";

            Assert.Equal("Sergei", user.Name);
        }
        [Fact]
        public void CanChangeSurname()
        {
            User user = new() { Id = 1, Email = "fruct@yandex.ru", Name = "Evgenii", Password = "123", Patronymic = "Evgenevich", Surname = "Antonov" };

            user.Surname = "Sergeev";

            Assert.Equal("Sergeev", user.Surname);
        }
        [Fact]
        public void CanChangePatronymic()
        {
            User user = new() { Id = 1, Email = "fruct@yandex.ru", Name = "Evgenii", Password = "123", Patronymic = "Evgenevich", Surname = "Antonov" };

            user.Patronymic = "Antonovich";

            Assert.Equal("Antonovich", user.Patronymic);
        }
        [Fact]
        public void CanChangeEmail()
        {
            User user = new() { Id = 1, Email = "fruct@yandex.ru", Name = "Evgenii", Password = "123", Patronymic = "Evgenevich", Surname = "Antonov" };

            user.Email = "gaglu@gmail.com";

            Assert.Equal("gaglu@gmail.com", user.Email);
        }
        [Fact]
        public void CanChangePassword()
        {
            User user = new() { Id = 1, Email = "fruct@yandex.ru", Name = "Evgenii", Password = "123", Patronymic = "Evgenevich", Surname = "Antonov" };

            user.Password = "321";

            Assert.Equal("321", user.Password);
        }
    }
}
