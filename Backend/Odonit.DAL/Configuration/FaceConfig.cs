using Odonit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Configuration
{
    public class FaceConfig : EntityTypeConfiguration<Face>
    {
        public FaceConfig() {
            ToTable("Face");
            HasKey(x => x.FaceId);
            Property(x => x.Name);
            Property(x => x.Position);
            Property(x => x.Quadrant);
        }
    }
}
