using Odonit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Configuration
{
    public class MedicalSecurityConfig : EntityTypeConfiguration<MedicalSecurity>
    {
        public MedicalSecurityConfig()
        {
            ToTable("MedicalSecurity");
            HasKey(x => x.MedicalSecurityId);
            Property(x => x.Name);
        }
    }
}
