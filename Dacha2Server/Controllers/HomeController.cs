using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dacha2Server.Models;

namespace Dacha2Server.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetDashboard()
        {
            var model = new DashboardModel();
            model.Load();
            return model.ToString();
        }

        [HttpGet]
        public string GetBsmtData()
        {
            var model = new BasementModel();
            model.Load();
            return model.ToString();
        }

        [HttpGet]
        public string SetBsmtData(string q)
        {
            var model = new BasementModel();
            model.FromString(q);
            model.Save();
            return "";
        }

        [HttpGet]
        public string GetBsmtSettings()
        {
            var model = new BasementSettingsModel();
            model.Load();
            return model.ToString();
        }

        [HttpGet]
        public string SetBsmtSettings(string q)
        {
            var model = new BasementSettingsModel();
            model.FromString(q);
            model.Save();
            return "";
        }

        [HttpGet]
        public string SetBsmtApplied()
        {
            var model = new BasementSettingsModel();
            model.SetApllied();
            return "";
        }

        [HttpGet]
        public string GetWtrnData()
        {
            var model = new WateringModel();
            model.Load();
            return model.ToString();
        }

        [HttpGet]
        public string SetWtrnData(string q)
        {
            var model = new WateringModel();
            model.FromString(q);
            model.Save();
            return "";
        }

        [HttpGet]
        public string GetWtrnSettings()
        {
            var model = new WateringSettingsModel();
            model.Load();
            return model.ToString();
        }

        [HttpGet]
        public string SetWtrnSettings(string q)
        {
            var model = new WateringSettingsModel();
            model.FromString(q);
            model.Save();
            return "";
        }

        [HttpGet]
        public string SetWtrnApplied()
        {
            var model = new WateringSettingsModel();
            model.SetApllied();
            return "";
        }

        [HttpGet]
        public string GetWtrnTuneSettings(string q)
        {
            var model = new WateringTuneSettingsModel();
            model.ZoneNumber = int.Parse(q[0].ToString());
            model.Load();
            return model.ToString();
        }

        [HttpGet]
        public string SetWtrnTuneSettings(string q)
        {
            var model = new WateringTuneSettingsModel();
            model.FromString(q);
            model.Save();
            return "";
        }

        [HttpGet]
        public string SetWtrnTuneApplied(string q)
        {
            var model = new WateringTuneSettingsModel();
            model.ZoneNumber = int.Parse(q[0].ToString());
            model.SetApllied();
            return "";
        }

    }
}