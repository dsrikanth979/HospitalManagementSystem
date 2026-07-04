using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string Specialization { get; set; }

        public string Qualification { get; set; }

        public int Experience { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ConsultationFee { get; set; }
    }
    }
