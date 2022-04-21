using Xunit;
using ASP_NotesApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NotesApp.Tests
{
  public class NoteController
    {
        [Fact]
        public void IndexViewDataMessage()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.Equal("Hello!", result?.ViewData["Message"]);
        }
    }
}