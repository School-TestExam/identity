using IdentityModel = Exam.Models.Identity.Identity.Identity;

namespace Exam.Services.Identity.Services
{
    public interface IIdentityService
    {
        public Task<IdentityModel> CreateIdentity(IdentityModel identity);
        public Task<bool> DeleteIdentity(Guid id);
        public Task<IdentityModel> GetIdentity(Guid id);
        public Task<IdentityModel> PutIdentity(Guid id, IdentityModel user);
    }

    public class IdentityService : IIdentityService
    {
        public IdentityService() { }

        public Task<IdentityModel> CreateIdentity(IdentityModel identity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteIdentity(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityModel> GetIdentity(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityModel> PutIdentity(Guid id, IdentityModel user)
        {
            throw new NotImplementedException();
        }
    }
}