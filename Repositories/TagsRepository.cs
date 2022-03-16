
using Dapper;
using Online.Models;
using Online.Utilities;

namespace Online.Repositories;

public interface ITagsRepository
{
    Task<List<Tags>> GetList();
    Task<Tags> GetById(long Id);
    Task<Tags> Create(Tags Data);
    Task<List<Tags>> GetTagsForProduct(long ProductId);
}

public class TagsRepository : BaseRepository, ITagsRepository
{
    public TagsRepository(IConfiguration configuration) : base(configuration)
    {

    }

    public async Task<Tags> Create(Tags Data)
    {
        var query = $@"INSERT INTO {TableNames.tags}( brand, color, model, product_id)
       VALUES (@Brand, @Color, @Model, @ProductId) RETURNING *";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Tags>(query, Data);
    }

    public async Task<Tags> GetById(long Id)
    {
        var query = $@"SELECT * FROM {TableNames.tags} WHERE id = @Id";
        using (var con = NewConnection)
            return (await con.QuerySingleOrDefaultAsync<Tags>(query, new { Id }));
    }

    public async Task<List<Tags>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.tags} ORDER BY id";
        using (var con = NewConnection)
            return (await con.QueryAsync<Tags>(query)).AsList();
    }

    public async Task<List<Tags>> GetTagsForProduct(long ProductId)
    {
        var query = $@"SELECT * FROM {TableNames.tags} WHERE product_id = @ProductId";
        using (var con = NewConnection)
            return (await con.QueryAsync<Tags>(query, new { ProductId })).AsList();
    }

}
