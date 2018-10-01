using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cars.Customers.Crud;
using Cars.Web.Models.Customers;
using Cars.Customers.Crud.Entities;

namespace Cars.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomersDbContext context;

        public CustomersController(CustomersDbContext context)
        {
            this.context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var list = await this.context.Customers.Select(x => x.ToViewModel()).ToListAsync();
            return View(list);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await this.context.Customers.FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer.ToViewModel());
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,IdentityNo,FirstName,LastName,City,Street,Number,Phone")] CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer(viewModel.Type, viewModel.IdentityNo, viewModel.FirstName, viewModel.LastName, viewModel.City, viewModel.Street,
                                            viewModel.Number, viewModel.Phone);
                context.Add(customer);
                await this.context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await this.context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer.ToViewModel());
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,IdentityNo,FirstName,LastName,City,Street,Number,Phone")] CustomerViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var customer = await this.context.Customers.FindAsync(id);
                    customer.Update(viewModel.Type, viewModel.IdentityNo, viewModel.FirstName, viewModel.LastName, viewModel.City, viewModel.Street, viewModel.Number, viewModel.Phone);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(viewModel.Id))
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
            return View(viewModel);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await this.context.Customers.FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer.ToViewModel());
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await this.context.Customers.FindAsync(id);
            this.context.Customers.Remove(customer);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return this.context.Customers.Any(e => e.Id == id);
        }
    }
}
