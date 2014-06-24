using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using InfoCopter2.Model;
using InfoCopter2.Repository;

namespace InfoCopter2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        InfoCopterContext _context = null;
        InfoCopterRepository repo = null;

        public MainWindow()
        {
            InitializeComponent();

            _context = new InfoCopterContext();
            repo = new InfoCopterRepository(_context);

            SetCompanies();
        }

        private Note Note;

        private void btnAddCompany_Click(object sender, RoutedEventArgs e)
        {
            if (txtCompanyName.Text.Trim().Length > 0)
            {
                long CompanyId = repo.AddCompany(txtCompanyName.Text);
                if (CompanyId > 0)
                {
                    SetCompanies();
                    listboxCompany.SelectedValue = CompanyId;
                    txtCompanyName.Text = string.Empty;
                }
            }
        }

        private void btnAddTopic_Click(object sender, RoutedEventArgs e)
        {
            if (txtTopicName.Text.Trim().Length > 0
                && listboxCompany.SelectedValue != null)
            {
                long CompanyId = (long)listboxCompany.SelectedValue;
                long TopicId = repo.AddTopic(CompanyId, txtTopicName.Text);
                if (TopicId > 0)
                {
                    SetCompanyTopics(CompanyId);
                    listboxTopic.SelectedValue = TopicId;
                    txtTopicName.Text = string.Empty;
                }
            }
        }

        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            if (listboxTopic.SelectedValue != null)
            {
                long TopicId = ((Topic)listboxTopic.SelectedItem).Id;
                long NoteId = repo.AddNote(TopicId, DateTime.Now);
                if (NoteId > 0)
                {
                    SetTopicNotes(TopicId);
                }
            }
        }

        private void SetCompanies()
        {
            listboxCompany.ItemsSource = repo.GetCompanies();
            listboxCompany.DisplayMemberPath = "CompanyName";
            listboxCompany.SelectedValuePath = "Id";
        }

        private void SetCompanyTopics(long CompanyId)
        {
            listboxTopic.ItemsSource = repo.GetCompanyTopics(CompanyId);
            listboxTopic.DisplayMemberPath = "TopicName";
        }

        private void SetTopicNotes(long TopicId)
        {
            listboxNote.ItemsSource = repo.GetTopicNotes(TopicId);
            listboxNote.DisplayMemberPath = "Date";
        }

        private void SetNote(long NoteId)
        {
            Note = repo.GetNote(NoteId);
            txtNote.DataContext = Note;
        }

        private void listboxCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            long CompanyId = ((Company)listboxCompany.SelectedItem).Id;

            SetCompanyTopics(CompanyId);
        }

        private void listboxTopic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            long TopicId = 0;
            if (listboxTopic.SelectedItem != null)
                TopicId = ((Topic)listboxTopic.SelectedItem).Id;

            SetTopicNotes(TopicId);
        }

        private void listboxNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            long NoteId = 0;
            if (listboxNote.SelectedItem != null)
                NoteId = ((Note)listboxNote.SelectedItem).Id;

            SetNote(NoteId);
        }
    }
}
