using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using System.Web.UI;

using System.Web.WebPages;
using Web2023Project.Admin.Dao;
using Web2023Project.Controllers.Admin;
using Web2023Project.Dao;
using Web2023Project.DAO;
using Web2023Project.Model;
using Web2023Project.Models;
using Web2023Project.Utils;
using Web2023Project.Website.Dao;
using Web2023Project.Website.Model;
using Binhluan = Web2023Project.Models.Binhluan;


namespace Web2023Project.Controllers
{
    public class HomeController : PhoneController
    {
        private readonly ProductDAO productDAO;
        private readonly ProductDetailDAO productDetailDAO;
        private readonly CommentDAO commentDAO;
        private readonly CartDAO cartDAO;
        private readonly FavouriteDAO favouriteDAO;

        public HomeController()
        {
            this.level = 0;
            this.productDAO = new ProductDAO();
            this.productDetailDAO = new ProductDetailDAO();
            this.commentDAO = new CommentDAO();
            this.cartDAO = new CartDAO();
            this.favouriteDAO = new FavouriteDAO();
        }

        public ActionResult Index()
        {
            var model = new HomeViewModel();

            model.listProduct_new = productDAO.GetNewProducts();
            model.listProduct_hot = productDAO.GetHotProducts();
            model.listProduct_sale = productDAO.GetSaleProducts();

            return View(model);
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Order_Success()
        {
            return View();
        }

        public ActionResult Guarentee()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("memberLogin");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Login(string userName, string password)
        {
            if (ModelState.IsValid)
            {
                Nguoidung nguoidung = await  LoginDao.login(userName, password);
                if (nguoidung != null)
                {
                   /* nguoidung.Quyen = LogDao.loadRolesByUserName(userName);*/
                    Session.Add("memberLogin", nguoidung);
                    LogDao.INFO("Tai khoan: " + nguoidung.Ten + ", email: " + nguoidung.Email, "Action: Login => DONE");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session.Add("errLogin", "Tên tài khoản hoặc mật khẩu không đúng");
                    LogDao.FAILEDLOGIN("Tai khoan: " + userName, "Action: Login ==> FAILED");
                    return RedirectToAction("Login", "Home");
                }
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public async Task<ActionResult>  Register(string password, string c_password, string name, string gender, string email, string phone)
        {

            if (ModelState.IsValid)
            {
                
                Nguoidung nguoidung = await LoginDao.register( password,  name, Int32.Parse(gender) ,  email,  phone);
                if (nguoidung != null)
                {
                    /* nguoidung.Quyen = LogDao.loadRolesByUserName(userName);*/
                    Session.Add("memberLogin", nguoidung);
                    LogDao.INFO("Tai khoan: " + nguoidung.Ten + ", email: " + nguoidung.Email, "Action: Login => DONE");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session.Add("errLogin", "Tên tài khoản hoặc mật khẩu không đúng");                 
                    return RedirectToAction("Register");
                }
            }
            else
            {
                return RedirectToAction("Register");
            }
        }
        [HttpPost]
        public async Task<ActionResult> ForgotPass(string email,string phone)
        {
           
            if (ModelState.IsValid)
            {

                Nguoidung nguoidung = await LoginDao.getInforFromChangePass(email, phone);
               
                if (nguoidung != null)
                {
                    /* nguoidung.Quyen = LogDao.loadRolesByUserName(userName);*/
                    Session.Add("forgotUser", nguoidung);
                  
                    return View("Reset_Password");
                }
                else
                {
                    Session.Add("errForgot", "Email và số điện thoại không đúng");
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Register");
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdatePass()
        {

            Web2023Project.Models.Nguoidung member = Session["forgotUser"] as Web2023Project.Models.Nguoidung;
           
            string newPass = Request["new-pass"];
            member.Matkhau = newPass;
            
            Task<bool> checkPass = Update_User_DAO.updateInfoUser(member);
            bool checkboolPass = await checkPass;
            if (checkboolPass)
            {
                    Task<Nguoidung> n = LoginDao.login(member.Sdt, member.Matkhau);
                    Nguoidung newUser = await n;
                    Session.Remove("memberLogin");
                    Session.Add("memberLogin", newUser);
                    return View("Profile_User");
                
            }
            else
            {
                return View("Reset_Password");
            }
           
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        public ActionResult Product(string producer, string active)
        {
            return View();
        }

        public ActionResult Category()
        {

            string category = Request["cate"];
            string sort = Request["type"];
            string page = Request["page"];
            if (page == null || page.Equals(""))
            {
                page = "1";
            }

            try
            {
                if (category != null)
                {
                    if (sort == null)
                    {
                        Session.Add("category",
                            CategoryDAO.findCateByProducer(category, null, Convert.ToInt32(page)));
                        Session.Add("producer", category);
                        Session.Add("active", null);
                        Session.Add("countPages", CategoryDAO.countOfCate(category));
                        Session.Add("page", page);
                    }
                    else
                    {
                        Session.Add("category",
                            CategoryDAO.findCateByProducer(category, sort, Convert.ToInt32(page)));
                        Session.Add("producer", category);
                        Session.Add("active", sort);
                        Session.Add("countPages", CategoryDAO.countOfCate(category));
                        Session.Add("page", page);
                    }
                    Session.Add("follow",
                        "DienThoai?cate=" + category + (sort != null ? "&type=" + sort : ""));
                    //  request.getRequestDispatcher("/product.jsp").forward(request, response);
                }
                else
                {
                    // response.sendRedirect("/error404.jsp");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View("Product");
        }
        public async Task<ActionResult> GetProductImage(int productId)
        {
            Image image = await productDAO.GetProductImage(productId);

            return View(image);
        }

        public async Task<ActionResult> Product_Detail(string tenviettat)
        {
            ProductShow product = await productDetailDAO.GetProductByShortenWord(tenviettat);

            if (product != null)
            {
                Session.Add("productDetail", product);
                return View(product);
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult FavoriteProduct()
        {
            Nguoidung member = Session["memberLogin"] as Nguoidung;

            if (member != null)
            {
                List<Sanphams> favoriteProducts = favouriteDAO.GetFavoriteProducts(member.Id);
                Session["favorite"] = favoriteProducts;

                Session["addedToFavorites"] = false;

                return View(favoriteProducts);
            }

            return RedirectToAction("Error404");
        }

        public ActionResult AddToFavorites()
        {
            Yeuthich favoriteProduct = new Yeuthich();
            Sanphams sanpham = new Sanphams();
            Nguoidung member = Session["memberLogin"] as Nguoidung;
            ProductShow product = Session["productDetail"] as ProductShow;

            if (member != null && product != null)
            {
                favoriteProduct.IdNd = member.Id;
                favoriteProduct.IdSp = product.ThongTin.Id;

                sanpham.Id = product.ThongTin.Id;
                sanpham.TenSp = product.ThongTin.TenSp;
                sanpham.GiaGoc = product.ThongTin.GiaGoc;
                sanpham.GiaDagiam = product.ThongTin.GiaDagiam;

                foreach (var pd in product.ThongTin.Hinhanhs)
                {
                    foreach (var sp in sanpham.Hinhanhs)
                    {
                        sp.Url = pd.Url;
                    }
                }

                favouriteDAO.AddProduct(sanpham);
                favouriteDAO.AddFavoriteProduct(favoriteProduct);

                Session["addedToFavorites"] = true;

                return RedirectToAction("FavoriteProduct");
            }

            return RedirectToAction("Index");
        }

        public ActionResult ViewFavorites()
        {
            Nguoidung member = Session["memberLogin"] as Nguoidung;
            ProductShow product = Session["productDetail"] as ProductShow;

            bool addedToFavorites = Session["addedToFavorites"] != null ? (bool)Session["addedToFavorites"] : false;

            if (member != null && product != null && addedToFavorites)
            {
                return RedirectToAction("FavoriteProduct");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Cart_Process()
        {
            string id = Request["id"];
            int soluong = 1;
            string update = Request["update"];
            String msp;
            if (id != null)
            {
                msp = id;
                Sanphams sp = null;
                try
                {
                    sp = productDAO.GetProductById(Convert.ToInt32(msp.Trim()));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                if (sp != null)
                {
                    Cart gh = (Cart)Session["giohang"];
                    if (Request["soluong"] != null)
                    {
                        soluong = Convert.ToInt32(Request["soluong"]);
                        // if (soluong <= 1) soluong = 1;
                        if (gh == null)
                        {
                            gh = new Cart();
                            gh.CartStatus = 1;
                            List<Item> items = new List<Item>();
                            Item item = new Item();
                            item.Price = sp.GiaDagiam;
                            item.Product = sp;
                            item.Amount = soluong;
                            item.ItemId = sp.Id;
                            items.Add(item);
                            gh.Item = items;
                        }
                        else
                        {
                            List<Item> items = gh.Item;
                            bool check = false;

                            foreach (Item i in items)
                            {
                                if (i.Product.Id == sp.Id)
                                {
                                    if (update == null && soluong == 1)
                                    {
                                        i.Amount = (i.Amount + soluong);
                                    }
                                    else
                                    {
                                        i.Amount = soluong;
                                    }

                                    check = true;
                                }
                            }

                            if (!check)
                            {
                                Item item = new Item();
                                item.Price = sp.GiaDagiam;
                                item.Product = sp;
                                item.Amount = soluong;
                                item.ItemId = sp.Id;
                                items.Add(item);
                            }
                        }
                    }
                    else
                    {
                        //xoa 1 item ra gio hang
                        List<Item> items = gh.Item;
                        foreach (Item item in items)
                        {
                            if (item.Product.Id == sp.Id)
                            {
                                items.Remove(item);
                                break;
                            }
                        }
                    }

                    Session.Add("giohang", gh);
                    Cart gh_respone = (Cart)Session["giohang"];
                    HttpContext.Response.Write(gh_respone.TotalItem() + "-" +
                                               string.Format("{0:0,0}", gh_respone.TotalPrice()) + "đ");
                }
            }

            return null;
        }

        public async Task<ActionResult> CommentUser()
        {
            Binhluan comment = new Binhluan();
            ProductShow product = Session["productDetail"] as ProductShow;

            comment.IdNd = Convert.ToInt32(Request.Form["IdNd"]);
            comment.IdSp = Convert.ToInt32(Request.Form["IdSp"]);
            comment.Noidung = Request.Form["Noidung"];
            comment.Danhgia = Convert.ToInt32(Request.Form["Danhgia"]);
            comment.Ngaybinhluan = DateTime.Now;
            comment.Trangthai = 0;

            if (comment.Noidung != null && comment.IdNd != 0 && comment.IdSp != 0)
            {
                bool result = await commentDAO.InsertCMT(comment);
                if (result)
                {
                    return RedirectToAction("Product_Detail", "Home", new
                    {
                        tenviettat = product.TenVietTat
                    });
                }
            }
            else
            { 
                Session.Add("messagecomment", "Nội dung không được bỏ trống");

                return RedirectToAction("Product_Detail", "Home", new
                {
                    tenviettat = product.TenVietTat
                });
            }
            return RedirectToAction("Product_Detail", "Home", new
            {
                tenviettat = product.TenVietTat
            });
        }

        public async Task<ActionResult> SearchKey(string key, int page = 1)
        {
            if (key != null)
            {
                try
                {
                    StringBuilder resp = new StringBuilder();

                    // lấy ra danh sách sản phẩm dựa vào key đó                  
                    Task<List<Sanphams>> task = SearchDAO.SeachTest(key);
                    List<Sanphams> dsct_sp = await task;
                    int pageSize = 4;
                    int skip = (page - 1) * pageSize;
                    List<Sanphams> dsct_sp_page = dsct_sp.Skip(skip).Take(pageSize).ToList();

                    Session.Add("pageCurrent", page);
                    Session.Add("category", dsct_sp_page);
                    // đếm tổng số sản phẩm lấy ra được
                    int count_sear = 0;
                    int totalPage = 1;
                    if (dsct_sp != null)
                    {
                        count_sear = dsct_sp.Count;
                        // tính tổng số trang
                        totalPage = count_sear / pageSize + 1;
                    }
                    Session.Remove("sort");

                    Session.Add("pageSize", totalPage);
                    Session.Add("count_sear", count_sear);
                    Session.Add("key", key);


                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return RedirectToAction("Product", "Home");
        }

        public async Task<ActionResult> NavigateAndSortPage(string key, int page = 1,string sort = "")
        {
            if (key != null)
            {
                try
                {
                    // lấy ra danh sách sản phẩm dựa vào key đó
                    // Gọi phương thức asynchronous và đợi kết quả
                    StringBuilder resp = new StringBuilder();
                    Task<List<Sanphams>> task = SearchDAO.SeachTest(key);
                    List<Sanphams> dsct_sp = await task;
                    
                    if (sort.Equals("desc")) {
                        dsct_sp = dsct_sp.OrderByDescending(p => p.GiaDagiam).ToList();
                        Session.Add("sort", sort);
                    }
                    else if (sort.Equals("asc"))
                    {
                        dsct_sp = dsct_sp.OrderBy(p => p.GiaDagiam).ToList();
                        Session.Add("sort", sort);
                    }
                    int pageSize = 4;
                    int skip = (page - 1) * pageSize;
                    List<Sanphams> dsct_sp_page = dsct_sp.Skip(skip).Take(pageSize).ToList();
                    
                    Session.Add("pageCurrent", page);
                    Session.Add("category", dsct_sp_page);
                    // đếm tổng số sản phẩm lấy ra được
                    int count_sear = 0;
                    int totalPage = 1;
                    if (dsct_sp != null)
                    {
                        count_sear = dsct_sp.Count;
                        totalPage = count_sear / pageSize + 1;
                    }
                 
                    Session.Add("pageSize", totalPage);
                    Session.Add("count_sear", count_sear);
                    Session.Add("key", key);
                 

                    return PartialView("SearchPagePartial", dsct_sp_page);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }                      

            return RedirectToAction("Product", "Home");
        }

        [HttpPost]

        public async Task<ActionResult> Search()
        {
            // láy ra data input
            string input = Request["input"];
            StringBuilder resp = new StringBuilder();
            String giaBan;
            if (input != null && !input.Equals(""))
            {
                try
                {
                    // Lấy ra danh sách kết quả
                    Task<List<Sanphams>> task = SearchDAO.SeachTest(input);
                    List<Sanphams> dssp = await task;


                    if (dssp != null)
                    {
                        foreach (Sanphams sp in dssp)
                        {
                            if (sp.GiaDagiam != 0)
                            {
                                giaBan = string.Format("{0:0,0}", sp.GiaDagiam);
                            }
                            else
                            {
                                giaBan = "";
                            }
                            // viết giá trị của dữ liệu vào thẻ li bằng stringbuilder
                            resp.Append("<li><a href='/Home/Product_Detail?id=").Append(sp.Id).Append("'")
                                .Append(">")
                                .Append("<h3>").Append(sp.TenSp).Append("</h3>")
                                .Append("<span class='price'>").Append(giaBan)
                                .Append("đ").Append("</span>").Append("<cite>").Append(string.Format("{0:0,0}", sp.GiaGoc + "đ")).Append("</cite>")
                                .Append("</a></li>");
                        }

                        HttpContext.Response.Write(resp);
                    }
                    else
                    {
                        HttpContext.Response.Write("empty");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                HttpContext.Response.Write("empty");
            }


            return new EmptyResult();
        }

        public ActionResult PaymentController()
        {
            Cart gh = Session["giohang"] as Cart;
            Nguoidung member = Session["memberLogin"] as Nguoidung;
            if (gh != null)
            {
                if (member != null)
                {
                    gh.Member = member;
                    gh.CartId = member.Id;
                }

                try
                {
                    if (cartDAO.InsertCart(gh))
                    {
                        gh.Item.Clear();
                        Session.Add("giohang", gh);
                        return View("Order_Success");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return View("Error");
                }
            }

            return View("Order_Success");
        }

        public ActionResult Profile_User()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Update_User_Profile()
        {
            Web2023Project.Models.Nguoidung member = Session["memberLogin"] as Web2023Project.Models.Nguoidung;
            string name = Request["name"];
            string email = Request["email"];
            string gender = Request["gender"];
            string phone = Request["phone"];
            Nguoidung n = new Nguoidung();
            n.Id = member.Id;
            n.Ten = name;
            n.Email = email;
            n.Sdt = phone;
            n.Gioitinh = ulong.Parse(gender);
            n.Matkhau = member.Matkhau;
            n.Anhdaidien = member.Anhdaidien;
            n.Quyen = member.Quyen;
            n.GoogleId = member.GoogleId;
            n.FacebookId = member.FacebookId;
            n.Ngaytao = member.Ngaytao;
            n.Ngaycapnhat = member.Ngaycapnhat;
            n.Trangthai = member.Trangthai;
            n.Diachis = member.Diachis;
            Task<bool> check = Update_User_DAO.updateInfoUser(n);
            bool checkbool = await check;
            if (checkbool)
            {
                Session.Remove("memberLogin");
                Session.Add("memberLogin", n);
                return View("Profile_User");
            }
            else
            {
                return View("Login");
            }
        }
        [HttpPost]
        public async Task<ActionResult> Address_User_Profile()
        {
            Web2023Project.Models.Nguoidung member = Session["memberLogin"] as Web2023Project.Models.Nguoidung;
            string name = Request["name"];
            string xa = Request["xa"];
            string huyen = Request["huyen"];
            string tinh = Request["tinh"];
            string phone = Request["phone"];
            Diachi n = new Diachi();
            n.IdNd = member.Id;
            n.Ten = name;
            n.Sdt = phone;
            n.Tinh = tinh;
            n.Xa = xa;
            n.Huyen = huyen;
          
            Task<bool> check = Update_User_DAO.AddAddressInfoUser(n,"add");
            bool checkbool = await check;
            if (checkbool)
            {
                int id = member.Id;
                Task<List<Diachi>> list = LoginDao.getDiaChi(id.ToString());
                member.Diachis = await list;
                Session.Add("memberLogin", member);
                return View("Profile_User");
            }
            else
            {
                return View("Login");
            }
        }
        [HttpPost]
        public async Task<ActionResult> UpdateAddress_User_Profile()
        {
            Web2023Project.Models.Nguoidung member = Session["memberLogin"] as Web2023Project.Models.Nguoidung;
            string name = Request["name"];
            string xa = Request["xa"];
            string huyen = Request["huyen"];
            string tinh = Request["tinh"];
            string phone = Request["phone"];
            string ghichu = Request["ghichu"];
            string idDC = Request["idDC"];
            string trangthai = Request["trangthai"];
            Diachi n = new Diachi();
            n.Id = Int32.Parse(idDC);
            n.Trangthai = sbyte.Parse(trangthai);
            n.Ghichu = ghichu;
            n.IdNd = member.Id;
            n.Ten = name;
            n.Sdt = phone;
            n.Tinh = tinh;
            n.Xa = xa;
            n.Huyen = huyen;

            Task<bool> check = Update_User_DAO.AddAddressInfoUser(n, "update");
            bool checkB = await check;
            if (checkB)
            {
                int id = member.Id;
                Task<List<Diachi>> list = LoginDao.getDiaChi(id.ToString());
                List<Diachi> listDc = await list;
                member.Diachis = listDc;
                Session.Add("memberLogin", member);
                return PartialView("UpdateAddress", listDc);
            }
            else
            {
                return View("Login");
            }
                
           
           
        }
        [HttpPost]
        public async Task<ActionResult> DeleteAddress_User_Profile()
        {
            Web2023Project.Models.Nguoidung member = Session["memberLogin"] as Web2023Project.Models.Nguoidung;
            string idDC = Request["idDC"];                      
            Task<bool> check = Update_User_DAO.DeleteAddress(idDC);
            bool checkB = await check;
            if (checkB)
            {
                int id = member.Id;
                Task<List<Diachi>> list = LoginDao.getDiaChi(id.ToString());
                List<Diachi> listDc = await list;
                member.Diachis = listDc;
                Session.Add("memberLogin", member);
                return PartialView("UpdateAddress", listDc);
            }
            else
            {
                return View("Login");
            }



        }
        [HttpPost]
        public async Task<ActionResult> ChangePass_User_Profile()
        {
            Web2023Project.Models.Nguoidung member = Session["memberLogin"] as Web2023Project.Models.Nguoidung;
            string currentPass = Request["current-pass"];
            string newPass = Request["new-pass"];
            Console.WriteLine(currentPass);
            Console.WriteLine(member.Matkhau);
            Task<bool> checkPass = Update_User_DAO.checkCurrentPass(member.Matkhau,currentPass);
            bool checkboolPass = await checkPass;
            if (checkboolPass)
            {
                Nguoidung n = new Nguoidung();
                n.Id = member.Id;
                n.Ten = member.Ten;
                n.Email = member.Email;
                n.Sdt = member.Sdt;
                n.Gioitinh = member.Gioitinh;
                n.Matkhau = newPass;
                n.Anhdaidien = member.Anhdaidien;
                n.Quyen = member.Quyen;
                n.GoogleId = member.GoogleId;
                n.FacebookId = member.FacebookId;
                n.Ngaytao = member.Ngaytao;
                n.Ngaycapnhat = member.Ngaycapnhat;
                n.Trangthai = member.Trangthai;
                Task<bool> check = Update_User_DAO.updateInfoUser(n);
                bool checkbool = await check;
                if (checkbool)
                {
                    Session.Remove("memberLogin");
                    Session.Add("memberLogin", n);
                    return View("Profile_User");
                }
            }          
            else
            {
                return View("Login");
            }
            return View("Profile_User");
        }

        public ActionResult Question()
        {
            return View();
        }

        public ActionResult Reset_Password()
        {
            return View();
        }
    }
}