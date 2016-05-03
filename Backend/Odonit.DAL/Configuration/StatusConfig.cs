using Odonit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Configuration
{
    public class StatusConfig : EntityTypeConfiguration<Status>
    {
        public StatusConfig() {
            ToTable("Status");
            HasKey(x => x.StatusId);
            Property(x => x.Name);
        }
    }
}
