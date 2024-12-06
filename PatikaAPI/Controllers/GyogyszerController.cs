using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PatikaAPI.Models;
using PatikaAPI.DTOs;

namespace PatikaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GyogyszerController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    List<Gyogyszer> result = context.Gyogyszers.ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {

                    List<Gyogyszer> result = new List<Gyogyszer>();
                    Gyogyszer hiba = new Gyogyszer()
                    {
                        Id = -1,
                        Nev = ex.Message

                    }; 
                    result.Add(hiba); 
                    return BadRequest(result);

                }
            }
        }
        [HttpGet ("ById")]
        public IActionResult Get(int id) 
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    Gyogyszer result = context.Gyogyszers.FirstOrDefault(x => x.Id == id);
                    return Ok(result);
                }
                catch (Exception ex)
                {

                    
                    Gyogyszer hiba = new Gyogyszer()
                    {
                        Id = -1,
                        Nev = ex.Message

                    };
                    
                    return BadRequest(hiba);

                }
            }
        }
        
        [HttpGet("GetGyogyszerById")]
        public IActionResult GetGyogyszer(int id) 
        {
            using (var context = new PatikaContext()) 
            {
                try
                {
                    Gyogyszer levi = context.Gyogyszers.FirstOrDefault(x => x.Id == id);
                    return Ok(levi);
                }
                catch (Exception ex)
                {
                    Gyogyszer gatya = new Gyogyszer()
                    {
                        Id = -1,
                        Nev = ex.Message
                    };
                    return BadRequest(gatya);
                    
                }
            }
        }
        [HttpGet("ToBetegsegName")]
        public IActionResult Get(string bName) 
        {
            using (var context = new PatikaContext()) 
            {
                try
                {
                    List<Gyogyszer> result = context.Kezels.Include(k => k.Gyogyszer)
                        .Include(k => k.Betegseg)
                        .Where(k => k.Betegseg.Megnevezes == bName)
                        .Select(k => k.Gyogyszer)
                        .ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {

                    List<Gyogyszer> result = new List<Gyogyszer>();
                    Gyogyszer hiba = new Gyogyszer()
                    {
                        Id = -1,
                        Nev = ex.Message
                    };
                    result.Add(hiba);

                    return BadRequest(result);
                }
            }
           }
        [HttpGet("ToBetegsegId")]
        public IActionResult GetwId(int id)
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    List<Gyogyszer> result = context.Kezels.Include(k => k.Gyogyszer)
                        .Include(k => k.Betegseg)
                        .Where(k => k.Betegseg.Id == id)
                        .Select(k => k.Gyogyszer)
                        .ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {

                    List<Gyogyszer> result = new List<Gyogyszer>();
                    Gyogyszer hiba = new Gyogyszer()
                    {
                        Id = -1,
                        Nev = ex.Message
                    };
                    result.Add(hiba);

                    return BadRequest(result);
                }
            }
        }

        [HttpGet("GyogyszerDTO")]
        public IActionResult GetGyogyszerDTO()
        {
            using (var context = new PatikaContext()) 
            {
                try
                {
                    List<GyogyszerNevHatoanyagDTO> result = context.Gyogyszers.Select(gy => new GyogyszerNevHatoanyagDTO()
                    {
                        Id = gy.Id,
                        Nev = gy.Nev,
                        Hatoanyag = gy.Hatoanyag
                    }).ToList();
                    return Ok(result);

                }
                catch (Exception ex)
                {
                    List<GyogyszerNevHatoanyagDTO> hibalista = new List<GyogyszerNevHatoanyagDTO>();
                    GyogyszerNevHatoanyagDTO hibaDTO = new GyogyszerNevHatoanyagDTO()
                    {
                        Id = -1,
                        Nev = ex.Message

                    };
                    hibalista.Add(hibaDTO);
                    return BadRequest(hibalista);
                }
            } 
        }
        [HttpPost("UjGyogyszer")]
        public IActionResult Post(Gyogyszer ujGyogyszer)
        {
            using (var context = new PatikaContext())
            {
                try
                {
                    context.Gyogyszers.Add(ujGyogyszer);
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
                    Gyogyszer torlendo = new Gyogyszer()
                    {
                        Id = id
                    };
                    context.Gyogyszers.Remove(torlendo);
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
        public IActionResult Put(Gyogyszer ujGyogyszer)
        {
            using(var context = new PatikaContext())
            {                       
                try
                {
                    if (context.Gyogyszers.Contains(ujGyogyszer))
                    {
                        context.Gyogyszers.Update(ujGyogyszer);
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

