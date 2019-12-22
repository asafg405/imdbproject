using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using CelebScraper;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Cors;


namespace Celebes.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class CelebsController : ControllerBase
    {
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public List<JObject> Celebes()
        {
            try
            {
                Scraper scraper = new Scraper();
                return scraper.GetActors();
            }
            catch (Exception e)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpDelete]
        [EnableCors("AllowOrigin")]
        public string  DeleteCeleb(string celebName)
        {
            Scraper scraper = new Scraper();
            try
            {
                scraper.DeleteCelebFromFile(celebName);
                return "Raw Was Deleted From File";
            }
            catch (Exception e) {
                return e+"Error deleting Raw";
            }
        }


        [HttpPost]
        [EnableCors("AllowOrigin")]
        public string ResetCelebsFile()
        {
            Scraper scraper = new Scraper();
            try
            {
                scraper.DeleteFile();
                return "Reset Was Success";
            }
            catch (Exception e)
            {
                return e + "Error Reset Celebs File";
            }
        }

    }
}
