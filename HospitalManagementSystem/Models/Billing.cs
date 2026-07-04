using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Models
{
    public class Billing
    {
        [Key]
        public int BillId { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [ForeignKey("AppointmentId")]
        public Appointment? Appointment { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ConsultationFee { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LabFee { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MedicineFee { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OtherCharges { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public DateTime BillDate { get; set; } = DateTime.Now;
    }
}