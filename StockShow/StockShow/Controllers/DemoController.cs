using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
namespace StockShow.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return RedirectToAction(nameof(Index3)); //切換到Index3的內容
        }
        public IActionResult Index3()
        {
            return View("Index");//切換到Index的內容
        }

        public IActionResult test()
        {
            return View();
        }
        public IActionResult TestAJAX()
        {
            return View();
        }

        public IActionResult TestFetch()
        {
            return View();
        }

        // Controller
        //[HttpPost]
        public IActionResult CallFetch1()
        {
            List<MyClass> myClasses = new List<MyClass>();
            myClasses.Add(new MyClass() { id = 1, name = "AAA" });
            MyClass myClass = new MyClass() { id = 2, name = "BBB" };
            myClasses.Add(myClass);
            string a = JsonConvert.SerializeObject(myClasses).ToString();
            return Content(a);
        }

        [HttpPost]
        public IActionResult CallFetch2([FromBody] MyClass input_json)
        {
            List<MyClass> myClasses = new List<MyClass>();

            myClasses.Add(new MyClass() { id = 1, name = "AAA" });

            MyClass myClass = new MyClass() { id = 2, name = "BBB" };

            List<MyClass> myClasses2 = new List<MyClass>()
            {
            new MyClass() { id = 3, name = "CCC" },
            new MyClass() { id = 4, name = "DDD" }
            };
            myClasses.Add(myClass);
            myClasses.AddRange(myClasses2);

            List<MyClass> myclasses_after = myClasses.Where(o => o.id == Convert.ToInt32(input_json.id) && o.name == input_json.name).ToList();

            string a =JsonConvert.SerializeObject(myclasses_after).ToString();
            return Content(a);
        }

        public IActionResult TestAjax1()
        {
            return Content("ResurnString");
        }

        public IActionResult TestAjax2_withValue(string str1,int int1)
        {
            string stri = str1 + int1.ToString();
            return Content(stri);
        }
        
        public IActionResult TestAjax3_withObj( MyClass myClass)
        {
            string a = System.Text.Json.JsonSerializer.Serialize(myClass);
            return Json(a);
        }


        public IActionResult tableFilterSample()
        {
            return View();
        }


        public IActionResult MyModalTest()
        {
            return View();
        }
        public class MyClass
        {
            public int id { get; set; }
            public string? name { get; set; }
        }
    }
}
