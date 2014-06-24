using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoCopter2.Model;
using InfoCopter2.Repository;
using System.Data.Entity;

namespace InfoCopter2.Repository
{
    class InfoCopterRepository : IInfoCopterRepository
    {
        private readonly InfoCopterContext _context;

        public InfoCopterRepository(InfoCopterContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Company> GetCompanies() {
            return (from a in _context.Companies select a).ToList();
        }

        public IEnumerable<Topic> GetCompanyTopics(long CompanyId) {
            return (from a in _context.Topics
                    where a.CompanyId == CompanyId
                    select a).ToList();
        }

        public IEnumerable<Note> GetTopicNotes(long TopicId) {
            return (from a in _context.Notes
                    where a.TopicId == TopicId
                    select a).ToList();
        }

        public long AddCompany(string CompanyName)
        {
            Company newCompany = new Company();
            newCompany.CompanyName = CompanyName;

            _context.Companies.Add(newCompany);
            _context.SaveChanges();
            return newCompany.Id;
        }

        public void RemoveCompany(string CompanyId) { }

        public long AddTopic(long CompanyId, string TopicName)
        {
            Topic newTopic = new Topic();            
            newTopic.CompanyId = CompanyId;
            newTopic.TopicName = TopicName;

            _context.Topics.Add(newTopic);
            _context.SaveChanges();
            return newTopic.Id;
        }

        public void RemoveTopic(string CompanyId) { }

        internal long AddNote(long TopicId, DateTime Date)
        {
            Note newNote = new Note();
            newNote.TopicId = TopicId;
            newNote.Date = Date;
            newNote.Text = "";

            _context.Notes.Add(newNote);
            _context.SaveChanges();
            return newNote.Id;
        }

        public void RemoveNote(string CompanyId) { }

        internal Note GetNote(long NoteId)
        {
            if (NoteId > 0)
            {
                Note note = (from a in _context.Notes
                             where a.Id == NoteId
                             select a).First();

                return note;
            }
            else
                return null;
        }
    }
}
