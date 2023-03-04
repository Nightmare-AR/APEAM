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
    public class CustomersController : Controller
    {
        private CustomerManager _customerManager;

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

        // GET: Customers
        public async Task<ActionResult> Index()
        {
            var customers = await Task.Run(() => CustomerManager.GetAll(null));
            return View(customers);
        }

        // GET: Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await Task.Run(() => CustomerManager.Get(id.Value));
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,RFC,Email,Address,ZipCode,City,State,Country,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.TimeStamp = DateTime.Now;

                var args = await Task.Run(() => CustomerManager.Save(customer));

                if(args.Success)             
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", args.Message);
            }
            
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await Task.Run(() => CustomerManager.Get(id.Value));
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,RFC,Email,Address,ZipCode,City,State,Country,Phone")] Customer customer)
        {            
            if (ModelState.IsValid)
            {
                var customerDb = await Task.Run(() => CustomerManager.Get(customer.ID));
                    customerDb.Name = customer.Name;
                    customerDb.RFC = customer.RFC;
                    customerDb.Email = customer.Email;
                    customerDb.Address = customer.Address;
                    customerDb.ZipCode = customer.ZipCode;                
                    customerDb.City = customer.City;                
                    customerDb.State = customer.State;                
                    customerDb.Country = customer.Country;
                    customerDb.Phone = customer.Phone;                    
                    customerDb.Uptime = DateTime.Now;

                var args = await Task.Run(() => CustomerManager.Save(customerDb));

                if(args.Success)                
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError("", args.Message);
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await Task.Run(() => CustomerManager.Get(id.Value));
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await Task.Run(() => CustomerManager.Get(id));
            
            if(customer != null)
            {
                var args = await Task.Run(() => CustomerManager.DeleteByID(id));

                if(args.Success)
                    return RedirectToAction("Index");
            }

            return RedirectToAction("Delete", new { id });
        }

    }
}
