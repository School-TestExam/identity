using Exam.Core.Persistence.Models.Entities;

namespace Exam.Models.Identity.Identity
{
    public class Identity : EntityBase<Guid>
    {
        public string Email { get; set; }
        public string Username {  get; set; }
        public string Password { get; set; } //Plaintext, but who gives a fuck?
        public string FullName { get; set; }
    }
}
