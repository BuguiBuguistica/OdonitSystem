using Odonit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Configuration
{
    public class TreatmentConfig : EntityTypeConfiguration<Treatment>
    {
        public TreatmentConfig() {
            ToTable("Treatment");
            HasKey(x => x.TreatmentId);
            Property(x => x.Code);
            Property(x => x.Name);
        }
    }
}
