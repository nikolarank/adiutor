﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Database.Entiteti;
using Database;
using NHibernate.Linq;
using Business.DTO;

namespace Business.DataAccess
{
    public static class Predmeti
    {

        static public void Obrisi(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();


                Predmet st = s.Load<Predmet>(id);

                s.Delete(st);
                s.Flush();
                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static int Dodaj(PredmetDTO c)
        {
            int ret = 0;
            try
            {
                ISession s = DataLayer.GetSession();

                Korisnik prof = new Korisnik()
                {
                    Id = c.ZaduzenId
                };

                Predmet pre = new Predmet()
                {
                    GodinaStudija = c.GodinaStudija,
                    Naziv = c.Naziv,
                    Semestar = c.Semestar,
                    Opis = c.Opis,
                    ZaduzeniProfesor = prof
                };

                s.Save(pre);
                ret = pre.Id;
                s.Flush();
                s.Close();

                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ret;
            }
        }

        static public PredmetDTO Procitaj(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Predmet p = s.Load<Predmet>(id);

                PredmetDTO pre = new PredmetDTO()
                {
                    Id = p.Id,
                    GodinaStudija = p.GodinaStudija,
                    Naziv = p.Naziv,
                    Semestar = p.Semestar,
                    Opis = p.Opis
                };

                if (p.ZaduzeniProfesor != null)
                {
                    pre.ZaduzenId = p.ZaduzeniProfesor.Id;
                }
                else
                {
                    pre.ZaduzenId = 0;
                }
                s.Flush();
                s.Close();

                return pre;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        static public void Izmeni(PredmetDTO c)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Korisnik prof = new Korisnik()
                {
                    Id = c.ZaduzenId
                };
                Predmet pre = new Predmet()
                {
                    Id = c.Id,
                    GodinaStudija = c.GodinaStudija,
                    Naziv = c.Naziv,
                    Semestar = c.Semestar,
                    Opis = c.Opis
                };

                if(prof.Id != 0)
                {
                    pre.ZaduzeniProfesor = prof;
                }
                else
                {
                    pre.ZaduzeniProfesor = null;
                }


                s.Update(pre);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        static public List<PredmetDTO> VratiSve()
        {
            try
            {
                ISession s = DataLayer.GetSession();


                List<Predmet> Predmeti = (from k in s.Query<Predmet>()
                                            select k).ToList<Predmet>();

                List<PredmetDTO> retVal = new List<PredmetDTO>();

                foreach (Predmet p in Predmeti)
                {
                    PredmetDTO pre = new PredmetDTO()
                    {
                        Id = p.Id,
                        GodinaStudija = p.GodinaStudija,
                        Naziv = p.Naziv,
                        Semestar = p.Semestar,
                        Opis = p.Opis
                    };

                    if (p.ZaduzeniProfesor != null)
                    {
                        pre.ZaduzenId = p.ZaduzeniProfesor.Id;
                    }
                    else
                    {
                        pre.ZaduzenId = 0;
                    }
                    retVal.Add(pre);
                }

                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        //static public List<PredmetDTO> VratiSve(int smerID)
        //{
        //    try
        //    {
        //        ISession s = DataLayer.GetSession();


        //        List<Predmet> Predmeti = (from k in s.Query<Predmet>()
        //                                  select k).ToList<Predmet>();

        //        List<PredmetDTO> retVal = new List<PredmetDTO>();

        //        foreach (Predmet p in Predmeti)
        //        {
        //            PredmetDTO pre = new PredmetDTO()
        //            {
        //                Id = p.Id,
        //                GodinaStudija = p.GodinaStudija,
        //                Naziv = p.Naziv,
        //                Semestar = p.Semestar,
                        
        //            };

        //            if (p.ZaduzeniProfesor != null)
        //            {
        //                pre.ProfesorId = p.ZaduzeniProfesor.Id;
        //            }
        //            else
        //            {
        //                pre.ProfesorId = 0;
        //            }

        //            retVal.Add(pre);
        //        }

        //        return retVal;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return null;
        //    }
        //}

        public static void DodajZaduzenog(int predmetId, int profesorId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Predmet predmet = s.Load<Predmet>(predmetId);
                Korisnik profesor = s.Load<Korisnik>(profesorId);
                predmet.ZaduzeniProfesor = profesor;

                s.Update(predmet);
                s.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void IzbrisiZaduzenog(int Id, ProfesorDTO p)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                PredmetDTO pr = Procitaj(Id);
                pr.ZaduzenId = 0;
                Izmeni(pr);
                s.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //static public IList<Smer> VratiSveSmerove(int PredmetId)
        //{
        //    try
        //    {
        //        ISession s = DataLayer.GetSession();
        //        Predmet p = s.Load<Predmet>(PredmetId);

        //        //List<Smer> smerovi = (from smer in s.Query<Smer>()
        //        //                            where (smer.ImaPredmete.Contains(p))
        //        //                            select smer).ToList<Smer>();

        //        return p.PripadaSmerovima;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return null;
        //    }
        //}

        static public List<PredmetDTO> VratiSvePredmete(int SmerId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                List<PredmetDTO> retVal = new List<PredmetDTO>();


                Smer t = s.Load<Smer>(SmerId);
                IList<Predmet> predmeti = t.ImaPredmete;

                foreach (Predmet predmet in predmeti)
                {
                    PredmetDTO dto = new PredmetDTO()
                    {
                        Id = predmet.Id,
                        Naziv = predmet.Naziv,
                        Semestar = predmet.Semestar,
                        GodinaStudija = predmet.GodinaStudija,
                        Opis = predmet.Opis
                    };
                    if (predmet.ZaduzeniProfesor != null)
                    {
                        dto.ZaduzenId = predmet.ZaduzeniProfesor.Id;
                    }
                    else
                    {
                        dto.ZaduzenId = 0;
                    }

                    retVal.Add(dto);
                }

                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

    }
}
