
using HospitalManagementSystem.Controllers;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class DoctorsController : BaseController
{
    private readonly HospitalDbContext _context;

    public DoctorsController(HospitalDbContext context)
    {
        _context = context;
    }

    // GET: DOCTORS
    // GET: DOCTORS
    public async Task<IActionResult> Index()
    {
        if (HttpContext.Session.GetString("Role") != "Admin")
        {
            return RedirectToAction("Index", "Home");
        }

        return View(await _context.Doctors.ToListAsync());
    }

    // GET: DOCTORS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(m => m.DoctorId == id);

        if (doctor == null)
            return NotFound();

        return View(doctor);
    }

    // GET: DOCTORS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DOCTORS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("DoctorId,DoctorName,Specialization,Qualification,Experience,Phone,Email,ConsultationFee")] Doctor doctor)
    {
        if (ModelState.IsValid)
        {
            _context.Add(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(doctor);
    }

    // GET: DOCTORS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var doctor = await _context.Doctors.FindAsync(id);

        if (doctor == null)
            return NotFound();

        return View(doctor);
    }

    // POST: DOCTORS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? doctorid, [Bind("DoctorId,DoctorName,Specialization,Qualification,Experience,Phone,Email,ConsultationFee")] Doctor doctor)
    {
        if (doctorid != doctor.DoctorId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(doctor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(doctor.DoctorId))
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
        return View(doctor);
    }

    // GET: DOCTORS/Delete/5
   public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
        return NotFound();

    var doctor = await _context.Doctors
        .FirstOrDefaultAsync(m => m.DoctorId == id);

    if (doctor == null)
        return NotFound();

    return View(doctor);
}
    // POST: DOCTORS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? doctorid)
    {
        var doctor = await _context.Doctors.FindAsync(doctorid);
        if (doctor != null)
        {
            _context.Doctors.Remove(doctor);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DoctorExists(int? doctorid)
    {
        return _context.Doctors.Any(e => e.DoctorId == doctorid);
    }
}
