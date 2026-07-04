
using HospitalManagementSystem.Controllers;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class BillingsController : BaseController
{
    private readonly HospitalDbContext _context;

    public BillingsController(HospitalDbContext context)
    {
        _context = context;
    }

    // GET: BILLINGS
    public async Task<IActionResult> Index()
    {
        var billings = _context.Billings
            .Include(b => b.Appointment)
                .ThenInclude(a => a.Patient)
            .Include(b => b.Appointment)
                .ThenInclude(a => a.Doctor);

        return View(await billings.ToListAsync());
    }

    // GET: BILLINGS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var billing = await _context.Billings
            .Include(b => b.Appointment)
                .ThenInclude(a => a.Patient)
            .Include(b => b.Appointment)
                .ThenInclude(a => a.Doctor)
            .FirstOrDefaultAsync(b => b.BillId == id);

        if (billing == null)
            return NotFound();

        return View(billing);
    }

    // GET: BILLINGS/Create
    public IActionResult Create()
    {
        ViewData["AppointmentId"] = new SelectList(
            _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToList(),
            "AppointmentId",
            "AppointmentId");

        return View();
    }

    // POST: BILLINGS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
     [Bind("BillId,AppointmentId,ConsultationFee,LabFee,MedicineFee,OtherCharges,BillDate")]
    Billing billing)
    {
        if (ModelState.IsValid)
        {
            billing.TotalAmount =
                billing.ConsultationFee +
                billing.LabFee +
                billing.MedicineFee +
                billing.OtherCharges;

            _context.Add(billing);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewData["AppointmentId"] = new SelectList(
            _context.Appointments,
            "AppointmentId",
            "AppointmentId",
            billing.AppointmentId);

        return View(billing);
    }

    // GET: BILLINGS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var billing = await _context.Billings.FindAsync(id);

        if (billing == null)
            return NotFound();

        ViewData["AppointmentId"] = new SelectList(
            _context.Appointments,
            "AppointmentId",
            "AppointmentId",
            billing.AppointmentId);

        return View(billing);
    }

    // POST: BILLINGS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
    int id,
    [Bind("BillId,AppointmentId,ConsultationFee,LabFee,MedicineFee,OtherCharges,BillDate")]
    Billing billing)
    {
        if (id != billing.BillId)
            return NotFound();

        if (ModelState.IsValid)
        {
            billing.TotalAmount =
                billing.ConsultationFee +
                billing.LabFee +
                billing.MedicineFee +
                billing.OtherCharges;

            try
            {
                _context.Update(billing);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillingExists(billing.BillId))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["AppointmentId"] = new SelectList(
            _context.Appointments,
            "AppointmentId",
            "AppointmentId",
            billing.AppointmentId);

        return View(billing);
    }
    // GET: BILLINGS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var billing = await _context.Billings
            .Include(b => b.Appointment)
            .FirstOrDefaultAsync(m => m.BillId == id);

        if (billing == null)
            return NotFound();

        return View(billing);
    }
    // POST: BILLINGS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var billing = await _context.Billings.FindAsync(id);

        if (billing != null)
        {
            _context.Billings.Remove(billing);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult GetAppointmentDetails(int id)
    {
        var appointment = _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefault(a => a.AppointmentId == id);

        if (appointment == null)
        {
            return NotFound();
        }

        return Json(new
        {
            patient = appointment.Patient?.FullName,
            doctor = appointment.Doctor?.DoctorName,
            consultationFee = appointment.Doctor?.ConsultationFee ?? 0
        });
    }
    // GET: BILLINGS/Invoice/5
    public async Task<IActionResult> Invoice(int? id)
    {
        if (id == null)
            return NotFound();

        var billing = await _context.Billings
            .Include(b => b.Appointment)
                .ThenInclude(a => a.Patient)
            .Include(b => b.Appointment)
                .ThenInclude(a => a.Doctor)
            .FirstOrDefaultAsync(b => b.BillId == id);

        if (billing == null)
            return NotFound();

        return View(billing);
    }
    private bool BillingExists(int id)
    
    {
        return _context.Billings.Any(e => e.BillId == id);
    }
}
