using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BenefitsCostCalculator.Data;
using BenefitsCostCalculator.Models;
using Newtonsoft.Json;

namespace BenefitsCostCalculator.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;
        private const double DEFAULT_EMPLOYEE_COST = 1000;
        private const double DEFAULT_DEPENDENT_COST = 500;

        public EmployeeController(EmployeeContext employeeContext)
        {
            _context = employeeContext;
        }

        // GET: List of employees
        public async Task<IActionResult> Index()
        {
            var model = await _context.EmployeeModel.ToListAsync();
            ViewData["TotalCost"] = model.Sum(el => el.TotalCost);
            return View(model);
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.EmployeeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            employeeModel.Dependents = _context.DependentModel.Where(el => el.ParentId == employeeModel.Id).ToList();

            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,NumberOfDependents,Dependents")] EmployeeModel employeeModel, string dependents)
        {
            var dependentsList = JsonConvert.DeserializeObject<List<DependentModel>>(dependents);

            foreach (var dependent in dependentsList)
            {
                dependent.FirstName = dependent.FirstName.Trim();
                dependent.LastName = dependent.LastName.Trim();

                // if element is empty after trim, delete it
                if (string.IsNullOrEmpty(dependent.FirstName) && string.IsNullOrEmpty(dependent.LastName))
                {
                    dependentsList.Remove(dependent);
                }
            }

            employeeModel.NumberOfDependents = dependentsList.Count;

            if (ModelState.IsValid)
            {
                employeeModel.FirstName = employeeModel.FirstName.Trim();
                employeeModel.LastName = employeeModel.LastName.Trim();
                employeeModel.TotalCost = CalculateTotalCost(employeeModel, dependentsList);

                _context.Add(employeeModel);
                _context.SaveChanges();

                foreach (var dependent in dependentsList)
                {
                    dependent.ParentId = employeeModel.Id;
                    _context.Add(dependent);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.EmployeeModel.FindAsync(id);
            employeeModel.Dependents = _context.DependentModel.Where(el => el.ParentId == employeeModel.Id).ToList();

            if (employeeModel == null)
            {
                return NotFound();
            }
            return View(employeeModel);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumberOfDependents,Id,FirstName,LastName")] EmployeeModel employeeModel)
        {
            if (id != employeeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeModelExists(employeeModel.Id))
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
            return View(employeeModel);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.EmployeeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            employeeModel.Dependents = _context.DependentModel.Where(el => el.ParentId == employeeModel.Id).ToList();

            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dependents = _context.DependentModel.Where(el => el.ParentId == id).ToList();
            foreach (var el in dependents)
            {
                _context.DependentModel.Remove(el);
            }

            var employeeModel = await _context.EmployeeModel.FindAsync(id);
            _context.EmployeeModel.Remove(employeeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeModelExists(int id)
        {
            return _context.EmployeeModel.Any(e => e.Id == id);
        }

        private double CalculateTotalCost(EmployeeModel employee, List<DependentModel> dependents)
        {
            double totalCost = DEFAULT_EMPLOYEE_COST;
            if (employee.FirstName.ToLower().StartsWith("a"))
            {
                totalCost -= (DEFAULT_EMPLOYEE_COST * .1);
            }

            foreach (var dependent in dependents)
            {
                if (dependent.FirstName.ToLower().StartsWith("a"))
                {
                    totalCost += DEFAULT_DEPENDENT_COST - (DEFAULT_DEPENDENT_COST * .1);
                }
                else
                {
                    totalCost += DEFAULT_DEPENDENT_COST;
                }
            }

            return totalCost;
        }
    }
}
