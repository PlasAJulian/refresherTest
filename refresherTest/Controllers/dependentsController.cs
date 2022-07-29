using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using refresherTest.Data;
using refresherTest.Models;

namespace refresherTest.Controllers
{
    public class dependentsController : Controller
    {
        private readonly refresherTestContext _context;

        public dependentsController(refresherTestContext context)
        {
            _context = context;
        }

        // GET: dependents
        public async Task<IActionResult> Index()
        {
            List<dependent> dependentList = new List<dependent>();

            string connetionString = @"Data Source=(localdb)\local;Initial Catalog=master;Integrated Security=True";
            SqlConnection cnn;
            SqlCommand comm;
            SqlDataReader dataR;
            string sql;

            cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.WriteLine("Connection Open  !");

            sql = "Select * from dependents";
            comm = new SqlCommand(sql, cnn);
            dataR = comm.ExecuteReader();

            dependent d;

            while (dataR.Read())
            {
                d = new dependent();
                d.ID = (int)dataR.GetValue(0);
                d.firstName = (string)dataR.GetValue(1);
                d.lastName = (string)dataR.GetValue(2);
                d.relationship = (string)dataR.GetValue(3);
                d.employeeID = (int)dataR.GetValue(4);

                dependentList.Add(d);
            }

            dataR.Close();
            comm.Dispose();
            cnn.Close();

            return View(dependentList);
        }

        // GET: dependents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.dependent
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dependent == null)
            {
                return NotFound();
            }

            return View(dependent);
        }

        // GET: dependents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: dependents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,firstName,lastName,relationship,employeeID")] dependent dependent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dependent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dependent);
        }

        // GET: dependents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.dependent.FindAsync(id);
            if (dependent == null)
            {
                return NotFound();
            }
            return View(dependent);
        }

        // POST: dependents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,firstName,lastName,relationship,employeeID")] dependent dependent)
        {
            if (id != dependent.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dependent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!dependentExists(dependent.ID))
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
            return View(dependent);
        }

        // GET: dependents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependent = await _context.dependent
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dependent == null)
            {
                return NotFound();
            }

            return View(dependent);
        }

        // POST: dependents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dependent = await _context.dependent.FindAsync(id);
            _context.dependent.Remove(dependent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool dependentExists(int id)
        {
            return _context.dependent.Any(e => e.ID == id);
        }
    }
}
