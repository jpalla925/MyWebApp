using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly MyWebAppDbContext _context;

        public ExpenseController(MyWebAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var expenses = await _context.Expenses.ToListAsync();
            return View(expenses);
        }

        public IActionResult CreateEdit(int? id)
        {
            if (id.HasValue)
            {
                // Editing: load the expense by id
                var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id.Value);
                if (expenseInDb == null)
                {
                    return NotFound(); // Handle case where expense does not exist
                }
                return View(expenseInDb);
            }
            // Creating: return an empty view
            return View(new Expense());
        }


        public IActionResult Delete(int id)
        {
            var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            if (expenseInDb == null)
            {
                return NotFound(); // Handle case where expense does not exist
            }
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index)); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(Expense expense)
        {
            if (ModelState.IsValid)
            {
                if (expense.Id == 0) // Create new expense
                {
                    _context.Add(expense);
                }
                else // Update existing expense
                {
                    var expenseInDb = _context.Expenses.SingleOrDefault(e => e.Id == expense.Id);
                    if (expenseInDb == null)
                    {
                        return NotFound(); // Handle case where expense does not exist
                    }
                    expenseInDb.Description = expense.Description;
                    expenseInDb.Value = expense.Value;
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }
    }
}