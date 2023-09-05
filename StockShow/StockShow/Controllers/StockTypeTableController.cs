﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.StockTypeList = stockType_List; // 使用ViewBag傳遞資料
            return View(); 
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
                    return RedirectToAction(nameof(AddStockType2));
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

        
        public async Task<IActionResult> EditStockType(string id)
        {
            if(id==null || _context == null)
            {
                return NotFound();
            }

            StockTypeTable? stocktype = await _context.StockTypeTables.FindAsync(Convert.ToInt32(id));
            if(stocktype==null)
                return NotFound();

            List<StockTypeTable> stockType_List = _context.StockTypeTables.ToList();
            ViewBag.StockTypeList = stockType_List;
            //ViewData["StockTypeIndex"] = new SelectList(_context.StockTypeTables, "StockTypeIndex", "StockType");
            return View("EditStockType", stocktype);
        }

        // POST: StockNoTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStockType(string id, [Bind("StockTypeIndex,StockType")] StockTypeTable stockTypeTable)
        {
            if (Convert.ToInt32(id) != stockTypeTable.StockTypeIndex)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockTypeTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockTypeTableExists(stockTypeTable.StockTypeIndex))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AddStockType2));
            }
            ViewData["StockTypeIndex"] = new SelectList(_context.StockTypeTables, "StockTypeIndex", "StockType", stockTypeTable.StockTypeIndex);
            return View(stockTypeTable);
        }
        
        public async Task<IActionResult> EditStockType2( int StockTypeIndex_edit,string StockType_edit)
        {
            //if (Convert.ToInt32(StockTypeIndex_edit) != stockTypeTable.StockTypeIndex)
            //{
            //    return NotFound();
            //}
            StockTypeTable update_stockType = new StockTypeTable();
            update_stockType.StockTypeIndex = StockTypeIndex_edit;
            update_stockType.StockType = StockType_edit;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(update_stockType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockTypeTableExists(update_stockType.StockTypeIndex))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AddStockType2));
            }
            ViewData["StockTypeIndex"] = new SelectList(_context.StockTypeTables, "StockTypeIndex", "StockType", update_stockType.StockTypeIndex);
            return View(update_stockType);
        }

        // GET: StockType/Delete/5
        public async Task<IActionResult> DeleteStockType(int id)
        {
            if ( _context.StockTypeTables == null)
            {
                return NotFound();
            }

            StockTypeTable a = await _context.StockTypeTables
                .FirstOrDefaultAsync(m => m.StockTypeIndex == id);
            if (a == null)
            {
                return NotFound();  
            }

            return View(a);
        }

        // POST: StockTypeTables/Delete/5
        [HttpPost, ActionName("DeleteStockType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStockTypeConfirmed(int id)
        {
            if (_context.StockTypeTables == null)
            {
                return Problem("Entity set 'Stock_dbContext.StockTypeTables'  is null.");
            }
            var a = await _context.StockTypeTables.FindAsync(id);
            //StockTypeTable delete_stockType = new StockTypeTable();
            //delete_stockType.StockTypeIndex = a.StockTypeIndex;
            //delete_stockType.StockType = a.StockType;
            if (a != null)
            {
                _context.StockTypeTables.Remove(a);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        private bool StockTypeTableExists(int id)
        {
            return _context.StockTypeTables.Any(e => e.StockTypeIndex ==id);
        }
    }

    
}
