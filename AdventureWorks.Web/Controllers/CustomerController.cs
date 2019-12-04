using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AdventureWorks.Domain;
using AdventureWorks.Domain.Models;
using AdventureWorks.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Web.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerManager _manager;

        public CustomerController(ICustomerManager manager)
        {
            _manager = manager;
        }

        [Route("zoeken")]
        public IActionResult Search()
        {
            SearchViewModel vm = new SearchViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Search(string keyword)
        {
            var customers = _manager.SearchCustomers(keyword)
                .Select(c => new CustomerViewModel()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email
                });
            return Json(customers);
        }

        // Tonen van details van een klant
        public IActionResult Details(int id)
        {
            Customer c = _manager.GetCustomer(id);
            if (c != null)
            {
                CustomerViewModel vm = new CustomerViewModel()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email
                };
                return View(vm);
            }
            else return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Customer c = new Customer()
                {
                    FirstName = collection["FirstName"],
                    LastName = collection["LastName"],
                    Email = collection["Email"]
                };

                _manager.InsertCustomer(c);

                return RedirectToAction(nameof(Search));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Customer c = _manager.GetCustomer(id);
            CustomerViewModel vm = new CustomerViewModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Customer c = _manager.GetCustomer(id);

                c.FirstName = collection["FirstName"];
                c.LastName = collection["LastName"];
                c.Email = collection["Email"];

                _manager.UpdateCustomer(c);

                return RedirectToAction(nameof(Search));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            Customer c = _manager.GetCustomer(id);
            CustomerViewModel vm = new CustomerViewModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email
            };

            return View(vm);
        }

        // POST: Test/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _manager.DeleteCustomer(id);

                return RedirectToAction(nameof(Search));
            }
            catch
            {
                return View();
            }
        }
    }
}