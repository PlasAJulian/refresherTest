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
    public class regionsController : Controller
    {
        private readonly refresherTestContext _context;

        public regionsController(refresherTestContext context)
        {
            _context = context;
        }

        // GET: regions
        public async Task<IActionResult> Index()
        {
            List<region> regionList = new List<region>();

            string connetionString = @"Data Source=(localdb)\local;Initial Catalog=master;Integrated Security=True";
            SqlConnection cnn;
            SqlCommand comm;
            SqlDataReader dataR;
            string sql;

            cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.WriteLine("Connection Open  !");

            sql = "Select * from regions";
            comm = new SqlCommand(sql, cnn);
            dataR = comm.ExecuteReader();

            region r;

            while (dataR.Read())
            {
                r = new region();
                r.ID = (int)dataR.GetValue(0);
                r.regionName = (string)dataR.GetValue(1);

                regionList.Add(r);
            }

            dataR.Close();
            comm.Dispose();
            cnn.Close();
            return View(regionList);
        }

        // GET: regions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.region
                .FirstOrDefaultAsync(m => m.ID == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: regions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: regions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,regionName")] region region)
        {
            if (ModelState.IsValid)
            {
                _context.Add(region);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        // GET: regions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.region.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }

        // POST: regions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,regionName")] region region)
        {
            if (id != region.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(region);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!regionExists(region.ID))
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
            return View(region);
        }

        // GET: regions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.region
                .FirstOrDefaultAsync(m => m.ID == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // POST: regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var region = await _context.region.FindAsync(id);
            _context.region.Remove(region);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool regionExists(int id)
        {
            return _context.region.Any(e => e.ID == id);
        }
    }
}
