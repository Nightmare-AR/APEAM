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
    public class SalesController : Controller
    {
        private int _iva = 16;
        private SaleManager _saleManager;
        private CustomerManager _customerManager;
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

        private CustomerManager CustomerManager
        {
            get
            {
                return _customerManager ?? new CustomerManager(HttpContext.GetOwinContext().Get<ApplicationDbContext>());
            }
            set
            {
                _customerManager = value;
            }
        }

        private SaleManager SaleManager
        {
            get
            {
                return _saleManager ?? new SaleManager(HttpContext.GetOwinContext().Get<ApplicationDbContext>());
            }
            set
            {
                _saleManager = value;
            }
        }

        // GET: Sales
        public async Task<ActionResult> Index()
        {
            var sales = await Task.Run(() => SaleManager.GetAll(null));
            return View(sales);
        }

        // GET: Sales/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = await Task.Run(() => SaleManager.Get(id.Value));
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public async Task<ActionResult> Create()
        {
            var customers = CustomerManager.GetAll(null);
            ViewBag.CustomerId = new SelectList(customers, "ID", "Name");

            Sale sale = new Sale();
            sale.IVA = _iva;
            sale.SaleDate = DateTime.Now;
            ViewData["Products"] = await Task.Run(() => ProductManager.GetAll(null));
            return View(sale);
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Folio,IVA,SaleDate,CustomerId,ItemLists")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                sale.TimeStamp = DateTime.Now;

                var args = await Task.Run(() => SaleManager.Save(sale));

                if (args.Success)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", args.Message);
            }
            var customers = CustomerManager.GetAll(null);
            ViewBag.CustomerId = new SelectList(customers, "ID", "Name", sale.CustomerId);
            ViewData["Products"] = await Task.Run(() => ProductManager.GetAll(null));
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = await Task.Run(() => SaleManager.Get(id.Value));
            if (sale == null)
            {
                return HttpNotFound();
            }

            var customers = CustomerManager.GetAll(null);
            ViewBag.CustomerId = new SelectList(customers, "ID", "Name", sale.CustomerId);
            ViewData["Products"] = await Task.Run(() => ProductManager.GetAll(null));
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Folio,IVA,SaleDate,CustomerId,ItemLists")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                Sale saleDb = await Task.Run(() => SaleManager.Get(sale.ID));
                saleDb.Folio = sale.Folio;
                saleDb.IVA = sale.IVA;
                saleDb.CustomerId = sale.CustomerId;
                saleDb.SaleDate = sale.SaleDate;
                saleDb.Uptime = DateTime.Now;

                var args = await Task.Run(() => SaleManager.Save(saleDb));

                if (args.Success)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", args.Message);
            }
            var customers = CustomerManager.GetAll(null);
            ViewBag.CustomerId = new SelectList(customers, "ID", "Name", sale.CustomerId);
            ViewData["Products"] = await Task.Run(() => ProductManager.GetAll(null));
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = await Task.Run(() => SaleManager.Get(id.Value));
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Sale sale = await Task.Run(() => SaleManager.Get(id));
            if (sale != null)
            {
                var args = await Task.Run(() => SaleManager.DeleteByID(id));

                if (args.Success)
                    return RedirectToAction("Index");

            };

            return RedirectToAction("Delete", new { id });
        }    
    }
}
