using Exam.Models.Identity.Requests;
using Exam.Models.Identity.Responses;
using Exam.Services.Identity.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Exam.Services.Identity.Services
{
    public interface IIdentityService
    {
        public Task<Guid> Create(CreateRequest request);

        public Task Delete(Guid id);

        public Task<GetResponse> Get(GetRequest request);

        public Task<GetResponse> Update(UpdateRequest request);
    }

    public class IdentityService : IIdentityService
    {
        private readonly IdentityContext _context;

        public IdentityService(IdentityContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(CreateRequest request)
        {
            if (string.IsNullOrEmpty(request.CreatedBy))
            {
                request.CreatedBy = "SYSTEM";
            }

            var entity = request.Adapt<Models.Entities.Identity.Identity>();

            entity.CreatedAt = DateTime.Now;

            _context.Identities.Add(entity);

            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(Guid id)
        {
            var entity = await _context.Identities.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
            {
                return;
            }

            _context.Identities.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<GetResponse> Get(GetRequest request)
        {
            var entity = await _context.Identities.FirstOrDefaultAsync(x => x.Id == request.Id);

            var response = entity.Adapt<GetResponse>();

            return response;
        }

        public async Task<GetResponse> Update(UpdateRequest request)
        {
            var entity = await _context.Identities.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (entity is null)
            {
                return null;
            }

            entity.Id = request.Id;
            entity.FullName = request.FullName;
            entity.Username = request.Username;
            entity.Password = request.Password;
            entity.Email = request.Email;

            await _context.SaveChangesAsync();

            var response = entity.Adapt<GetResponse>();

            return response;
        }
    }
}