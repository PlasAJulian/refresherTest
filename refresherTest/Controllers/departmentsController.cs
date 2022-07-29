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
    public class departmentsController : Controller
    {
        private readonly refresherTestContext _context;

        public departmentsController(refresherTestContext context)
        {
            _context = context;
        }

        // GET: departments
        public async Task<IActionResult> Index()
        {
            List<department> departmentList = new List<department>();

            string connetionString = @"Data Source=(localdb)\local;Initial Catalog=master;Integrated Security=True";
            SqlConnection cnn;
            SqlCommand comm;
            SqlDataReader dataR;
            string sql;

            cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.WriteLine("Connection Open  !");

            sql = "Select * from departments";
            comm = new SqlCommand(sql, cnn);
            dataR = comm.ExecuteReader();

            department d;

            while (dataR.Read())
            {
                d = new department();
                d.ID = (int)dataR.GetValue(0);
                d.departmentName = (string)dataR.GetValue(1);
                d.locationID = (int)dataR.GetValue(2);

                Console.WriteLine("*********************");
                departmentList.Add(d);
            }

            dataR.Close();
            comm.Dispose();
            cnn.Close();
            return View(departmentList);
        }

        // GET: departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.department
                .FirstOrDefaultAsync(m => m.ID == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,departmentName,locationID")] department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,departmentName,locationID")] department department)
        {
            if (id != department.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!departmentExists(department.ID))
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
            return View(department);
        }

        // GET: departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.department
                .FirstOrDefaultAsync(m => m.ID == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.department.FindAsync(id);
            _context.department.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool departmentExists(int id)
        {
            return _context.department.Any(e => e.ID == id);
        }
    }
}
