using Attendance.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance.Api.Models
{
    public class Delivery
    {
        [Key]
        public string Id { get; set; }
        public string EmployeeId {get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employees Employees { get; set; }
        public DeliveryStatus Status { get; set; }  
        public RecordState Active { get; set; }
    }
}
