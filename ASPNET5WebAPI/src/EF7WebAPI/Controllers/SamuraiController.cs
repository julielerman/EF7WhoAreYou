using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using EF7Samurai.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNET5WebAPI.Controllers
{

  [Route("api/[controller]")]
  public class SamuraisController : Controller
  {
    private NonTrackingDataWrapper _data;

    public SamuraisController(NonTrackingDataWrapper data) {
      _data=data;
    }
   
    [HttpGet]
    public IActionResult Get() {
      var samurais= _data.GetSamuraisWithQuotes();
       return new ObjectResult(samurais);
     
     
    }

    [HttpGet("{id:int}", Name = "GetByIdRoute")]
    public IActionResult GetById(int id) {
      var samurai = _data.GetSamurai(id);
      if (samurai == null) {
        return HttpNotFound();
      }

      return new ObjectResult(samurai);
    }
  }
}
