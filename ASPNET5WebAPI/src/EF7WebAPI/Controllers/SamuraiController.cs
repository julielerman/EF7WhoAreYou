﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ASPNET5WebAPI.Model;
using Microsoft.Data.Entity;
using Microsoft.Framework.Logging;
using EF7Samurai.Domain;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNET5WebAPI.Controllers
{

  [Route("api/[controller]")]
  public class SamuraiController : Controller
  {
    private SamuraiContext _context;
    public SamuraiController(SamuraiContext context) {
      _context = context;
    }
   
    [HttpGet]
    public IActionResult Get() {
      var samurais= _context.Samurais.Include(s => s.Quotes).ToList();
       return new ObjectResult(samurais);
     
     
    }

    [HttpGet("{id:int}", Name = "GetByIdRoute")]
    public IActionResult GetById(int id) {
      var samurai = _context.Samurais.FirstOrDefault(s => s.Id == id);
      if (samurai == null) {
        return HttpNotFound();
      }

      return new ObjectResult(samurai);
    }
  }
}
