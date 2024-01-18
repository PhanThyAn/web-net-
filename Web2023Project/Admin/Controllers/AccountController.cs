using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using Web2023Project.Models;


namespace Web2023Project.Controllers.Admin
{
	public class AccountController : PhoneController
	{
		private readonly ApiService _apiService = new ApiService(new HttpClient());
		public AccountController()
		{
			this.level = 1;
		}
		public async Task<ActionResult> Account_Manage()
		{
			ViewBag.Title = "Quản lí tài khoản";
			List<Nguoidung> members = await _apiService.GetAsync<List<Nguoidung>>("Nguoidungs");
			return View(members);
		}

		[ActionName("Account_Delete")]
		public async Task<ActionResult> Account_Manage(string id) // note
		{
			bool remove = await _apiService.DeleteAsync($"Nguoidungs/{id}");
			if (remove)
			{
				Session.Add("dia-log", "sucXóa Thành Công");
			}

			return RedirectToAction("Account_Manage");
		}

		[HttpGet]
		public async Task<ActionResult> Account_Update(string id)
		{
			ViewBag.Title = "Cập nhật tài khoản";
			Nguoidung member = await _apiService.GetAsync<Nguoidung>($"Nguoidungs/{id}");
			return View(member);
		}
		// PUT & POST
		[HttpPost]
		public async Task<ActionResult> Account_Update(string email, string password, string name, string gender, string phone, string level)
		{
			if (ModelState.IsValid)
			{
				string action = Request["action"];
				string IDUser = Request["id"];
				ulong gioitinh = (ulong.TryParse(gender, out ulong parsedgioitinh) ? parsedgioitinh : 0);
				sbyte quyen = (sbyte)(sbyte.TryParse(level, out sbyte parsedlevel) ? parsedlevel : 0);
				if (action.Equals("edit"))
				{
					int id = 0;
					if (IDUser != null)
					{
						id = Int32.Parse(IDUser);

					}
					//Nguoidung nguoidungUpdate = new Nguoidung(id, name, phone, gioitinh, email, password, quyen);
					Nguoidung nguoidung = await _apiService.GetAsync<Nguoidung>($"Nguoidungs/{id}");
					nguoidung.Ten = name;
					nguoidung.Sdt = phone;
					nguoidung.Gioitinh = gioitinh;
					nguoidung.Email = email;
					nguoidung.Quyen = quyen;
					if (nguoidung != null)
					{
						bool checkPutNd = await _apiService.PutAsync<Nguoidung>($"Nguoidungs/{id}", nguoidung);
						Session.Add("dia-log", checkPutNd ? "sucSửa Thành Công" : "errThất Bại! Không tồn tại Người dùng.");
					}

				}
				else if (action.Equals("add"))
				{
					Nguoidung nguoidungCreate = new Nguoidung(name, phone, gioitinh, email, password, "", quyen);
					Nguoidung nguoidung = await _apiService.PostAsync<Nguoidung>("Register", nguoidungCreate);
					Session.Add("dia-log", (nguoidung != null) ? "sucThêm mới tài khoản thành Công" : "errThất Bại! Tài khoản thêm không thành công.");
				}
			}
			return RedirectToAction("Account_Manage");
		}
	}
}