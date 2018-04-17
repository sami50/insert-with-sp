using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webwithsp.Models;

namespace Webwithsp.Controllers
{
    public class productsController : Controller
    {
        private ssEntities db = new ssEntities();

        // GET: products
        public ActionResult Index()
        {
            return View(db.products.ToList());
        }

        // GET: products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pid,aq,oq")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pid,aq,oq")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult order()
        {
           
            return View();
        }
        [HttpPost]
        public string sp1(int arg)
        {
            string cm = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                 SqlConnection con = new SqlConnection(cm);
            string ss;
            string ss2;
            try
            {
                SqlCommand cmd = new SqlCommand("one", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@neworderquantity", SqlDbType.Int).Value =arg;

                SqlParameter output = new SqlParameter();
                SqlParameter output2 = new SqlParameter();
                output.ParameterName = "@previousorderquantity";
                output.SqlDbType = SqlDbType.Int;
                output.Direction = ParameterDirection.Output;

                output2.ParameterName = "@previousavquantity";
                output2.SqlDbType =  SqlDbType.Int;
                output2.Direction = ParameterDirection.Output;

                //cmd.Parameters["@previousorderquantity"].Direction = ParameterDirection.Output;
                //cmd.Parameters["@previousavquantity"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                cmd.Parameters.Add(output2);

                con.Open();
                cmd.ExecuteNonQuery();
                ss = output.Value.ToString();
                ss2 = output2.Value.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            
                return "hello"+ss+" "+ ss2;
        }
    }
}
