using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using StockShow.Models;
using StockShow.STKDataQuery;
using System.Data;
using static StockShow.Controllers.DemoController;

namespace StockShow.Controllers
{
    public class StockReviewController : Controller
    {
        public IActionResult StockTrend()
        {
            var Employees = new List<SelectListItem>{
                new SelectListItem {Text = "Shyju", Value = "1"},
                new SelectListItem {Text = "Sean", Value = "2"}
            };



            List<SelectListItem> STKType = STKTypeQuery.GetAllSTKType();
            ViewBag.STKTypeList = STKType;

            List<SelectListItem> STKNoList = STKNoQuery.GetStockNo_BySTKTypeIndex(1);
            ViewBag.STKNoList = STKNoList;


            //ViewBag.Employees = Employees;
            return View();
        }

        [HttpPost]
        public JsonResult Get5DaysData_Post([FromBody] Models.StockDataTable input_json)
        {

            if (input_json.StockNo != null)
            {
                StockDataQuery STKDataQuery = new StockDataQuery();
                DataTable dt = STKDataQuery.Get5DaysData(input_json.StockNo);
                string json_output = JsonConvert.SerializeObject(dt);

                return Json(json_output);
            }

            return Json(null);
        }
        
    }
}
