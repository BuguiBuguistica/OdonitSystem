using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Odonit.DAL.Models;

namespace Odonit.DAL.Configuration
{
    public class AddressConfig : EntityTypeConfiguration<Address>
    {
        public AddressConfig()
        {
            ToTable("Address");
            HasKey(x => x.Id);
            Property(x => x.Street);
            Property(x => x.ZipCode);
            Property(x => x.Number);
        }
    }
}
