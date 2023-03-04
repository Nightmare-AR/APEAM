using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using APEAM.DataAccess;
using APEAM.Entities;
using APEAM.Logic;
using Microsoft.AspNet.Identity.Owin;

namespace APEAM.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ProductManager _productManager;

        private ProductManager ProductManager
        {
            get
            {
                return _productManager ?? new ProductManager(HttpContext.GetOwinContext().Get<ApplicationDbContext>());
            }
            set
            {
                _productManager = value;
            }
        }

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var products = await Task.Run(() => ProductManager.GetAll(null));
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await Task.Run(() => ProductManager.Get(id.Value));
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Description,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.TimeStamp = DateTime.Now;

                var args = await Task.Run(() => ProductManager.Save(product));

                if (args.Success)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", args.Message);
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await Task.Run(() => ProductManager.Get(id.Value));
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Description,Price")] Product product)
        {           
            if (ModelState.IsValid)
            {
                var productDb = await Task.Run(() => ProductManager.Get(product.ID));
                    productDb.Name = product.Name;
                    productDb.Description = product.Description;
                    productDb.Price = product.Price;
                    productDb.Uptime = DateTime.Now;

                var args = await Task.Run(() => ProductManager.Save(productDb));

                if (args.Success)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", args.Message);
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            Product product = await Task.Run(() => ProductManager.Get(id));
            if(product != null)
            {
                var args = await Task.Run(() => ProductManager.DeleteByID(id));

                if (args.Success)
                    return RedirectToAction("Index");

            }            

            return RedirectToAction("Delete", new { id });
        }
   
    }
}
