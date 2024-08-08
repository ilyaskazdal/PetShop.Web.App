using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Entity
{
    public class Adopt
    {
        public int AdoptId { get; set; }
        public string AnimalName { get; set; }
        public string AnimalDescription { get; set; }
        public bool Gender { get; set; }
        public int UserId {  get; set; }


    }
}
