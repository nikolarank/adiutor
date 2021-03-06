﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entiteti;
using FluentNHibernate.Mapping;

namespace Database.Mapiranja
{
    class SmerMapiranja : ClassMap<Smer>
    {
        public SmerMapiranja()
        {
            Table("SMER");

            Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Ime).Column("IME");
            Map(x => x.PocSem).Column("POC_SEM");
            Map(x => x.KrajSem).Column("KRAJ_SEM");

            HasManyToMany(x => x.ImaPredmete).Table("PREDMET_SMER").ParentKeyColumn("SMER_ID")
                .ChildKeyColumn("PREDMET_ID");
        }
    }
}
