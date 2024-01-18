using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web2023Project.Models;

namespace Web2023Project.Controllers.Admin
{
    public class CommentController : PhoneController
    {
		private readonly ApiService _apiService = new ApiService(new HttpClient());

		public CommentController()
        {
            this.level = 1;
        }
		public async Task<ActionResult> Comment_Manage()
        {
            ViewBag.Title = "Quản lí bình luận";
            List<Binhluan> listComment = await _apiService.GetAsync<List<Binhluan>>("Binhluans");
			return View(listComment);
        }

        public ActionResult Comment_Update()
        {
            return View();
        }

        [ActionName("Delete_Comment")]
		public async Task<ActionResult> Comment_Manage(string id)
        {
			bool remove = await _apiService.DeleteAsync($"Binhluans/{id}");
			if (remove)
            {
                Session.Add("dia-log", "sucXóa Thành Công");
            }
            return RedirectToAction("Comment_Manage");
        }
    }
}