using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Product_manage_RAHUL_SATA.Models;

namespace Product_manage_RAHUL_SATA.Controllers
{
    public class Product_TableController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product_Table
        public ActionResult Index()
        {
            var p = db.Product_Table.Where(x => x.Userid == User.Identity.Name);
            return View(p.ToList());
        }

        // GET: Product_Table/Details/5
        public ActionResult Details(string id)
        {
            Debug.WriteLine("Id is : " + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Table product_Table = db.Product_Table.Find(id);
            Debug.WriteLine(product_Table.Name);
            if (product_Table == null)
            {
                return HttpNotFound();
            }
            return View(product_Table);
        }

        // GET: Product_Table/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product_Table/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product_Table product_Table, HttpPostedFileBase small_img, HttpPostedFileBase large_img)
        {
            if (ModelState.IsValid)
            {
                string filePath1=null,filePath2=null;
                string k=null;
                if (small_img != null)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(small_img.FileName);
                    string extension = System.IO.Path.GetExtension(small_img.FileName);
                    filePath1 = "~/Small_Imgs/" + fileName + DateTime.Now.ToString("yyMMddssffff") + extension;
                    small_img.SaveAs(Server.MapPath(filePath1));
                }
                if (large_img != null)
                {
                    string fileName2 = System.IO.Path.GetFileNameWithoutExtension(large_img.FileName);
                    string extension = System.IO.Path.GetExtension(large_img.FileName);
                    filePath2 = "~/Large_Imgs/" + fileName2 + DateTime.Now.ToString("yyMMddssffff") + extension;
                    large_img.SaveAs(Server.MapPath(filePath2));
                }
                else {
                    filePath2 = "/Large_Imgs/dont_delete/no_img.png";
                }
                if (product_Table.Long_description == null) { k = "----------"; }
                else { k = product_Table.Long_description; }
                var product = new Product_Table
                {
                    Id = DateTime.Now.ToString("yyyyMMddssffffyyMMdd"),
                    Userid = User.Identity.Name,
                    Name = product_Table.Name,
                    Category = product_Table.Category,
                    Price = product_Table.Price,
                    Quantity = product_Table.Quantity,
                    Short_description= product_Table.Short_description,
                    Long_description= k,
                    Small_img = filePath1,
                    Large_img = filePath2
                };
                
                db.Product_Table.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_Table);
        }

        // GET: Product_Table/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Table product_Table = db.Product_Table.Find(id);
            if (product_Table == null)
            {
                return HttpNotFound();
            }
            return View(product_Table);
        }

        // POST: Product_Table/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product_Table product_Table,String Small_path, String Large_path, HttpPostedFileBase Small_img, HttpPostedFileBase Large_img)
        {
            
            String filePath1 = Small_path;
            if (Small_img != null)
            {
                
                string fileName = System.IO.Path.GetFileNameWithoutExtension(Small_img.FileName);
                string extension = System.IO.Path.GetExtension(Small_img.FileName);
                filePath1 = "~/Small_Imgs/" + fileName + DateTime.Now.ToString("yyMMddssffff") + extension;
                Small_img.SaveAs(Server.MapPath(filePath1));
            }
            String filePath2 = Large_path;
            if (Large_img != null)
            {
                string fileName2 = System.IO.Path.GetFileNameWithoutExtension(Large_img.FileName);
                string extension = System.IO.Path.GetExtension(Large_img.FileName);
                filePath2 = "~/Large_Imgs/" + fileName2 + DateTime.Now.ToString("yyMMddssffff") + extension;
                Large_img.SaveAs(Server.MapPath(filePath2));
            }
            if (ModelState.IsValid)
            {
                // db.Entry(product_Table).State = EntityState.Modified;
                var product = db.Product_Table.Single(u => u.Id == product_Table.Id);
                product.Name = product_Table.Name;
                product.Category = product_Table.Category;
                product.Price = product_Table.Price;
                product.Quantity = product_Table.Quantity;
                product.Short_description = product_Table.Short_description;
                product.Long_description = product_Table.Long_description;
                product.Small_img = filePath1;
                product.Large_img = filePath2;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_Table);
        }

        // GET: Product_Table/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Table product_Table = db.Product_Table.Find(id);
            if (product_Table == null)
            {
                return HttpNotFound();
            }
            return View(product_Table);
        }

        [HttpPost]
        public ActionResult delete_all(FormCollection formCollection)
        {
            if (formCollection["ID"] == null) {
                return RedirectToAction("Index");            
            }
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            foreach (string id in ids)
            {
                var product = this.db.Product_Table.Find(id);
                this.db.Product_Table.Remove(product);
                this.db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // POST: Product_Table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product_Table product_Table = db.Product_Table.Find(id);
            db.Product_Table.Remove(product_Table);
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
    }
}
