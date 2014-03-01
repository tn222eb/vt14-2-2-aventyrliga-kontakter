using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace ÄventyrligaKontakter.Model.DAL
{
    public class ContactDAL : DALBase
    {
        public static IEnumerable<Contact> GetContacts()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(100);

                    var cmd = new SqlCommand("Person.uspGetContacts", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactId");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                            {
                                ContactId = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAddress = reader.GetString(emailAddressIndex)
                            });
                        }
                    }
                    contacts.TrimExcess();

                    return contacts;
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel uppstod då kontakter skulle hämtas från databasen.");
                }
            }
        }

        public Contact GetContactById(int contactId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Person.uspGetContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ContactId", contactId);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int contactIdIndex = reader.GetOrdinal("ContactId");
                            int firstNameIndex = reader.GetOrdinal("FirstName");
                            int lastNameIndex = reader.GetOrdinal("LastName");
                            int emailAddressIndex = reader.GetOrdinal("EmailAddress");


                            return new Contact
                            {
                                ContactId = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAddress = reader.GetString(emailAddressIndex)
                            };
                        }
                    }
                    return null;
                }
                catch
                {
                    throw new ApplicationException("Ett fel har uppstått i dataåtkomst lager");
                }

            }
        }

        public void InsertContact(Contact contact)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Person.uspAddContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@Emailaddress", SqlDbType.NVarChar, 50).Value = contact.EmailAddress;

                    cmd.Parameters.Add("@ContactId", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    // Öppnar anslutning till databasen
                    conn.Open();

                    // Exekverar den lagrade proceduren
                    cmd.ExecuteNonQuery();

                    contact.ContactId = (int)cmd.Parameters["@ContactId"].Value;
                }
                catch
                {
                    throw new ApplicationException("Ett fel har uppstått i dataåtkomst lager");
                }

            }
        }

        public void UpdateContact(Contact contact)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Person.uspUpdateContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Skickar med parametrar för att lägga till en ny kontakt i databasen.
                    cmd.Parameters.Add("@ContactId", SqlDbType.Int, 4).Value = contact.ContactId;
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50).Value = contact.EmailAddress;

                    // Öppnar anslutning till databasen
                    conn.Open();

                    // Exekverar den lagrade proceduren
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("Ett fel har uppstått i dataåtkomst lager");
                }
            }
        }

        public void DeleteContact(int contactId)
        {

            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Person.uspRemoveContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Skickar med parametrar för att lägga ta bort kontakt från databasen
                    cmd.Parameters.Add("@ContactId", SqlDbType.Int, 4).Value = contactId;

                    // Öppnar anslutning till databasen
                    conn.Open();

                    // Exekverar den lagrade proceduren
                    cmd.ExecuteNonQuery();

                }
                catch
                {
                    throw new ApplicationException("Ett fel har uppstått i dataåtkomst lager");
                }
            }
        }


        public static IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(100);

                    var cmd = new SqlCommand("Person.uspGetContactsPageWise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                    totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactId");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                            {
                                ContactId = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAddress = reader.GetString(emailAddressIndex)
                            });
                        }
                    }

                    contacts.TrimExcess();
                    return contacts;
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel uppstod vid kontakt med databasen.");
                }
            }
        }

    }
}






