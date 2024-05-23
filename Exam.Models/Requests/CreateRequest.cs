namespace Exam.Models.Identity.Requests;

public class CreateRequest
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string CreatedBy { get; set; } = string.Empty;

}