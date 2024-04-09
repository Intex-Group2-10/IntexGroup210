
namespace IntexGroup210.Models
{
	public class EFLegoRepository : ILegoRepository
	{
		private LegoDbContext _context;

		public EFLegoRepository(LegoDbContext temp)
		{
			_context = temp;
		}

		public IQueryable<Customer> Customers => _context.Customers;

		public IQueryable<LineItem> LineItems => _context.LineItems;

		public IQueryable<Order> Orders => _context.Orders;

		public IQueryable<Product> Products => _context.Products;

		public IQueryable<User> Users => _context.Users;
	}
}
