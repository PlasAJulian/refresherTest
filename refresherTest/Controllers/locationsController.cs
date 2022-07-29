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
    public class locationsController : Controller
    {
        private readonly refresherTestContext _context;

        public locationsController(refresherTestContext context)
        {
            _context = context;
        }

        // GET: locations
        public async Task<IActionResult> Index()
        {
            List<location> locationList = new List<location>();

            string connetionString = @"Data Source=(localdb)\local;Initial Catalog=master;Integrated Security=True";
            SqlConnection cnn;
            SqlCommand comm;
            SqlDataReader dataR;
            string sql;

            cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.WriteLine("Connection Open  !");

            sql = "Select * from locations";
            comm = new SqlCommand(sql, cnn);
            dataR = comm.ExecuteReader();

            location l;

            while (dataR.Read())
            {
                l = new location();
                l.ID = (int)dataR.GetValue(0);
                l.address = (string)dataR.GetValue(1);
                l.postalCode = (string)dataR.GetValue(2);
                l.city = (string)dataR.GetValue(3);
                l.state = (string)dataR.GetValue(4);
                l.countryID = (string)dataR.GetValue(5);

                locationList.Add(l);
            }

            dataR.Close();
            comm.Dispose();
            cnn.Close(); 

            return View(locationList);
        }

        // GET: locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.location
                .FirstOrDefaultAsync(m => m.ID == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: locations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,address,postalCode,city,state,countryID")] location location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,address,postalCode,city,state,countryID")] location location)
        {
            if (id != location.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!locationExists(location.ID))
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
            return View(location);
        }

        // GET: locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.location
                .FirstOrDefaultAsync(m => m.ID == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.location.FindAsync(id);
            _context.location.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool locationExists(int id)
        {
            return _context.location.Any(e => e.ID == id);
        }
    }
}
