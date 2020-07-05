using System.Collections.ObjectModel;
using ContactManager.Model;
using ContactManager.Views;

namespace ContactManager.Presenters
{
    public class ApplicationPresenter : PresenterBase<Shell>
    {
        private readonly ContactRepository _contactRepository;
        private ObservableCollection<Contact> _currentContacts;
        private string _statusText;

        public ApplicationPresenter(
            Shell view,
            ContactRepository contactRepository)
            : base(view)
        {
            _contactRepository = contactRepository;

            _currentContacts = new ObservableCollection<Contact>(
                _contactRepository.FindAll()
                );
        }

        public ObservableCollection<Contact> CurrentContacts
        {
            get { return _currentContacts; }
            set
            {
                _currentContacts = value;
                OnPropertyChanged("CurrentContacts");
            }
        }

        public string StatusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                OnPropertyChanged("StatusText");
            }
        }

        public void Search(string criteria)
        {
            if (!string.IsNullOrEmpty(criteria) && criteria.Length > 2)
            {
                CurrentContacts = new ObservableCollection<Contact>(
                    _contactRepository.FindByLookup(criteria)
                    );

                string statusMessage = string.Format(
                    "{0} contact(s) found.",
                    CurrentContacts.Count
                    );

                UpdateStatusText(statusMessage);
            }
            else
            {
                CurrentContacts = new ObservableCollection<Contact>(
                    _contactRepository.FindAll()
                    );

                string statusMessage = "Displaying all contacts.";

                UpdateStatusText(statusMessage);
            }
        }

        public void OpenContact(Contact contact)
        {
            if (contact == null) return;

            View.AddTab(
                new EditContactPresenter(
                    this,
                    new EditContactView(),
                    contact
                    )
                );
        }

        public void NewContact()
        {
            OpenContact(new Contact());
        }

        public void SaveContact(Contact contact)
        {
            if (!CurrentContacts.Contains(contact))
                CurrentContacts.Add(contact);

            _contactRepository.Save(contact);

            string statusMessage = string.Format(
                "Contact <{0}> was saved.",
                contact.LookupName
                );

            UpdateStatusText(statusMessage);
        }

        public void DeleteContact(Contact contact)
        {
            if (CurrentContacts.Contains(contact))
                CurrentContacts.Remove(contact);

            _contactRepository.Delete(contact);

            string statusMessage = string.Format(
                "Contact <{0}> was deleted.",
                contact.LookupName
                );

            UpdateStatusText(statusMessage);
        }

        public void CloseTab<T>(PresenterBase<T> presenter)
        {
            View.RemoveTab(presenter);
        }

        public void DisplayAllContacts()
        {
            View.AddTab(
                new ContactListPresenter( this, new ContactListView() ) 
                );
        }

        public void UpdateStatusText(string statusMessage)
        {
            StatusText = statusMessage;
        }
    }
}