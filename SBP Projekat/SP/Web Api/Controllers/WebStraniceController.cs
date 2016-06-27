﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using SP.Data_Access;
using Studentski_projekti.Entiteti;
using SP;
using NHibernate;

namespace Test.Controllers
{
    public class WebStraniceController : ApiController
    {
        ISession sesija = DataLayer.GetSession();

        // GET api/clanak
        public IEnumerable<WebStranice> Get()
        {
            IEnumerable<WebStranice> WebStranicei = Crud<WebStranice>.ReturnAll(sesija);

            foreach (WebStranice WebStranice in WebStranicei)
            {
                WebStranice.PrakticniProjekat = null;
            }

            return WebStranicei;
        }

        // GET api/clanak/5
        public WebStranice Get(int id)
        {
            WebStranice WebStranice = Crud<WebStranice>.Read(sesija, id);
            WebStranice.PrakticniProjekat = null;
            return WebStranice;
        }

        // POST api/clanak
        public void Post([FromBody]WebStranice WebStranice)
        {
            Crud<WebStranice>.Create(sesija, WebStranice);
        }

        // PUT api/clanak/5
        public void Put(int id, [FromBody]WebStranice WebStranice)
        {
            Crud<WebStranice>.Update(sesija, WebStranice);
        }

        // DELETE api/clanak/5
        public void Delete(int id)
        {
            Crud<WebStranice>.Delete(sesija, id);
        }

    }
}
