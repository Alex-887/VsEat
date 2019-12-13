﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using DataTransferObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebVSEat.Controllers
{
    public class CustomerController : Controller
    {
      
        private ICustomerManager CustomerManager { get; }

        public CustomerController(ICustomerManager customerManager)
        {
            CustomerManager = customerManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Customer cus)
        {
            Customer customer = CustomerManager.GetCustomerByUsernamePassword(cus.login, cus.password);

            if (customer != null)
            {
                HttpContext.Session.SetInt32("id", customer.IdCustomer);
                HttpContext.Session.SetString("name", customer.name);
                return RedirectToAction("Index", "Restaurant");
            }
            else
            {
                return RedirectToAction("Customer","Login");
            }
        }






        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

       
    }
}