using System.ComponentModel.DataAnnotations;

namespace Firotechbd.Areas.Admin.Models
{
    public class Employee
    {
    }
    public class EmployeePubProfile
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Slug { get; set; }

        [StringLength(255)]
        public string? EmployeeProfileImageUrl { get; set; }

        [StringLength(100)]
        public string? EmployeeName { get; set; }

        [StringLength(30)] // Assuming a phone number limit
        public string? EmployeePhone { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? EmployeeEmail { get; set; }

        [StringLength(255)]
        public string? EmployeeAddress { get; set; }

        [StringLength(50)]
        public string? EmployeePosition { get; set; }

        [StringLength(20)] // Status can have a specific limit
        public string? EmployeeStatus { get; set; }

        [StringLength(1000)]  
        public string? EmployeeAboutme { get; set; } //about me section

        [StringLength(100)]
        public string? Employeefb { get; set; } // Facebook URL

        [StringLength(100)]
        public string? Employeewp { get; set; } // WhatsApp link

        [StringLength(100)]
        public string? Employeex { get; set; } // Twitter or X URL

        [StringLength(100)]
        public string? EmployeeMailHot { get; set; } // Hotmail or other mail

        [StringLength(100)]
        public string? EmployeeLinkedin { get; set; } // LinkedIn URL

        // Admin Access
        public bool IsActive { get; set; }

        public Guid CreateBy { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public Guid UpdateBy { get; set; }
    }
}
