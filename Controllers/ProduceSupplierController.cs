using AssignmentBackEnd1.DatabaseHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AssignmentBackEnd1.DatabaseHelper.MyDbContext;

namespace AssignmentBackEnd1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduceSupplierController : ControllerBase
    {
        private readonly MyDbContext _context;
        public ProduceSupplierController(MyDbContext context)
        {
            _context = context;
        }
        // GetAll() is automatically recognized as
        // http://localhost:<port #>/api/todo

        [HttpGet]
        public IEnumerable<ProduceSupplier> GetAll()
        {
            return _context.ProduceSuppliers.ToList();
        }

        // GetById() is automatically recognized as
        // http://localhost:<port #>/api/todo/{id}

        [HttpGet("{produceId}/{supplierId}")]
        public IActionResult GetById(long produceId, long supplierId)
        {
            var item = _context.ProduceSuppliers.FirstOrDefault(n => n.ProduceID == produceId && n.SupplierID == supplierId);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }


        [HttpPost]
        public IActionResult Create([FromBody] ProduceSupplier produceSupplier)
        {
            if (produceSupplier.Qty < 0)
            {
                return BadRequest();
            }
            try {
            _context.ProduceSuppliers.Add(produceSupplier);
            _context.SaveChanges();
            }
            catch (Exception ee)
            {
                return Conflict(new { message = $"Incorrect entry, please try again." });
            }
            return new ObjectResult(produceSupplier);
        }


        [HttpPut]
        [Route("myedit")] // Custom route
        public IActionResult GetByParams([FromBody] ProduceSupplier produceSupplier)
        {
            var item = _context.ProduceSuppliers.Where(p => p.ProduceID == produceSupplier.ProduceID && p.SupplierID == produceSupplier.SupplierID).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                item.Qty = produceSupplier.Qty;
                _context.SaveChanges();
            }
            return new ObjectResult(item);
        }

        [HttpDelete("{produceId}/{supplierId}")]
        public IActionResult MyDelete(long produceId, long supplierId)
        {
            var item = _context.ProduceSuppliers.FirstOrDefault(n => n.ProduceID == produceId && n.SupplierID == supplierId);
            if (item == null)
            {
                return NotFound();
            }
            _context.ProduceSuppliers.Remove(item);
            _context.SaveChanges();
            return new ObjectResult(item);
        }


        //[HttpDelete("{produceId}/{supplierId}")]
        //public IActionResult MyDelete(long produceId, long supplierId)
        //{
        //    var item = _context.ProduceSuppliers.FirstOrDefault(n => n.ProduceID == produceId && n.SupplierID == supplierId);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.ProduceSuppliers.Remove(item);
        //    _context.SaveChanges();
        //    return new ObjectResult(item);
        //}

    }
    //I need to place individual delete action methods in try catch (catch conflict) the save to db part. 

}

