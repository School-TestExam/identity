using System.ComponentModel;

namespace Exam.Models.Identity.Requests
{
    public class UpdateRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        
        [DefaultValue("SYSTEM")]
        public string LastUpdatedBy { get; set; } = "SYSTEM";
    }
}
