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
using PhoneBookWebApi.Models;
using System.Web.Http.Cors;

namespace PhoneBookWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DirectoriesController : ApiController
    {
        private PhoneBookEntities db = new PhoneBookEntities();

        // GET: api/Directories
        public IQueryable<Directory> GetDirectories()
        {
            return db.Directories;
        }

        // GET: api/Directories/5
        [ResponseType(typeof(Directory))]
        public IHttpActionResult GetDirectory(int id)
        {
            Directory directory = db.Directories.Find(id);
            if (directory == null)
            {
                return NotFound();
            }

            return Ok(directory);
        }

        // PUT: api/Directories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDirectory(int id, Directory directory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != directory.Id)
            {
                return BadRequest();
            }

            db.Entry(directory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Directories
        [ResponseType(typeof(Directory))]
        public IHttpActionResult PostDirectory(Directory directory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Directories.Add(directory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = directory.Id }, directory);
        }

        // DELETE: api/Directories/5
        [ResponseType(typeof(Directory))]
        public IHttpActionResult DeleteDirectory(int id)
        {
            Directory directory = db.Directories.Find(id);
            if (directory == null)
            {
                return NotFound();
            }

            db.Directories.Remove(directory);
            db.SaveChanges();

            return Ok(directory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DirectoryExists(int id)
        {
            return db.Directories.Count(e => e.Id == id) > 0;
        }
    }
}