using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoCopter2.Model;

namespace InfoCopter2.Repository
{
    public interface IInfoCopterRepository : IDisposable
    {
        IEnumerable<Company> GetCompanies();
        IEnumerable<Topic> GetCompanyTopics(long CompanyId);
        IEnumerable<Note> GetTopicNotes(long TopicId);
    }
}
