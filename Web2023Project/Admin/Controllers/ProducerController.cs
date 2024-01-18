using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web2023Project.Models;


namespace Web2023Project.Controllers.Admin
{
	public class ProducerController : PhoneController
	{
		private readonly ApiService _apiService = new ApiService(new HttpClient());

		public ProducerController()
		{
			this.level = 1;
		}
		// GET 
		[HttpGet]
		public async Task<ActionResult> Producer_Update(string ProducerID)
		{
			ViewBag.Title = "Cập nhật nhà cung cấp";
			Nhacungcap producer = await _apiService.GetAsync<Nhacungcap>($"Nhacungcaps/{ProducerID}");
			return View(producer);
		}
		// GET ALL
		public async Task<ActionResult> Producer()
		{
			ViewBag.Title = "Nhà cung cấp";
			List<Nhacungcap> listProducer = await _apiService.GetAsync<List<Nhacungcap>>("Nhacungcaps");
			return View(listProducer);
		}
		// DELETE
		[ActionName("Producer_Delete")]
		public async Task<ActionResult> Producer_Manage(string ProducerID)
		{
			bool remove = await _apiService.DeleteAsync($"Nhacungcaps/{ProducerID}");
			if (remove)
			{
				Session.Add("dia-log", "sucXóa Thành Công");
			}
			return RedirectToAction("Producer");
		}

		// PUT & POST
		[ValidateInput(false)]
		[HttpPost]
		public async Task<ActionResult> Producer_Update(string producerName, string picture, string producerStatus)
		{
			if (ModelState.IsValid)
			{
				string action = Request["action"];
				string producerID = Request["producerID"];
				sbyte trangThaiValue = (sbyte)(sbyte.TryParse(producerStatus, out sbyte parsedtrangThai) ? parsedtrangThai : 0);
				string tepHinhAnh = null; // base
				if (!picture.StartsWith("http", StringComparison.OrdinalIgnoreCase))
				{
					string physicalPath = Server.MapPath(picture);
					if (System.IO.File.Exists(physicalPath))
					{
						byte[] imageBytes = System.IO.File.ReadAllBytes(physicalPath);
						tepHinhAnh = Convert.ToBase64String(imageBytes);
					}
				}
				if (action.Equals("edit"))
				{
					int id = 0;
					if (producerID != null)
					{
						id = Int32.Parse(producerID);

					}
					//Nhacungcap nccUpdate = new Nhacungcap(id, producerName, producerAddress, trangThaiValue);
					Nhacungcap nhacungcap = await _apiService.GetAsync<Nhacungcap>($"Nhacungcaps/{id}");
					nhacungcap.TenNcc = producerName;
					nhacungcap.Trangthai = trangThaiValue;
					nhacungcap.TepHinhAnh = tepHinhAnh;
					nhacungcap.TenTepHinhAnh = picture;
					if (nhacungcap != null)
					{
						bool checkPutNcc = await _apiService.PutAsync<Nhacungcap>($"Nhacungcaps/{id}", nhacungcap);
						Session.Add("dia-log", checkPutNcc ? "sucSửa Thành Công" : "errThất Bại! Không tồn tại Nhà cung cấp.");
					}
				}
				else if (action.Equals("add"))
				{
					Nhacungcap nccCreate = new Nhacungcap(producerName, "", trangThaiValue, tepHinhAnh, picture);
					Nhacungcap nhacungcap = await _apiService.PostAsync<Nhacungcap>("Nhacungcaps", nccCreate);
					Session.Add("dia-log", (nhacungcap != null) ? "sucThêm mới nhà cung cấp thành Công" : "errThất Bại! Nhà cung cấp thêm không thành công.");
				}
			}

			return RedirectToAction("Producer");
		}

		public ActionResult Producer_Update()
		{
			return View();
		}
	}
}