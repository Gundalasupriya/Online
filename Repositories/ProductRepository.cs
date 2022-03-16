
using Dapper;
using Online.Models;
using Online.Utilities;

namespace Online.Repositories;

public interface IProductRepository
{
Task<List<Product>> GetList();
Task<Product> GetById(long Id);
Task<Product> Create(Product Data);
Task<bool> Update (Product Data);

Task <List<Product>> GetProductForOrder(long OrderId);
}

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(IConfiguration configuration) : base(configuration)
    {

    }

    public async Task<Product> Create(Product Data)
    {
      var query = $@"INSERT INTO {TableNames.product}(name, price) VALUES(@Name, @Price) RETURNING *";
      using (var con = NewConnection)
      return await con.QuerySingleOrDefaultAsync<Product>(query, Data);
    }

    public async Task<Product> GetById(long Id)
    {
       var query = $@"SELECT * FROM {TableNames.product} WHERE id = @Id";
       using (var con = NewConnection)
        return (await con.QuerySingleOrDefaultAsync<Product>(query, new { Id}));

    }

    public async Task<List<Product>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.product} ORDER BY id";
        using (var con = NewConnection)
        return (await con.QueryAsync<Product>(query)).AsList();
    }

    public async Task<List<Product>> GetProductForOrder(long OrderId)
    {
        var query = $@"SELECT * FROM {TableNames.order_product} op
        LEFT JOIN {TableNames.product} p ON p.id = op.product_id
        WHERE order_id = @OrderId";
        using (var con = NewConnection)
        return (await con.QueryAsync<Product>(query, new { OrderId })).AsList();

    }

    public async Task<bool> Update(Product Data)
    {
         var query = $@"UPDATE ""{TableNames.product}"" SET price =@Price WHERE id=@Id;";
        using (var con = NewConnection)
        return (await con.ExecuteAsync(query,Data))== 1;
    }
}