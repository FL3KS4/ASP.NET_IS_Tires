using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_project.Services;
using WebApp_project.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace WebApp_project.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private ZakaznikService zakaznik;
        Zakaznik za = new Zakaznik();

        public CustomersController(ZakaznikService service)
        {
            this.zakaznik = service;                     
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


        public async Task<IActionResult> Profile()
        {
            

            var userLogin = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            za = await zakaznik.FindByLogin(userLogin);

            ViewBag.Info = za;
            ViewBag.Vozidla = await zakaznik.getbyZakaznikID(za.ID);

            return View();
        }

        public async Task<IActionResult> Edit()
        {
            var userLogin = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            za = await zakaznik.FindByLogin(userLogin);
             

            ViewBag.Info = za;

            return View(za);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Zakaznik zak)
        {                   
            if (ModelState.IsValid)
            {
                za = await zakaznik.FindByLogin(zak.login);
                za.jmeno = zak.jmeno;
                za.prijmeni = zak.prijmeni;
                za.adresa = zak.adresa;
                za.telefon = zak.telefon;
                za.heslo = zak.heslo;

                zakaznik.updateZakaznik(za);
                return RedirectToAction("Profile", "Customers");
            }
            else
            {
                return View();
            }                       
        }

        public IActionResult AddVehicle()
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
            ViewBag.Types = items;

            

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddVehicle(Vozidlo vuz)
        {
            Zakaznik_vozidlo_relation zv = new Zakaznik_vozidlo_relation();

            var userLogin = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            za = await zakaznik.FindByLogin(userLogin);

            if (ModelState.IsValid)
            {
                zakaznik.addNewVozidlo(vuz);

                if(vuz.ID != null)
                {
                    zv.vozidloID = vuz.ID.Value;
                    zv.zakaznikID = za.ID;
                    zakaznik.AddVozidloToZakaznik(zv);
                }
                return RedirectToAction("Profile", "Customers");
            }
            else
            {
                return this.AddVehicle();
            }
        }

        public async Task<IActionResult> Reserve()
        {
            var userLogin = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            za = await zakaznik.FindByLogin(userLogin);

            Customer myReserveG = new Customer();
            myReserveG.name = za.jmeno;
            myReserveG.lastName = za.prijmeni;
            myReserveG.telephoneNum = za.telefon;

            ViewBag.Info = myReserveG;

            List<SelectListItem> Cars = new List<SelectListItem>();

            List<VozidloPneu> vozidla = await zakaznik.getbyZakaznikID(za.ID);

            if(vozidla != null)
            {
                foreach (VozidloPneu v in vozidla)
                {
                    SelectListItem newItem = new SelectListItem(v.znacka, v.znacka);
                    Cars.Add(newItem);
                }
            }           

            ViewBag.Cars = Cars;



            return View(myReserveG);
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (zakaznik.CheckCalender(customer) == false)
                {
                    TempData["msg"] = "<div class=\"alert alert-warning\" role=\"alert\"> Rezervace v tento čas není možná! </div>";
                    return await this.Reserve();
                }

                int guestID = zakaznik.CreateNewGuest(customer);
                int vehicleID = zakaznik.CreatenewVehicle(customer);

                customer.guestID = guestID;
                customer.vehicleID = vehicleID;

                int calenderID = zakaznik.AddCalender(customer);

                if (calenderID != -1)
                {
                    return RedirectToAction("Successful", "Customers");
                }
                else
                {
                    return await this.Reserve();
                }
            }
            else
            {
                return await this.Reserve();
            }
        }

        public IActionResult Successful()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVeh(string IDs)
        {
            string[] IDss = IDs.Split(' ');
            int vehID = int.Parse(IDss[0]);
            int zakID = int.Parse(IDss[1]);

         

            List<Zakaznik_vozidlo_relation> vozidlaSum = await zakaznik.getInZakVeh(vehID);

            if(vozidlaSum.Count == 1)
            {
                zakaznik.deleteByID(zakID, vehID);

                List<Vozidlo> vozidla = await zakaznik.getByID(vehID);
                zakaznik.deleteVehByID(vozidla[0]);



                return Redirect(Request.GetTypedHeaders().Referer.ToString());

            }
            else
            {
                zakaznik.deleteByID(zakID, vehID);
                return Redirect(Request.GetTypedHeaders().Referer.ToString());
            }

           
        }

        
    }
}
