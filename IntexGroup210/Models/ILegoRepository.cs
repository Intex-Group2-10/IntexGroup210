namespace IntexGroup210.Models
{
	public interface ILegoRepository
	{
		public IQueryable<Customer> Customers { get; }
		public IQueryable<LineItem> LineItems { get; }
		public IQueryable<Order> Orders { get; }
		public IQueryable<Product> Products { get; }
		public IQueryable<User> Users { get; }
	}
}
