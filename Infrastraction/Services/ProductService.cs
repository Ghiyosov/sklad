using Dapper;
using Domein.Models;

namespace Infrastraction.Services;

public class ProductService
{
    private readonly DapperContext _context;
    public void AddProdact(Product product)
    {
        var sql = "inser into product(name,skladid)values(@name,@skladid)";
        _context.Connection().Execute(sql, product);
    }
}
