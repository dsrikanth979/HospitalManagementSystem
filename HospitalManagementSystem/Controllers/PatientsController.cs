
using HospitalManagementSystem.Controllers;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PatientsController : BaseController
{
    private readonly HospitalDbContext _context;

    public PatientsController(HospitalDbContext context)
    {
        _context = context;
    }

    // GET: PATIENTS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Patients.ToListAsync());
    }

    // GET: Patients/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var patient = await _context.Patients
            .FirstOrDefaultAsync(m => m.PatientId == id);

        if (patient == null)
            return NotFound();

        return View(patient);
    }

    // GET: PATIENTS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PATIENTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PatientId,FullName,Age,Gender,Phone,Email,Address,Disease,DoctorName,AdmissionDate")] Patient patient)
    {
        if (ModelState.IsValid)
        {
            _context.Add(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(patient);
    }

    // GET: PATIENTS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var patient = await _context.Patients.FindAsync(id);

        if (patient == null)
            return NotFound();

        return View(patient);
    }

    // POST: PATIENTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Patient patient)
    {
        if (id != patient.PatientId)
            return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(patient);
    }

    // GET: PATIENTS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var patient = await _context.Patients
            .FirstOrDefaultAsync(m => m.PatientId == id);

        if (patient == null)
            return NotFound();

        return View(patient);
    }

    // POST: PATIENTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var patient = await _context.Patients.FindAsync(id);

        if (patient != null)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
