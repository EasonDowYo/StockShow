using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockShow.Models;

namespace StockShow.Controllers
{
    public class StockNoTablesController : Controller
    {
        private readonly Stock_dbContext _context;

        public StockNoTablesController(Stock_dbContext context)
        {
            _context = context;
        }

        // GET: StockNoTables
        public async Task<IActionResult> Index()
        {
            var stock_dbContext = _context.StockNoTables.Include(s => s.StockTypeIndexNavigation);
            return View(await stock_dbContext.ToListAsync());
        }

        // GET: StockNoTables/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.StockNoTables == null)
            {
                return NotFound();
            }

            var stockNoTable = await _context.StockNoTables
                .Include(s => s.StockTypeIndexNavigation)
                .FirstOrDefaultAsync(m => m.StockNo == id);
            if (stockNoTable == null)
            {
                return NotFound();
            }

            return View(stockNoTable);
        }

        // GET: StockNoTables/Create
        public IActionResult Create()
        {
            ViewBag.StockTypeIndex = new SelectList(_context.StockTypeTables, "StockTypeIndex", "StockType");
            return View();
        }

        // POST: StockNoTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockNo,StockName,StockType,StockTypeIndex,Note1,Note2,Note3,Note4,Note5,Note6,Note7")] StockNoTable stockNoTable)
        {
            stockNoTable.StockNoName = stockNoTable.StockNo + " " + stockNoTable.StockName;
            //List<StockTypeTable> a = _context.StockTypeTables.Where(o => o.StockTypeIndex == stockNoTable.StockTypeIndex).ToList();
            //stockNoTable.StockTypeIndexNavigation =new StockTypeTable { StockTypeIndex = stockNoTable.StockTypeIndex , StockType = a[0].StockType };

            if (ModelState.IsValid)
            {
                _context.Add(stockNoTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //給取DropDownList裡面的Item
            ViewBag.StockTypeIndex = new SelectList(_context.StockTypeTables, "StockTypeIndex", "StockType", stockNoTable.StockTypeIndex);
            return View(stockNoTable);
        }

        // GET: StockNoTables/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.StockNoTables == null)
            {
                return NotFound();
            }

            var stockNoTable = await _context.StockNoTables.FindAsync(id);
            if (stockNoTable == null)
            {
                return NotFound();
            }
            SelectList a = new SelectList(_context.StockTypeTables, "StockTypeIndex", "StockTypeIndex", stockNoTable.StockType);
            ViewData["StockTypeIndex"] = a;
            return View(stockNoTable);
        }

        // POST: StockNoTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StockNo,StockName,StockNoName,StockType,StockTypeIndex,Note1,Note2,Note3,Note4,Note5,Note6,Note7")] StockNoTable stockNoTable)
        {
            if (id != stockNoTable.StockNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockNoTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockNoTableExists(stockNoTable.StockNo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StockTypeIndex"] = new SelectList(_context.StockTypeTables, "StockTypeIndex", "StockTypeIndex", stockNoTable.StockTypeIndex);
            return View(stockNoTable);
        }

        // GET: StockNoTables/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.StockNoTables == null)
            {
                return NotFound();
            }

            var stockNoTable = await _context.StockNoTables
                .Include(s => s.StockTypeIndexNavigation)
                .FirstOrDefaultAsync(m => m.StockNo == id);
            if (stockNoTable == null)
            {
                return NotFound();
            }

            return View(stockNoTable);
        }

        // POST: StockNoTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.StockNoTables == null)
            {
                return Problem("Entity set 'Stock_dbContext.StockNoTables'  is null.");
            }
            var stockNoTable = await _context.StockNoTables.FindAsync(id);
            if (stockNoTable != null)
            {
                _context.StockNoTables.Remove(stockNoTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockNoTableExists(string id)
        {
            return _context.StockNoTables.Any(e => e.StockNo == id);
        }



        //////////
        // StockNoHandleAll
        //////////

        public IActionResult StockNoHandle()
        {
            // StockType Select Items
            var StockTypeIndex_List = new SelectList(_context.StockTypeTables, "StockTypeIndex", "StockType");
            ViewBag.StockTypeIndex = StockTypeIndex_List; // SelectList(資料表 ,選項的Value ,選項的Text)

            List<StockNoTable> stockNo_List = _context.StockNoTables.ToList();
            ViewBag.StockNoList = stockNo_List; // 使用ViewBag傳遞資料
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StockNoHandle([Bind("StockNo,StockName,StockType,StockTypeIndex,Note1,Note2,Note3,Note4,Note5,Note6,Note7")] StockNoTable _stockNoTable)
        {

            _stockNoTable.StockNoName = _stockNoTable.StockNo + " " + _stockNoTable.StockName;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(_stockNoTable);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(StockNoHandle));
                }
                catch(Exception e)
                {
                    return NotFound();
                }
            }
            List<StockNoTable> stockNo_List = _context.StockNoTables.ToList();
            ViewBag.StockNoList = stockNo_List; // 使用ViewBag傳遞資料
            // StockType Select Items
            ViewBag.StockTypeIndex = new SelectList(_context.StockTypeTables, "StockTypeIndex", "StockType"); // SelectList(資料表 ,選項的Value ,選項的Text)

            return View("StockNoHandle");
        }

    }
}
