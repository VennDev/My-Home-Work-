using System.Web.Mvc;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MyHomeWork.IRepository;
using MyHomeWork.Models;
using MyHomeWork.Repository;

namespace MyHomeWork.Controllers
{
    
    public class CustomersController : Controller
    {
        
        private readonly ICustomersRepository _repository = null;
        
        public CustomersController()
        {
            _repository = new CustomersRepository();
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.listTable = _repository.GetCustomers();
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customers customer)
        {
            if (ModelState.IsValid && Request.Form["IdRequest"] == "0") _repository.AddCustomer(customer); else _repository.UpdateCustomer(customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetCustomer(string customerId)
        {
            return Json(_repository.GetCustomer(customerId), JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string customerId)
        {
            _repository.DeleteCustomer(customerId);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public string CheckCustomer(string customerId)
        {
            Customers cs = _repository.GetCustomer(customerId);
            return cs != null ? "true" : "false";
        }
        
    }
}