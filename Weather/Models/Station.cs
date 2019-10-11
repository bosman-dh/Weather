using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Models
{
    public class Station
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Elevation { get; set; }

        public virtual ICollection<Data> Datas { get; set; }

        public override bool Equals(object obj) //need to Distinct Collection<Station>
        {
            if (obj is Station)
            {
                if (this.Id == (obj as Station).Id)
                    return true;
                else
                    return false;
            }
            else
                return base.Equals(obj);
        }

        public override int GetHashCode()   //need to Distinct Collection<Station>
        {
            return this.Id.GetHashCode();
        }
    }
}
