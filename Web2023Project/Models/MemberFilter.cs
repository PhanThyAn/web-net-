using System;
using System.Web;
using System.Web.Mvc;
using MySqlX.XDevAPI;
using Web2023Project.Controllers.Admin;
using Web2023Project.Model;

namespace Web2023Project.Models
{
    public class MemberFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            PhoneController phoneController = filterContext.Controller as PhoneController;
            Nguoidung member = HttpContext.Current.Session["memberLogin"] as Nguoidung;
            bool isOK = false;
            if (phoneController.level == 1)
            {
                if (member != null)
                {
                    string actionName = filterContext.ActionDescriptor.ActionName;
                    string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                    if (member.Quyen == 1)
                    {
						//foreach (Role role in member.Roles)
						//{
						//    if (role.containsInRole(actionName, controllerName)) isOK = true;
						//}
						isOK = true;
					}

                    //if (isOK == false)
                    //{
                    //    HttpContext.Current.Session.Add("messageFilter", "Bạn không thể thực hiện chức năng này.");
                    //    phoneController.HttpContext.Response.Redirect("/Admin/Index_Admin");
                    //}
                }
					if (isOK == false) phoneController.HttpContext.Response.Redirect("/Home");

			}

            base.OnActionExecuting(filterContext);
        }
    }
}