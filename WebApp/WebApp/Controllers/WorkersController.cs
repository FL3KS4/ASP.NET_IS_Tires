using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp_project.Models;
using WebApp_project.Services;

namespace WebApp_project.Controllers
{
    [Authorize]
    public class WorkersController : Controller
    {
        private PracovnikService pracovnik;
        private EshopService _eshop;
        Servis_Pracovnik prac = new Servis_Pracovnik();

        public WorkersController(PracovnikService pracovnik, EshopService eshop)
        {
            this.pracovnik = pracovnik;
            this._eshop = eshop;
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
            prac = await pracovnik.FindByLogin(userLogin);

            ViewBag.Info = prac;
            
            return View();
        }


        public async Task<IActionResult> Edit()
        {
            var userLogin = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            prac = await pracovnik.FindByLogin(userLogin);

            ViewBag.Info = prac;

            return View(prac);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Servis_Pracovnik worker)
        {
            if (ModelState.IsValid)
            {
                prac = await pracovnik.FindByLogin(worker.login);
                prac.jmeno = worker.jmeno;
                prac.prijmeni = worker.prijmeni;
                prac.telefon = worker.telefon;
                prac.datum_narozeni = worker.datum_narozeni;
                prac.heslo = worker.heslo;

                pracovnik.updatePracovnik(prac);
                return RedirectToAction("Profile", "Workers");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> EditEshop()
        {
            ViewBag.Tires = await _eshop.getAll();
            return View();
        }

        public IActionResult AddNew()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem("-", ""),
                new SelectListItem("Letní", "1"),
                new SelectListItem("Zimní", "2"),
                new SelectListItem("Celoroční", "3"),
                new SelectListItem("Autobus", "4"),
                new SelectListItem("Traktor", "5"),
                new SelectListItem("Nákladní auto", "6"),
            };
            ViewBag.Types = items;

            return View();
        }

        [HttpPost]
        public IActionResult AddNew(Eshop eshop)
        {
            if(ModelState.IsValid)
            {
                _eshop.addNewItem(eshop);
                return RedirectToAction("EditEshop", "Workers");
            }
            else
            {
                return  this.AddNew();
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _eshop.findByID(id);

            if (item == null)
            {
                return RedirectToAction("EditEshop", "Workers");
            }
            else
            {
                 _eshop.deleteByID(id);

                return RedirectToAction("EditEshop", "Workers");
            }
        }

        public async Task<IActionResult> EditItem(int id)
        {
            Eshop item = await _eshop.findByID(id);

            List<SelectListItem> types = new List<SelectListItem>()
            {
                new SelectListItem("-", ""),
                new SelectListItem("Letní", "1"),
                new SelectListItem("Zimní", "2"),
                new SelectListItem("Celoroční", "3"),
                new SelectListItem("Autobus", "4"),
                new SelectListItem("Traktor", "5"),
                new SelectListItem("Nákladní auto", "6"),
            };
            ViewBag.Types = types;

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> EditItem(Eshop eshop ,int id)
        {
            if(ModelState.IsValid)
            {
                eshop.ID = id;
                _eshop.updateItem(eshop);
                return RedirectToAction("EditEshop", "Workers");
            }
            return await this.EditItem(id);
        }


    }
}
