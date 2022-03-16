
using Dapper;
using Online.Models;
using Online.Utilities;

namespace Online.Repositories;

public interface ICustomerRepository
{
Task<List<Customer>> GetList();
Task<Customer> GetById(long Id);
Task<Customer> Create(Customer Data);
Task<bool> Update (Customer Data);
}

public class CustomerRepository : BaseRepository, ICustomerRepository
{
    public CustomerRepository(IConfiguration configuration) : base(configuration)
    {

    }

    public async Task<Customer> Create(Customer Data)
    {
        var query = $@"INSERT INTO {TableNames.customer}(name, password, gender, date_of_birth,email, mobile)
        VALUES(@Name, @Password, @Gender, @DateOfBirth, @Email, @Mobile) RETURNING *";

        using (var con = NewConnection)
        return await con.QuerySingleOrDefaultAsync<Customer>(query, Data);

    }

    public async Task<Customer> GetById(long Id)
    {
      var query =$@"SELECT * FROM {TableNames.customer} WHERE id = @Id";
       using (var con = NewConnection)
        return (await con.QuerySingleOrDefaultAsync<Customer>(query, new{ Id}));
    }

    public async Task<List<Customer>> GetList()
    {
        var query =$@"SELECT * FROM {TableNames.customer} ORDER BY id";
        using (var con = NewConnection)
        return(await con.QueryAsync<Customer>(query)).AsList();
    }

    public async Task<bool> Update(Customer Data)
    {
        var query = $@"UPDATE ""{TableNames.customer}"" SET email =@Email WHERE id=@Id;";
        using (var con = NewConnection)
        return (await con.ExecuteAsync(query,Data))== 1;

    }
}