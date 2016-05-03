using Odonit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Configuration
{
    public class ToothConfig : EntityTypeConfiguration<Tooth>
    {
        public ToothConfig() { 
            ToTable("Tooth");
            HasKey(x => x.ToothId);
            Property(x => x.Code);
        }
    }
}
