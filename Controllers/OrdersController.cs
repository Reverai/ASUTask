using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASUTask.Data;
using ASUTask.Models;
using ASUTask.ViewModels;
using System.Collections;

namespace ASUTask.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AsuDbContext _context;

        public OrdersController(AsuDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public IActionResult Index(DateTime? startDate, DateTime? endDate, int? provider, int? orderNumber)
        {
            if (startDate == null && endDate == null)
            {
                startDate = DateTime.Now.AddMonths(-1);
                endDate = DateTime.Now;
            }
            

            var orders = _context.Orders
                .Where(x => x.Date > startDate && x.Date < endDate).Include(o => o.Provider)
                .ToList();



            if (provider != null && provider != 0)
            {
                orders = orders.Where(p => p.ProviderId == provider).ToList();
            }
            if (orderNumber != null && orderNumber != 0)
            {
                orders = orders.Where(p => p.Id == orderNumber).ToList();
            }

            List<Provider> providers = _context.Providers.ToList();
            providers.Insert(0, new Provider { Name = "Все", Id = 0 });

            List<Order> orderNumbers = _context.Orders.ToList();
            orderNumbers.Insert(0, new Order { Number = "Все", Id = 0 });

            var viewModel = new OrdersFilter(orders, startDate, endDate);
            viewModel.Providers = new SelectList(providers, "Id", "Name");
            viewModel.OrderNumbers = new SelectList(orderNumbers, "Id", "Number");

            return View(viewModel);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id, int? name, int? unit)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = new ViewItems();
            viewModel.Order = await _context.Orders
                .Include(o => o.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);
            viewModel.OrderItems = _context.OrderItems.Where(i => i.OrderId == id);


            if (name != null && name != 0)
            {
                viewModel.OrderItems = viewModel.OrderItems.Where(p => p.Id == name).ToList();
            }if (unit != null && unit != 0)
            {
                viewModel.OrderItems = viewModel.OrderItems.Where(p => p.Id == unit).ToList();
            }


            List<OrderItem> orderItems = _context.OrderItems.Where(i => i.OrderId == id).ToList();
            orderItems.Insert(0, new OrderItem { Name = "Все", Id = 0 });

            viewModel.Names = new SelectList(orderItems, "Id", "Name");
            viewModel.Units = new SelectList(orderItems, "Id", "Unit");

            

/*            var order = await _context.Orders
                .Include(o => o.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);*/
/*            if (order == null)
            {
                return NotFound();
            }*/
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Date,ProviderId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { order.Id });
            }
            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Id", order.ProviderId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Id", order.ProviderId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Date,ProviderId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Id", order.ProviderId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
