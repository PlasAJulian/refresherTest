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
    public class employeesController : Controller
    {
        private readonly refresherTestContext _context;

        public employeesController(refresherTestContext context)
        {
            _context = context;
        }

        // GET: employees
        public async Task<IActionResult> Index()
        {
            List<employee> employeeList = new List<employee>();

            string connetionString = @"Data Source=(localdb)\local;Initial Catalog=master;Integrated Security=True";
            SqlConnection cnn;
            SqlCommand comm;
            SqlDataReader dataR;
            string sql;

            cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.WriteLine("Connection Open  !");

            sql = "Select * from employees";
            comm = new SqlCommand(sql, cnn);
            dataR = comm.ExecuteReader();

            employee e;

            while (dataR.Read())
            {
                e = new employee();
                e.ID = (int)dataR.GetValue(0);
                e.firstName = (string)dataR.GetValue(1);
                e.lastName = (string)dataR.GetValue(2);
                e.email = (string)dataR.GetValue(3);
                e.phoneNum = (string)dataR.GetValue(4);
                e.hireDate = (DateTime)dataR.GetValue(5);
                e.jobID = (int)dataR.GetValue(6);
                e.income = (decimal)dataR.GetValue(7);
                //e.managereID = dataR.GetValue(8);
                //e.departmentID = (string)dataR.GetValue(9);

                employeeList.Add(e);
            }

            dataR.Close();
            comm.Dispose();
            cnn.Close();

            return View(employeeList);
        }

        // GET: employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employee
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,firstName,lastName,email,phoneNum,hireDate,jobID,income,managereID,departmentID")] employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,firstName,lastName,email,phoneNum,hireDate,jobID,income,managereID,departmentID")] employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!employeeExists(employee.ID))
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
            return View(employee);
        }

        // GET: employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employee
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.employee.FindAsync(id);
            _context.employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool employeeExists(int id)
        {
            return _context.employee.Any(e => e.ID == id);
        }
    }
}
