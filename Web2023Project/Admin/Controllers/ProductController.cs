using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web2023Project.Models;

namespace Web2023Project.Controllers.Admin
{
	public class ProductController : PhoneController
	{
		private readonly ApiService _apiService = new ApiService(new HttpClient());

		public ProductController()
		{
			this.level = 1;
		}
		// GET
		public async Task<ActionResult> Product_Manage()
		{
			ViewBag.Title = "Quản lí sản phẩm";
			ViewBag.ListProducer = await _apiService.GetAsync<List<Nhacungcap>>("Nhacungcaps");
			List<Sanpham> listProducts = await _apiService.GetAsync<List<Sanpham>>("Sanphams");
			return View(listProducts);
		}

		[ActionName("Product_Delete")]
		public async Task<ActionResult> Product_Manage(string IDProduct)
		{
			bool remove = await _apiService.DeleteAsync($"Sanphams/{IDProduct}");
			if (remove)
			{
				Session.Add("dia-log", "sucXóa Thành Công");
			}

			return RedirectToAction("Product_Manage");
		}

		[HttpGet]
		public async Task<ActionResult> Product_Update(string productID)
		{
			ViewBag.Title = "Cập nhật sản phẩm";
			ViewBag.ListProducer = await _apiService.GetAsync<List<Nhacungcap>>("Nhacungcaps");
			Sanpham sanpham = await _apiService.GetAsync<Sanpham>($"Sanphams/{productID}");
			return View(sanpham);
		}

		[ValidateInput(false)]
		[HttpPost]
		public async Task<ActionResult> Product_Update(string productName, int producer, string salePrice, string price, int amount, string picture,
	string color, string screen, string operatingSystem, string CAMERA, string CPU, string RAM,
	string memory, string PIN, string content, string tenviettat, string status)
		{
			ViewData["Title"] = "Cập nhật sản phẩm";
			string action = Request["action"];
			string productID = Request["productID"];
			if (!ModelState.IsValid)
			{
				Console.WriteLine("ModelState không hợp lệ!");
				Session.Add("dia-log", "errThất Bại! Sản phẩm " + (action.Equals("add") ? "Thêm" : "Sửa") + " không thành công.");
				return RedirectToAction("Product_Manage");
			}
			string tepHinhAnh = null; // base
			if(!picture.StartsWith("http", StringComparison.OrdinalIgnoreCase))
			{
				string physicalPath = Server.MapPath(picture);
				if (System.IO.File.Exists(physicalPath))
				{
					byte[] imageBytes = System.IO.File.ReadAllBytes(physicalPath);
					tepHinhAnh = Convert.ToBase64String(imageBytes);
				}
			}
			int id = 0;	
			if (productID != null)
			{
				id = Int32.Parse(productID);
				
			}
			sbyte trangThaiValue = (sbyte)(sbyte.TryParse(status, out sbyte parsedtrangThai) ? parsedtrangThai : 0);
			double prices = double.TryParse(price, out double parsedGiaGoc) ? parsedGiaGoc : 0;
			double salePrices = double.TryParse(salePrice, out double parsedGiaDaGiam) ? parsedGiaDaGiam : 0;
			if (action.Equals("edit"))
			{
				Sanpham spUpdate = new Sanpham(id, productName, producer, "", salePrices, prices, amount,
				color, screen, operatingSystem, CAMERA, CPU, RAM,
				memory, PIN, content, tenviettat, trangThaiValue,tepHinhAnh,picture);
				Sanpham sanpham = await _apiService.GetAsync<Sanpham>($"Sanphams/{id}");
				if (sanpham != null)
				{
					bool checkPutSp = await _apiService.PutAsync<Sanpham>($"Sanphams/{id}", spUpdate);
					//bool checkPutHa = await _apiService.PutAsync<Hinhanh>($"Hinhanh/{id}", spUpdate);
					Session.Add("dia-log", checkPutSp ? "sucSửa Thành Công" : "errThất Bại! Không tồn tại Sản phẩm.");
				}
			}
			else if (action.Equals("add"))
			{
				Sanpham spCreate = new Sanpham(productName, producer, "", salePrices, prices, amount,
					color, screen, operatingSystem, CAMERA, CPU, RAM,
					memory, PIN, content, tenviettat, trangThaiValue, tepHinhAnh, picture);
				Sanpham sanpham = await _apiService.PostAsync<Sanpham>("Sanphams", spCreate);
				//Hinhanh haCreate = new Hinhanh(sanpham.Id, picture);
				//Hinhanh hinhanh = await _apiService.PostAsync<Hinhanh>("Hinhanhs", haCreate);
				Session.Add("dia-log", (sanpham != null/* && hinhanh != null*/) ? "sucThêm mới sản phẩm thành Công" : "errThất Bại! Sản phẩm thêm không thành công.");
			}
			return RedirectToAction("Product_Manage");
		}
	}
}