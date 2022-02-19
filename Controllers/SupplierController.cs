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
    public class SupplierController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SupplierController(MyDbContext context)
        {
            _context = context;
        }

        // GetAll() is automatically recognized as
        // http://localhost:<port #>/api/todo
        [HttpGet]
        public IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers.ToList();
        }

        // GetById() is automatically recognized as
        // http://localhost:<port #>/api/todo/{id}

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var item = _context.Suppliers.FirstOrDefault(s => s.SupplierID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }


        [HttpPost]
        public IActionResult Create([FromBody] Supplier supplier)
        {
            if (supplier.SupplierName == null || supplier.SupplierName == "")
            {
                return BadRequest();
            }
            try
            {
                _context.Suppliers.Add(supplier);
                _context.SaveChanges();
            }
            catch (Exception ee)
            {
                return Conflict(new { message = $"An existing record with the id '{supplier.SupplierID}' was already found." });
            }
            return new ObjectResult(supplier);
        }


        [HttpPut]
        [Route("myedit")] // Custom route
        public IActionResult GetByParams([FromBody] Supplier supplier)
        {
            var item = _context.Suppliers.Where(s => s.SupplierID == supplier.SupplierID).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                item.SupplierName = supplier.SupplierName;
                _context.SaveChanges();
            }
            return new ObjectResult(item);
        }

        [HttpDelete("{id}")]
        public IActionResult MyDelete(int id)
        {
            var itemFromChild = _context.ProduceSuppliers.Where(s => s.SupplierID == id);
            if (itemFromChild == null)
            {
                return NotFound();
            }
            try
            {
                Console.WriteLine(itemFromChild);
                foreach (var eachRow in itemFromChild)
                {
                    _context.ProduceSuppliers.Remove(eachRow);
                }

                _context.SaveChanges();
            }
            catch (Exception ee)
            {
                return Conflict(new { message = "Invalid request, please try a different entry." });
            }
            var item = _context.Suppliers.Where(s => s.SupplierID == id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            try
            {
                _context.Suppliers.Remove(item);
                _context.SaveChanges();
            }
            catch (Exception ee)
            {
                return Conflict(new { message = "Invalid request, please try a different entry." });
            }
            return new ObjectResult(item);
        }


        //[HttpDelete("{id}")]
        //public IActionResult MyDelete(int id)
        //{
        //    var item = _context.Suppliers.Where(s => s.SupplierID == id).FirstOrDefault();
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    try
        //    {
        //        _context.Suppliers.Remove(item);
        //        _context.SaveChanges();
        //    }
        //    catch (Exception ee)
        //    {
        //        return Conflict(new { message = $"An existing record with the id '{id}' was already found." });
        //    }
        //    return new ObjectResult(item);
        //}

    }
}

