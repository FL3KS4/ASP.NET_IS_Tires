using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApp_project.Models;
using WebApp_project.Services;
using Microsoft.Extensions.DependencyInjection;


namespace WebApp_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private EshopService _eshop;
        


        public HomeController(EshopService eshop, IHttpClientFactory clientFactory)
        {
            _eshop = eshop;
            _clientFactory = clientFactory;
        }

        

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            List<int> items;
            if (this.HttpContext.Session.TryGetValue("Cart", out byte[] data))
            {
                string txt = Encoding.UTF8.GetString(data);
                items = txt.Split(',').Select(x => int.Parse(x)).ToList();
            }
            else
            {
                items = new List<int>();
            }

            ViewBag.Cart = items;

            base.OnActionExecuting(context);
        }

        public async Task<IActionResult> Index()
        {
            InfoTable info = new InfoTable();
            info.vozidlaCount = await InfoService.getAllVozidla();
            info.zakaznikCount = await InfoService.getAllZakaznik();
            info.pneuCount = await InfoService.getAllPneu();
            info.pracovniciCount = await InfoService.getAllPracovnik();

            ViewBag.Info = info;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CustomerGuest()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem("-", ""),
                new SelectListItem("Osobní auto", "1"),
                new SelectListItem("Motocykl", "2"),
                new SelectListItem("Autobus", "3"),
                new SelectListItem("Traktor", "4"),
                new SelectListItem("Nákladní auto", "5")

            };
            ViewBag.Cars = items;

            List<SelectListItem> items2 = new List<SelectListItem>()
            {
                new SelectListItem("-", ""),
                new SelectListItem("Defekt", "Defekt"),
                new SelectListItem("Vyvážení", "Vyvážení")
            };

            ViewBag.Problems = items2;

            return View();
        }

        public IActionResult Order()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem("-", ""),
                new SelectListItem("Osobní odběr", "Osobní odběr"),
                new SelectListItem("Doručení Česká pošta", "Doručení Česká pošta"),
                new SelectListItem("Doručení PPL", "Doručení PPL")            
            };
            ViewBag.TTypes = items;

            List<SelectListItem> items2 = new List<SelectListItem>()
            {
                new SelectListItem("-", ""),
                new SelectListItem("Dobírka", "Dobírka"),
                new SelectListItem("Platba předem", "Platba předem"),
                new SelectListItem("Platba převodem", "Platba převodem")
            };

            ViewBag.PTypes = items2;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Order(OrderCustomer customer)
        {
            if(ModelState.IsValid)
            {
                List<Eshop> tmpCart = new List<Eshop>();
                foreach (int n in ViewBag.Cart)
                {

                    Eshop eshop = await _eshop.findByID(n);


                    if (tmpCart.Any(prod => prod.ID == eshop.ID) == false)
                    {
                        eshop.pocet = 1;
                        tmpCart.Add(eshop);
                    }
                    else
                    {
                        tmpCart[tmpCart.FindIndex(prod => prod.ID == eshop.ID)].pocet += 1;
                    }
                }

                if(tmpCart != null)
                {
                    foreach (Eshop e in tmpCart)
                    {
                        Eshop tmpEshop = null;

                        UriBuilder builder = new UriBuilder("https://localhost:44309/API/Eshop" + "/ItemID");
                        builder.Query = $"id={e.ID}";

                        var request = new HttpRequestMessage(HttpMethod.Get, builder.ToString());
                        var client = _clientFactory.CreateClient();
                        var response = await client.SendAsync(request);



                        if (response.IsSuccessStatusCode)
                        {
                            string apiItems = await response.Content.ReadAsStringAsync();
                            tmpEshop = JsonConvert.DeserializeObject<Eshop>(apiItems);
                        }
                        tmpEshop.pocet = tmpEshop.pocet - e.pocet;

                        _eshop.updateItem(tmpEshop);
                        this.HttpContext.Session.Clear();
                    }

                    return RedirectToAction("OrderSuccess", "Home");
                }


                return RedirectToAction("Cart", "Home");
            }
            else
            {
                return this.Order();
            }
        }

        

        public IActionResult OrderSuccess()
        {
            return View();
        }


        public async Task<IActionResult> Eshop()
        {
            
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44309/API/Eshop");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            List<Eshop> items;

            if(response.IsSuccessStatusCode)
            {
                string apiItems = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<Eshop>>(apiItems);

                ViewBag.Tires = items;
            }       
            return View();
        }

        public async Task<IActionResult> ItemDetail(int id)
        {
            Eshop item = null;

            UriBuilder builder = new UriBuilder("https://localhost:44309/API/Eshop" + "/ItemID");
            builder.Query = $"id={id}";

            var request = new HttpRequestMessage(HttpMethod.Get, builder.ToString());
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);



            if (response.IsSuccessStatusCode)
            {
                string apiItems = await response.Content.ReadAsStringAsync();
                item = JsonConvert.DeserializeObject<Eshop>(apiItems);
            }


            return View(item);
        }


        public async Task<IActionResult> Cart()
        {
            List <Eshop> tmpCart = new List<Eshop>();
            foreach(int n in ViewBag.Cart)
            {

                Eshop eshop = await _eshop.findByID(n);

                
                if (tmpCart.Any(prod => prod.ID == eshop.ID) == false)
                {
                    eshop.pocet = 1;
                    tmpCart.Add(eshop);
                }
                else 
                {
                    tmpCart[tmpCart.FindIndex(prod => prod.ID == eshop.ID)].pocet += 1;
                }                
            }

            ViewBag.CartItems = tmpCart;
            return View();
        }

        public IActionResult AddToCart(int id)
        {
            List<int> items;

 
            if (this.HttpContext.Session.TryGetValue("Cart", out byte[] data))
            {
                string txt = Encoding.UTF8.GetString(data);
                items = txt.Split(',').Select(x => int.Parse(x)).ToList();              
            }
            else
            {
                items = new List<int>();
            }
            items.Add(id);


            JsonResult tmpJson = new JsonResult(items);

            data = Encoding.UTF8.GetBytes(string.Join(",", items));
            this.HttpContext.Session.Set("Cart", data);

            

            return RedirectToAction("Eshop", "Home");
        }

        [HttpPost]
        public IActionResult EmptyCart()
        {

            this.HttpContext.Session.Clear();

            return RedirectToAction("Eshop", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login user)
        {
            if(ModelState.IsValid)
            {
                Servis_Pracovnik pracovnik = null;
                Zakaznik zakaznik = null;

                UriBuilder builder = new UriBuilder("https://localhost:44309/API/Login" + "/Login");
                builder.Query = $"login={user.login}&password={user.heslo}";

                var request = new HttpRequestMessage(HttpMethod.Get, builder.ToString());
                var client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);

                

                if (response.IsSuccessStatusCode)
                {
                    string apiItems = await response.Content.ReadAsStringAsync();
                    zakaznik = JsonConvert.DeserializeObject<Zakaznik>(apiItems);
                    if(zakaznik.typZakaznikID == null)
                    {
                        zakaznik = null;
                        pracovnik = JsonConvert.DeserializeObject<Servis_Pracovnik>(apiItems);
                        
                    }                                    
                }


                

                if (zakaznik == null && pracovnik == null)
                {
                    TempData["msg"] = "<div class=\"alert alert-danger\" role=\"alert\"> Login nebo heslo je nesprávné! </div>";
                    return RedirectToAction("Login", "Home");
                }

                if (pracovnik != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.login)
                };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var props = new AuthenticationProperties();
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                    return RedirectToAction("Profile", "Workers");
                }
                else if(zakaznik != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, zakaznik.login)
                };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var props = new AuthenticationProperties();
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                    return RedirectToAction("Profile", "Customers");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CustomerGuest([Bind]Guest guest)
        {
            if(ModelState.IsValid)
            {
                if (GuestService.CheckCalender(guest) == false)
                {
                    TempData["msg"] = "<div class=\"alert alert-warning\" role=\"alert\"> Rezervace v tento čas není možná! </div>";
                    return RedirectToAction("CustomerGuest","Home");
                }

                int guestID = GuestService.CreateNewGuest(guest);
                int vehicleID = GuestService.CreatenewVehicle(guest);

                guest.guestID = guestID;
                guest.vehicleID = vehicleID;

                int calenderID = GuestService.AddCalender(guest);

                if (calenderID != -1)
                {
                    return RedirectToAction("Successful", "Home");
                }
                else
                {
                    return this.CustomerGuest();
                }
            }
            else
            {
                return this.CustomerGuest();
            }
           
        }

        public IActionResult Successful()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
