using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanningMaker.Modele
{
    public class Cours : Enseignement
    {
        public Cours() : base() { }
        public Cours(int numGroup) : base(numGroup) { }
    }
}
