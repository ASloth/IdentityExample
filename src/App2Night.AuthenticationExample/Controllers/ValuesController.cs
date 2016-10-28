using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App2Night.AuthenticationExample.Controllers
{
    [Authorize] //-> Endpunkte benötigen Authentifizierung
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        //Öffentliche Werte die ohne Authentifizierung abgerufen werden können.
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

         
        [HttpGet("{id}")]
        public string Get(int id)
        { 
            var caller = User as ClaimsPrincipal;
            var userInformation = caller.Claims.FirstOrDefault(o => o.Type == "name");

            var result = $"Hello  {userInformation} your super super secret is: secret, wow.";


            foreach (Claim claim in caller.Claims)
            {
                result += "\n" + claim.Type + " " + claim.Value;
            }
            return result;
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
