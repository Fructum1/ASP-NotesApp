﻿namespace ASP_NotesApp.Models.ViewModels.Note
{
    public class EditViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? NoteBody { get; set; }
        public bool Pined { get; set; }
        public int Status { get; set; }
    }
}
