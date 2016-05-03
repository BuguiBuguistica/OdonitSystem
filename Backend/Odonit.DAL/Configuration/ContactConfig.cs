using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Odonit.DAL.Models;

namespace Odonit.DAL.Configuration
{
    public class ContactConfig : EntityTypeConfiguration<Contact>
    {
        public ContactConfig()
        {
            ToTable("Contact");
            HasKey(x => x.ContactId);
            Property(x => x.LandPhone);
            Property(x => x.CellPhone);
            Property(x => x.Email);
        }
    }
}
