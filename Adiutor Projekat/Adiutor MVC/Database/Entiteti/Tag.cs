﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entiteti
{
    public class Tag
    {
        public virtual int Id { get; set; }
        public virtual string Ime { get; set; }
        public virtual string TagIme { get; set; }
        public virtual string Opis { get; set; }

        public virtual IList<Pitanje> PripadaPitanjima { get; set; }
 

        public Tag()
        {
            PripadaPitanjima = new List<Pitanje>();
        }
    }
}
