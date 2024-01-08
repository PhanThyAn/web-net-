using System.Web.Mvc;

namespace Web2023Project.Controllers.Admin
{
    public class MemberController : PhoneController
    {
        public MemberController()
        {
            this.level = 1;
        }
        public ActionResult Member()
        {
            ViewBag.Title = "Thành Viên";
            return View();
        }

        public ActionResult Member_Update()
        {
            return View();
        }
    }
}