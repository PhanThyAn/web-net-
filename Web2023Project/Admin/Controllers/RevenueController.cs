using System.Web.Mvc;

namespace Web2023Project.Controllers.Admin
{
    public class RevenueController : PhoneController
    {
        public RevenueController()
        {
            this.level = 1;
        }
        // GET
        public ActionResult Revenue()
        {
            return View();
        }
    }
}