using Exam.Abstractions.Exceptions;
using Exam.Models.Identity.DTO;
using Exam.Models.Identity.Requests;
using Exam.Services.Identity.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Exam.Services.Identity.Services
{
    public interface IIdentityService
    {
        public Task<IdentityDTO> Create(CreateRequest request);

        public Task Delete(Guid id);

        public Task<IdentityDTO> Get(Guid id);

        public Task<IdentityDTO> Update(Guid id, UpdateRequest request);
    }

    public class IdentityService : IIdentityService
    {
        private readonly IdentityContext _context;

        public IdentityService(IdentityContext context)
        {
            _context = context;
        }

        public async Task<IdentityDTO> Create(CreateRequest request)
        {
            var entity = request.Adapt<Models.Entities.Identity.Identity>();

            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            entity.LastUpdatedBy = string.Empty;

            _context.Identities.Add(entity);

            await _context.SaveChangesAsync();

            return entity.Adapt<IdentityDTO>();
        }

        public async Task Delete(Guid id)
        {
            var entity = await _context.Identities.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new NotFoundException($"Couldn't find entity with id: {id}");
            }

            _context.Identities.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<IdentityDTO> Get(Guid id)
        {
            var entity = await _context.Identities.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new NotFoundException($"Couldn't find entity with id: {id}");
            }

            return entity.Adapt<IdentityDTO>();
        }

        public async Task<IdentityDTO> Update(Guid id, UpdateRequest request)
        {
            var entity = await _context.Identities.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new NotFoundException($"Couldn't find entity with id: {id}");
            }

            request.Adapt(entity);
            entity.UpdatedAt = DateTime.Now;

            _context.Identities.Update(entity);

            await _context.SaveChangesAsync();

            return entity.Adapt<IdentityDTO>();
        }
    }
}