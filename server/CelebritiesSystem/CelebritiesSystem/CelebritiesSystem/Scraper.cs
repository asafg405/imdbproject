using IronWebScraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace CelebScraper
{
    public class Scraper : WebScraper
    {
        private string CelebesFileName = "Celebes.Json";


        public void DeleteFile()
        {
            File.Delete(CelebesFileName);
        }

        public void DeleteCelebFromFile(string celebName)
        {
            var celeb = File.ReadAllLines(CelebesFileName).Where(line => !line.Trim().Contains(celebName)).ToArray();
            File.WriteAllLines(CelebesFileName, celeb);
        }

        public int CountFileLines(string fileName)
        {
            if (File.Exists(fileName))
            {
                return File.ReadLines(CelebesFileName).Count();
            }
            return 0;
        }
        public List<JObject> GetActors()
        {
            List<JObject> CelebesList = new List<JObject>();
            int fileLines = CountFileLines(CelebesFileName);
            if (fileLines == 0)
            {
                Start();
            }
            using (StreamReader fileReader = new StreamReader(CelebesFileName))
            {
                while (!fileReader.EndOfStream)
                {
                    var celebData = JObject.Parse(fileReader.ReadLine());
                    CelebesList.Add(celebData); 
                }
            }
            return CelebesList;
        }

        public override void Init()
        {
            DeleteFile();
            var url = "https://www.imdb.com/list/ls052283250/";
            this.Request(url, Parse);
        }

        public override void Parse(Response response)
        {
            this.WorkingDirectory = Directory.GetCurrentDirectory();
            foreach (var celeb_Info in response.Css("div.lister-item.mode-detail"))
            {
                var celebImgURL = celeb_Info.Css("div.lister-item-image>a>img")[0].Attributes["src"];
                var celebName = celeb_Info.Css("h3")[0].InnerTextClean;
                var celebRole = celeb_Info.Css("p")[0].InnerTextClean; 
                Scrape(new ScrapedData() { { "name", celebName }, { "Role", celebRole }, { "URL", celebImgURL } }, CelebesFileName);
            }
        }
    }
}
