using Odonit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Configuration
{
    public class MedicalHistoryConfig : EntityTypeConfiguration<MedicalHistory>
    {
        public MedicalHistoryConfig()
        {
            ToTable("MedicalHistory");
            HasKey(x => x.MedicalHistoryId);
            Property(x => x.CurrentMedication);
            Property(x => x.BloodGroup);
            Property(x => x.Alergic);
            Property(x => x.Hemorrhage);
            Property(x => x.Diabetes);
            Property(x => x.HeartDisease);
            Property(x => x.RespiratoryDisease);
            Property(x => x.KidneyDisease);
            Property(x => x.Hepatitis);
            Property(x => x.Hypertension);
            Property(x => x.STD);
            Property(x => x.PregnancyDate);
            Property(x => x.TransfusionDate);
            Property(x => x.Observations);
            Property(x => x.Habits);
            Property(x => x.Others);
            HasOptional(x => x.Exam).WithMany(a => a.MedicalHistories).HasForeignKey(x => x.ExamId);
        }
    }
}
