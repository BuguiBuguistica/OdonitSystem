using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odonit.DAL.Models
{
    public class Odontogram
    {
        public Odontogram(){
            Teeth = new HashSet<Tooth>();
        }
        public int OdontogramId { get; set; }
        public virtual ICollection<Tooth> Teeth { get; set; }
    }
}
