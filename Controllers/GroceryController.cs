using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuthApp.Models;
using System.Linq;

namespace Controllers.Controllers
{
  [Route("api/test")]
  [Authorize]
  public class GroceryListController : Controller
  {
    private readonly GroceryListContext _context;

    public GroceryListController(GroceryListContext context)
    {
      _context = context;
    }     

    [HttpGet]
    public object GetAll()
    {
      return new 
      {
        nameof = "Nahid Hasan",
        loggedBy = User.Identity.Name
      };
    }

    [HttpGet("{id}", Name = "GetGroceryItem")]
    public IActionResult GetById(long id)
    {
      var item = _context.GroceryList.FirstOrDefault(t => t.Id == id);
      if (item == null)
      {
        return NotFound();
      }
      return new ObjectResult(item);
    }

    [HttpPost]
    public IActionResult Create([FromBody] GroceryItem item)
    {
      if (item == null)
      {
        return BadRequest();
      }

      _context.GroceryList.Add(item);
      _context.SaveChanges();

      return CreatedAtRoute("GetGroceryItem", new { id = item.Id }, item);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
      var item = _context.GroceryList.First(t => t.Id == id);
      if (item == null)
      {
        return NotFound();
      }

      _context.GroceryList.Remove(item);
      _context.SaveChanges();
      return new NoContentResult();
    }
  }
}