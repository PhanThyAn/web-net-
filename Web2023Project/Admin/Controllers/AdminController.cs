﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Web2023Project.Admin.Dao;
 using Web2023Project.DAO;
 using Web2023Project.Model;
using Web2023Project.Utils;
using Web2023Project.Website.Dao;
using NewsDAO = Web2023Project.Admin.Dao.NewsDAO;

namespace Web2023Project.Controllers.Admin
{
    public class AdminController : PhoneController
    {
        public AdminController()
        {
            this.level = 1;
        }

        public ActionResult Index_Admin()
        {
            return View();
        }
    }
}