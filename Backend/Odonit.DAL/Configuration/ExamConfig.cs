using Odonit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Configuration
{
    public class ExamConfig : EntityTypeConfiguration<Exam>
    {
        public ExamConfig()
        {
            ToTable("Exam");
            HasKey(x => x.ExamId);
            Property(x => x.Extraoral);
            Property(x => x.Intraoral);
            Property(x => x.Others);
            Property(x => x.Observations);
            
        }
    }
}
