using ItechCleanArst.Application.Bussines.Categories.DTOs;
using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Categories.Queries
{
    public record GetCategoriesQuery() : IRequest<IEnumerable<CategoryDto>>;

    public sealed class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly IApplicationDbContext _dbcontext;

        public GetCategoriesQueryHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var category = await (from c in _dbcontext.Categories
                                  where !c.IsDeleted
                                  select new CategoryDto
                                  {
                                      Id = c.Id,
                                      Name = c.Name,
                                      CreatedDt = c.CreatedAt,
                                      UpdatedDt = c.UpdatedAt
                                  }).ToListAsync(cancellationToken);
            
            return category;
        }
    }
}