using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions;
using ContactManager.Model;
using ContactManager.Views;
using System.Linq;

namespace ContactManager.Utils
{
    class DataValidation
    {
        public bool validateContact(EditContactView currentContactView, Contact currentContact)
        {
            List<TextBox> invalidElementList = new List<TextBox>();
            List<TextBox> uniqueInvalidElementList = new List<TextBox>();


            // Collect text boxes with invalid data in a list
            invalidElementList.AddRange(IsNonEmpty(currentContactView));
            invalidElementList.AddRange(IsNonInteger(currentContactView));
            invalidElementList.AddRange(IsNonEmail(currentContactView));

            uniqueInvalidElementList = invalidElementList.Distinct().ToList();

            // If any of the textbox has invalid data, return false
            // to prevent invalid contact data from being saved
            if (invalidElementList.Count > 0)
            {
                // Make border of each invalid textbox red
                foreach (TextBox invalidTextbox in uniqueInvalidElementList)
                {
                    invalidTextbox.BorderBrush = Brushes.Red;
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        private List<TextBox> IsNonEmpty(EditContactView currentContactView)
        {
            List<TextBox> emptyTextBoxList = new List<TextBox>();

            if (currentContactView.firstName.Text.Length == 0)
                emptyTextBoxList.Add(currentContactView.firstName);

            if (currentContactView.lastName.Text.Length == 0)
                emptyTextBoxList.Add(currentContactView.lastName);

            if (currentContactView.line1.Text.Length == 0)
                emptyTextBoxList.Add(currentContactView.line1);

            if (currentContactView.line2.Text.Length == 0)
                emptyTextBoxList.Add(currentContactView.line2);

            if (currentContactView.organization.Text.Length == 0)
                emptyTextBoxList.Add(currentContactView.organization);

            if (currentContactView.jobTitle.Text.Length == 0)
                emptyTextBoxList.Add(currentContactView.jobTitle);

            if (currentContactView.city.Text.Length == 0)
                emptyTextBoxList.Add(currentContactView.city);

            if (currentContactView.zip.Text.Length == 0)
                emptyTextBoxList.Add(currentContactView.zip);

            if (currentContactView.country.Text.Length == 0)
                emptyTextBoxList.Add(currentContactView.country);

            if (currentContactView.office.Text.Length == 0)
                emptyTextBoxList.Add(currentContactView.office);

            if (currentContactView.cell.Text.Length == 0)
                emptyTextBoxList.Add(currentContactView.cell);

            if (currentContactView.home.Text.Length == 0)
                emptyTextBoxList.Add(currentContactView.home);

            if (currentContactView.primaryEmail.Text.Length == 0)
                emptyTextBoxList.Add(currentContactView.primaryEmail);

            return emptyTextBoxList;
        }

        private List<TextBox> IsNonInteger(EditContactView currentContactView)
        {

            List<TextBox> nonIntTextBoxList = new List<TextBox>();
            int result;

            bool isInt = int.TryParse(currentContactView.zip.Text, out result);

            if (!isInt) nonIntTextBoxList.Add(currentContactView.zip);

            return nonIntTextBoxList;

        }

        private List<TextBox> IsNonEmail(EditContactView currentContactView)
        {
            string emailString;
            bool isEmail;

            // Regex taken from msdn example : https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format?redirectedfrom=MSDN
            string validEmailRegExp = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            List<TextBox> nonEmailTextBoxList = new List<TextBox>();

            emailString = currentContactView.primaryEmail.Text;
            isEmail = Regex.IsMatch(emailString, validEmailRegExp, RegexOptions.IgnoreCase);
            if (!isEmail) nonEmailTextBoxList.Add(currentContactView.primaryEmail);

            emailString = currentContactView.secondaryEmail.Text;
            if (emailString.Length > 0)
            {
                isEmail = Regex.IsMatch(emailString, validEmailRegExp, RegexOptions.IgnoreCase);
                if (!isEmail) nonEmailTextBoxList.Add(currentContactView.secondaryEmail);
            }


            return nonEmailTextBoxList;

        }

    }
}
