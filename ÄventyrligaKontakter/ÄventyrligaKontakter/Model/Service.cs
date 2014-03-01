﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ÄventyrligaKontakter.Model.DAL;

namespace ÄventyrligaKontakter.Model
{
    public class Service
    {
        private ContactDAL _contactDAL;

        private ContactDAL ContactDAL
        {
            get { return _contactDAL ?? (_contactDAL = new ContactDAL());}
        }

        public void DeleteContact(int contactId)
        {
            ContactDAL.DeleteContact(contactId);
        }

        public void SaveContact(Contact contact)
        {
            ICollection<ValidationResult> validationResults;
            if (!contact.Validate(out validationResults))
            {
                var ex = new ValidationException("Kontaktobjektet klarade inte datavalideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

            if (contact.ContactId == 0)
            {
                ContactDAL.InsertContact(contact);
            }
            else
            {
                ContactDAL.UpdateContact(contact);
            }
        }

        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }

        public Contact GetContact(int contactId)
        {
            return ContactDAL.GetContactById(contactId);
        }

        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }
        
    }
}