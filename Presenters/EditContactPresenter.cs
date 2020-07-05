using ContactManager.Model;
using ContactManager.Views;
using ContactManager.Utils;

namespace ContactManager.Presenters
{
    public class EditContactPresenter : PresenterBase<EditContactView>
    {
        private readonly ApplicationPresenter applicationPresenter;
        private readonly Contact contact;
        private readonly DataValidation contactInfoValidator = new DataValidation();

        public EditContactPresenter(
            ApplicationPresenter _applicationPresenter,
            EditContactView view,
            Contact _contact) : base(view, "Contact.LookupName")
        {
            applicationPresenter = _applicationPresenter;
            contact = _contact;
        }

        public Contact Contact
        {
            get { return contact; }
        }

        public void SelectImage()
        {
            string imagePath = View.AskUserForImagePath();
            if (!string.IsNullOrEmpty(imagePath))
                Contact.ImagePath = imagePath;
        }

        public void Save()
        {
            if(contactInfoValidator.validateContact(View, Contact))
            {
                applicationPresenter.SaveContact(Contact);
            }
            else
            {
                string statusMessage = string.Format(
                    "Contact '{0}' Couldn't be saved dude to Invalid Data",
                    contact.LookupName
                    );

                applicationPresenter.UpdateStatusText(statusMessage);
            }
        }

        public void Delete()
        {
            applicationPresenter.CloseTab(this);
            applicationPresenter.DeleteContact(Contact);
        }

        public void Close()
        {
            applicationPresenter.CloseTab(this);
        }

        public override bool Equals(object obj)
        {
            EditContactPresenter presenter = obj as EditContactPresenter;
            return presenter != null && presenter.Contact.Equals(Contact);
        }
    }
}