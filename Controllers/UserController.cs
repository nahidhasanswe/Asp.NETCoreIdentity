using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthApp.Repository;
using AuthApp.Models;
using MongoDB.Driver;

namespace AuthApp.Controllers
{
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly UserRepository _userRepo = new UserRepository();

        [HttpGet]
        public async Task<IEnumerable<UserInfo>> Get()
        {
            return await _userRepo.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<UserInfo> Get(string id)
        {
            return await _userRepo.Get(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserInfo user)
        {
            try
            {
                await _userRepo.Add(user);
                return new JsonResult("Successfully Registration Complete");

            }catch (MongoException exc)
            {
                return BadRequest("Email or phone number is already exist");

            }catch
            {
                return BadRequest(user);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
