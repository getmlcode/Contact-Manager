using System.Collections.ObjectModel;
using ContactManager.Model;
using ContactManager.Views;
 
namespace ContactManager.Presenters
{
    public class ContactListPresenter : PresenterBase<ContactListView>
    {
        private readonly ApplicationPresenter _applicationPresenter;

        public ContactListPresenter(
            ApplicationPresenter applicationPresenter,
            ContactListView view)
            : base(view, "TabHeader")
        {
            _applicationPresenter = applicationPresenter;
        }
        
        public ObservableCollection<Contact> AllContacts
        {
            get
            {
                return _applicationPresenter.CurrentContacts;
            }
        }

        public string TabHeader
        {
            get { return "All Contacts"; }
        }

        public void Close()
        {
            _applicationPresenter.CloseTab(this);
        }

        public void OpenContact(Contact contact)
        {
            _applicationPresenter.OpenContact(contact);
        }

        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }
    }
}
