using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        public string FullName { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? Disease { get; set; }

        public string? DoctorName { get; set; }

        public DateTime AdmissionDate { get; set; }
    }
}