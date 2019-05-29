using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace NotesFragment
{
    public class DatabaseService
    {
        public SQLiteConnection db;
        public static List<Notes> NotesList { get; set; }
        public static DatabaseService DbConnection { get; set; }

        public DatabaseService()
        {
            CreateDatabase();
            CreateTableWithData();
            NotesList = GetAllNotes().ToList();
        }

        private void CreateDatabase()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "NotesDB01.db3");
            db = new SQLiteConnection(dbPath);
        }
        public Notes GetOneNote(int id)
        {
            return db.Table<Notes>().Where(x => x.Id == id).FirstOrDefault();
        }

        public void AddNote(Notes note)
        {
            note.Title = "";
            note.Note = "";
            db.Insert(note);
        }

        public void UpdateNote(int id, string title, string content)
        {
            var note = new Notes
            {
                Id = id,
                Title = title,
                Note = content
            };
            db.Update(note);
        }

        public List<Notes> GetAllNotes()
        {
            var table = db.Table<Notes>();
            return table.ToList();
        }

        public void DeleteNote(int id)
        {
            var note = new Notes
            {
                Id = id
            };
            db.Delete(note);
        }

        private void CreateTableWithData()
        {
            db.CreateTable<Notes>();
            if (db.Table<Notes>().Count() == 0)
            {
                var newNotes = new Notes
                {
                    Title = "Example ",
                    Note = "Content for the example note."
                };
                db.Insert(newNotes);
            }
        }

    }
}