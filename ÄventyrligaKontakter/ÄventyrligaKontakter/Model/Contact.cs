using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ÄventyrligaKontakter.Model
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage="Ett förnamn måste anges")]
        [StringLength(50, ErrorMessage = "Förnamnet kan bestå av som mest 50 tecken.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ett efternamn måste anges")]
        [StringLength(50 , ErrorMessage = "Efternamnet kan bestå av som mest 50 tecken.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "En e-postadress måste anges")]
        [MaxLength(50, ErrorMessage = "E-postadressen kan bestå av som mest 50 tecken.")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
        public string EmailAddress { get; set; }
    }
}