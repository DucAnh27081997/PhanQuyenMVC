using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AppEnglishTest.Areas.administrator.Models.DataModel;

namespace AppEnglishTest.Areas.adminstrator.Controllers
{
    public class SessionTestsController : ApiController
    {
        public ShopDBContext db = new ShopDBContext();

        // GET: api/SessionTests
        //public IQueryable<SessionTest> GetSessionTests()
        //{
        //    return db.SessionTests.ToList();
        //}

        //// GET: api/SessionTests/5
        //[ResponseType(typeof(SessionTest))]
        //public IHttpActionResult GetSessionTest(int id)
        //{
        //    SessionTest sessionTest = db.SessionTests.Find(id);
        //    if (sessionTest == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(sessionTest);
        //}

        //// PUT: api/SessionTests/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutSessionTest(int id, SessionTest sessionTest)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != sessionTest.SessionTestID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(sessionTest).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SessionTestExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/SessionTests
        //[ResponseType(typeof(SessionTest))]
        //public IHttpActionResult PostSessionTest(SessionTest sessionTest)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.SessionTests.Add(sessionTest);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = sessionTest.SessionTestID }, sessionTest);
        //}

        //// DELETE: api/SessionTests/5
        //[ResponseType(typeof(SessionTest))]
        //public IHttpActionResult DeleteSessionTest(int id)
        //{
        //    SessionTest sessionTest = db.SessionTests.Find(id);
        //    if (sessionTest == null)
        //    {
        //        return NotFound();
        //    }

        //    db.SessionTests.Remove(sessionTest);
        //    db.SaveChanges();

        //    return Ok(sessionTest);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool SessionTestExists(int id)
        //{
        //    return db.SessionTests.Count(e => e.SessionTestID == id) > 0;
        //}
    }
}