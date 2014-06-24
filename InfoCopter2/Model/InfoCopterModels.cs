using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoCopter2.Model
{
    public class Company
    {
        public Company() {
            TopicValues = new List<Topic>();
        }

        public long Id { get; set; }
        public string CompanyName { get; set; }

        public virtual ICollection<Topic> TopicValues { get; set; }
    }

    public class Topic
    {
        public Topic() {
            NoteValues = new List<Note>(); 
        }

        public long Id { get; set; }        
        public string TopicName { get; set; }

        public virtual long CompanyId { get; set; }
        public virtual ICollection<Note> NoteValues { get; set; }
    }

    public class Note
    {
        public Note() { }

        public long Id { get; set; }        
        public DateTime Date { get; set; }
        public string Text { get; set; }

        public virtual long TopicId { get; set; }
    }
}
