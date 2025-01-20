using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;
public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    private readonly DbSet<Customer> _customer;
    public CustomerRepository(ApplicationDbContext dbContext, Func<CacheTech, ICacheService> cacheService) : base(dbContext, cacheService)
    {
        _customer = dbContext.Set<Customer>();
    }
}