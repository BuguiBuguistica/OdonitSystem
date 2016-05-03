using Odonit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Configuration
{
    public class ToothServiceConfig : EntityTypeConfiguration<ToothService>
    {
        public ToothServiceConfig() {
            ToTable("ToothService");
            HasKey(x => x.ToothServiceId);
            Property(x => x.Diagnosis);
            Property(x => x.InitialDate).IsOptional();
            Property(x => x.EndDate).IsOptional();
            Property(x => x.Amount).IsOptional();
            Property(x => x.Payment).IsOptional();
            Property(x => x.Remainder).IsOptional();
            HasRequired(x => x.Person);
            HasRequired(x => x.Tooth);
            HasOptional(x => x.Face);
            HasOptional(x => x.Service);
            HasRequired(x => x.Status);
            HasOptional(x => x.Treatment);
        }
    }
}
