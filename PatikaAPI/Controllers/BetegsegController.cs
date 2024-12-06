using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PatikaAPI.DTOs;
using PatikaAPI.Models;

namespace PatikaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BetegsegController : ControllerBase
    {
        [HttpGet]
       
        [HttpGet("Betegség")]
        public IActionResult GetBetegseg() 
        {
            using (var context = new PatikaContext()) 
            {
                try
                {
                    List<Betegseg> beteg = context.Betegsegs.ToList();
                    return Ok(beteg);
                }
                catch (Exception ex)
                {

                    List<Betegseg> hiba = new List<Betegseg>();
                    Betegseg hibak = new Betegseg()
                    {
                        Id = -1,
                        Megnevezes = ex.Message
                    }; 
                    hiba.Add(hibak); 
                    return BadRequest(hiba);

                }
               
            }
            
        }
        [HttpGet("GetBetegségById")]
        public IActionResult GetBetegseg(int id) 
        {
            using (var context = new PatikaContext()) 
            {
                try
                {
                    Betegseg levi = context.Betegsegs.FirstOrDefault(x => x.Id == id);
                    if (levi == null)
                    {
                        return NotFound("Nincs ilyen ID");
                    }
                    else
                    {
                        return Ok(levi);
                    }
                    
                }
                catch (Exception ex)
                {
                    Betegseg gatya = new Betegseg()
                    {
                        Id = -1,
                        Megnevezes = ex.Message
                    };
                    return BadRequest(gatya);
                    
                }
            }
        }
       
        [HttpGet("ToHatoanyagName")]
        public IActionResult Get(string l) 
        {
            using (var context = new PatikaContext()) 
            {
                try
                {
                    List<Betegseg> result = context.Kezels.Include(k => k.Gyogyszer)
                                            .Include(k => k.Betegseg)
                                            .Where(k => k.Gyogyszer.Hatoanyag == l)
                                            .Select(k => k.Betegseg)
                                            .ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {

                    throw;
                }
               
            }
        }
        [HttpGet("WId")]
        public IActionResult GetwId(int id)
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    List<Betegseg> result = context.Kezels.Include(k => k.Gyogyszer)
                                            .Include(k => k.Betegseg)
                                            .Where(k => k.Gyogyszer.Id == id)
                                            .Select(k => k.Betegseg)
                                            .ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {

                    List<Betegseg> gatyo = new List<Betegseg>();
                    Betegseg gatya = new Betegseg()
                    {
                        Id = -1,
                        Megnevezes = ex.Message
                    };
                    gatyo.Add(gatya);
                    return BadRequest(gatyo);

                }

            }
        }
        [HttpGet("emmegaz")]
        public IActionResult GetBetegsegNev() 
        {
            using (var context = new PatikaContext()) 
            {
                try
                {
                    List<BetegsegNevDTO> ninjago = context.Betegsegs.Select(b => new BetegsegNevDTO()
                    {
                        Id = b.Id,
                        Megnevezes = b.Megnevezes
                    }).ToList();
                    return Ok(ninjago);
                }
                catch (Exception ex)
                {
                    BetegsegNevDTO hiba = new BetegsegNevDTO()
                    {
                        Id = -1,
                        Megnevezes = ex.Message
                    };
                    return BadRequest(hiba);
                    
                }
            }
        }
        [HttpPost("UjGyogyszer")]
        public IActionResult Post(Betegseg ujBetegseg)
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    context.Betegsegs.Add(ujBetegseg);
                    context.SaveChanges();
                    return Ok("Sikeres rögzítés");

                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpDelete("huszársereg")]
        public IActionResult Delete(int id)
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    Betegseg torlendo = new Betegseg()
                    {
                        Id = id
                    };
                    context.Betegsegs.Remove(torlendo);
                    context.SaveChanges();
                    return Ok("sikeres");
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
        }
        [HttpPut("Modosit")]
        public IActionResult Put(Betegseg ujBetegseg)
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    if (context.Betegsegs.Contains(ujBetegseg))
                    {
                        context.Betegsegs.Update(ujBetegseg);
                        context.SaveChanges();
                        return Ok("szép");
                    }
                    else
                    {
                        return NotFound("Nem létezik ilyen azonosító");
                    }

                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
