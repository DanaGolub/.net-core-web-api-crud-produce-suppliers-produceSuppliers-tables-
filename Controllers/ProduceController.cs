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
    public class ProduceController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProduceController(MyDbContext context)
        {
            _context = context;
        }

        // GetAll() is automatically recognized as
        // http://localhost:<port #>/api/todo
        [HttpGet]
        public IEnumerable<Produce> GetAll()
        {
            return _context.Produces.ToList();
        }

        // GetById() is automatically recognized as
        // http://localhost:<port #>/api/todo/{id}

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var item = _context.Produces.FirstOrDefault(p => p.ProduceID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }


        [HttpPost]
        public IActionResult Create([FromBody] Produce produce)
        {
            if (produce.Description == null || produce.Description == "")
            {
                return BadRequest();
            }
            _context.Produces.Add(produce);
            _context.SaveChanges();
            return new ObjectResult(produce);
        }


        [HttpPut]
        [Route("myedit")] // Custom route
        public IActionResult GetByParams([FromBody] Produce produce)
        {
            var item = _context.Produces.Where(p => p.ProduceID == produce.ProduceID).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                item.Description = produce.Description;
                _context.SaveChanges();
            }
            return new ObjectResult(item);
        }


        [HttpDelete("{id}")]
        public IActionResult MyDelete(int id)
        {
            var itemFromChild = _context.ProduceSuppliers.Where(s => s.ProduceID == id);
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
                return Conflict(new { message = $"An existing record with the id '{id}' was already found." });
            }
            var item = _context.Produces.Where(p => p.ProduceID == id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            try
            {
                _context.Produces.Remove(item);
                _context.SaveChanges();
            }
            catch (Exception ee)
            {
                return Conflict(new { message = $"An existing record with the id '{id}' was already found."});
            }
            return new ObjectResult(item);
        }

    }
}
