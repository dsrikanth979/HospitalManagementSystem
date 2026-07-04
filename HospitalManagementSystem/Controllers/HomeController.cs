using HospitalManagementSystem.Controllers;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class HomeController : BaseController
{
    private readonly HospitalDbContext _context;

    public HomeController(HospitalDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Check whether the user is logged in
       

        DashboardViewModel model = new DashboardViewModel();

        model.TotalPatients = _context.Patients.Count();
        model.TotalDoctors = _context.Doctors.Count();
        model.TotalAppointments = _context.Appointments.Count();

        model.TotalRevenue = _context.Billings.Sum(b => (decimal?)b.TotalAmount) ?? 0;

        return View(model);
    }   
}