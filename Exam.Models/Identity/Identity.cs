using Exam.Core.Persistence.Models.Entities;

namespace Exam.Models.Identity.Identity
{
    public class Identity// : EntityBase<Guid>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
