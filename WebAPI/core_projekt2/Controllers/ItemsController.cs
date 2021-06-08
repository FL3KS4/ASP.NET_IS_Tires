using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_projekt2.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace core_projekt2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private DatabaseManipulation db;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ItemsController> _logger;

        public ItemsController(ILogger<ItemsController> logger)
        {
            _logger = logger;
            this.db = new DatabaseManipulation();
        }




        [HttpGet]
        public async Task<ActionResult<Item_DTO>> GetAll() //vratí všechny itemy z databáze
        {           
            var res = await db.DbSelect<Item_DTO>("Select * from Item");           
            return Ok(res);
        }


        [HttpPost]
        public async Task<ActionResult> Insert(Item_DTO newItem) //insert
        {
            if(newItem==null)
            {
                return BadRequest();
               
            }
            db.CommandEdit("@item_name", newItem.item_name);
            db.CommandEdit("@item_count", newItem.item_count);
            db.CommandEdit("@item_price", newItem.item_price);
            db.CommandEdit("@item_category", newItem.item_category);
            var res = await db.UpdateInsertDelete("insert into Item values (@item_name,@item_count,@item_price,@item_category)");

            return Ok();
        }
    }
}
