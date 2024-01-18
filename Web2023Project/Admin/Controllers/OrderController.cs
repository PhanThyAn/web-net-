using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web2023Project.Models;

namespace Web2023Project.Controllers.Admin
{
	public class OrderController : PhoneController
	{
		private readonly ApiService _apiService = new ApiService(new HttpClient());
		public OrderController()
		{
			this.level = 1;
		}
		// GET
		public async Task<ActionResult> Order_Manage()
		{
			ViewBag.Title = "Quản lí đơn hàng";
			List<Donhang> listOrder = await _apiService.GetAsync<List<Donhang>>("Donhangs");
			return View(listOrder);
		}
		[ActionName("Delete_Order")]
		public async Task<ActionResult> Order_Manage(string cartID)
		{
			bool remove = await _apiService.DeleteAsync($"Donhangs/{cartID}");
			if (remove)
			{
				Session.Add("dia-log", "sucXóa Thành Công");
			}
			return RedirectToAction("Order_Manage");
		}
		[ActionName("Update_Order")]
		public async Task<ActionResult> Update_Order(string cartID)
		{
			
			Donhang donhang = await _apiService.GetAsync<Donhang>($"Donhangs/{cartID}");
			donhang.Trangthai = 2;
			if (donhang != null)
			{
				bool update = await _apiService.PutAsync<Donhang>($"Donhangs/{cartID}", donhang);
				Session.Add("dia-log", update ? "sucSửa Thành Công" : "errThất Bại! Không tồn tại Đơn hàng.");
			}
			return RedirectToAction("Order_Manage");
		}
		// [HttpGet] //Phần này dùng để lấy ra đối tượng member để gán giá trị trong form update member nè
		// public ActionResult Order_Manage(string cartID)
		// {
		//     ViewBag.Title = "Quản lí đơn hàng";
		//
		//     return View();
		// }
	}
}