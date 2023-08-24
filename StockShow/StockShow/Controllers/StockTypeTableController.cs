using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using StockShow.Models;

namespace StockShow.Controllers
{
    public class StockTypeTableController : Controller
    {
        private readonly Stock_dbContext _context;

        public StockTypeTableController(Stock_dbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            var a = _context.StockTypeTables.ToList();
            return View(a);
        }
        public IActionResult List()
        {
            var a = _context.StockTypeTables.ToList();
            return View(a);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockType")]StockTypeTable stockTypeTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockTypeTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //給取DropDownList裡面的Item
            //ViewBag.StockTypeIndex = new SelectList(_context.StockTypeTables, "StockTypeIndex", "StockType", stockNoTable.StockTypeIndex);
            return View(stockTypeTable);
        }

        public IActionResult AddStockType()
        {
            var data = _context.StockTypeTables.ToList();
            return View(data); //使用model傳遞資料
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStockType([Bind("StockType")] StockTypeTable stockTypeTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockTypeTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var data = _context.StockTypeTables.ToList();
            //給取DropDownList裡面的Item
            //ViewBag.StockTypeIndex = new SelectList(_context.StockTypeTables, "StockTypeIndex", "StockType", stockNoTable.StockTypeIndex);
            return View("AddStockType",data);
        }

        public IActionResult AddStockType2()
        {
            List<StockTypeTable> stockType_List = _context.StockTypeTables.ToList();
            ViewBag.StockTypeList = stockType_List;
            return View(); // 使用ViewBag傳遞資料
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStockType2( [Bind("StockType")] StockTypeTable stockTypeTable)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(stockTypeTable);
                    _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex) 
                {
                    return NotFound();
                }
            }
            List<StockTypeTable> stockType_List = _context.StockTypeTables.ToList();
            ViewBag.StockTypeList = stockType_List;
            return View("AddStockType2");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStockType(string _id, [Bind("StockTypeIndex,StockType")] StockTypeTable stockNoTable)
        {
            if (ModelState.IsValid)
            {
                _context.Update(stockNoTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            List<StockTypeTable> stockType_List = _context.StockTypeTables.ToList();
            ViewBag.StockTypeList = stockType_List;
            return View("AddStockType2", stockType_List);
        }
    }
}
