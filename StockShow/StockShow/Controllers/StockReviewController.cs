using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockShow.STKDataQuery;
using System.Data;
using static StockShow.Controllers.DemoController;

namespace StockShow.Controllers
{
    public class StockReviewController : Controller
    {
        public IActionResult StockTrend()
        {
            
            return View();
        }

        [HttpPost]
        public JsonResult Get5DaysData_Post([FromBody] Models.StockDataTable input_json)
        {
            



            StockDataQuery query = new StockDataQuery();
            DataTable dt = query.Get5DaysData(input_json.StockNo);

            

            string json_output =  JsonConvert.SerializeObject(dt);

            return Json(json_output);
            
        }
    }
}
