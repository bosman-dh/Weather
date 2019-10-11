using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Models
{
    public class Data
    {
        public int? DataId { get; set; }
        public DateTime Date { get; set; }
        public float? Precipitation { get; set; }
        public float? Snow { get; set; }
        public float? Tavg { get; set; }
        public float? Tmax { get; set; }
        public float? Tmin { get; set; }

        public string StationId { get; set; }

        public virtual Station Station { get; set; }


        public override bool Equals(object obj) //need to Distinct Collection<Data>
        {
            if (obj is Data)
            {
                if (this.Date.ToString() + this.StationId == (obj as Data).Date.ToString() + (obj as Data).StationId)
                    return true;
                else
                    return false;
            }
            else
                return base.Equals(obj);
        }

        public override int GetHashCode()   //need to Distinct Collection<Data>
        {
            return this.Date.ToString().GetHashCode();
        }
    }
}
