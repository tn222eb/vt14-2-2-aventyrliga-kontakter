using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ÄventyrligaKontakter.Model;

namespace ÄventyrligaKontakter
{
    public partial class Default : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        private string Message
        {
            get
            {
                return Session["Message"] as string;
            }
            set
            {
                Session["Message"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Message != null)
            {
                MessagePanel.Visible = true;
                SuccessLabel.Text = Message;
                Session.Remove("Message");
            }
        }

        public IEnumerable<Contact> ContactListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return Service.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        public void ContactListView_DeleteItem(int contactId)
        {
            try
            {
                Service.DeleteContact(contactId);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kontaktuppgiften skulle tas bort");
            }
        }

        public void ContactListView_InsertItem(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Service.SaveContact(contact);
                    Message = String.Format("'{0} {1}' har lagts till", contact.FirstName, contact.LastName);
                    Response.Redirect("~/");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kontaktuppgiften skulle sparas");
                }
            }
        }

        public void ContactListView_UpdateItem(int contactId)
        {
            try
            {
                var contact = Service.GetContact(contactId);
                if (contact == null)
                {
                    ModelState.AddModelError(String.Empty, String.Format("Kontakten med kontaktnummer {0} hittades inte.", contactId));
                    return;
                }

                if (TryUpdateModel(contact))
                {
                    Service.SaveContact(contact);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kontaktuppgiften skulle uppdateras.");
            }
        }

        protected void ImageCloseButton_Click(object sender, ImageClickEventArgs e)
        {
            MessagePanel.Visible = false;
        }





    }
}