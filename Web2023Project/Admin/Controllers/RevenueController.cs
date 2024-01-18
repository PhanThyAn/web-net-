using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web2023Project.Models;

namespace Web2023Project.Controllers.Admin
{
    public class RevenueController : PhoneController
    {
		private readonly ApiService _apiService = new ApiService(new HttpClient());
		public RevenueController()
        {
            this.level = 1;
        }
        // GET
		public async Task<ActionResult> Revenue()
		{
			ViewBag.Title = "Quản lí doanh thu";
			List<Donhang> listOrder = await _apiService.GetAsync<List<Donhang>>("Donhangs");
			return View(listOrder);
		}
	}
}