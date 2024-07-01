using Attendance.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace Attendance.Api.Models
{
    public class Employees
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public RecordState Active { get; set; }
    }
}
