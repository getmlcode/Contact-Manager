using System.Windows;
using System.Windows.Controls;
using ContactManager.Presenters;
using ContactManager.Model;

namespace ContactManager.Views
{
    /// <summary>
    /// Interaction logic for ContactListView.xaml
    /// </summary>
    public partial class ContactListView : UserControl
    {
        public ContactListView()
        {
            InitializeComponent();
        }

        public ContactListPresenter Presenter
        {
            get { return DataContext as ContactListPresenter; }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Presenter.Close();
        }

        private void OpenContact_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;
            if (button != null)
                Presenter.OpenContact(button.DataContext as Contact);
        }
    }
}
