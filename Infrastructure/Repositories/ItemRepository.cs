using Application.Common.Repositories;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class ItemRepository(ItemsDbContext dbContext) : Repository<Item>(dbContext), IItemRepository
{
	
}