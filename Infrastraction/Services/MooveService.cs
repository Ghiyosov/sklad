using Dapper;
using Domein.Models;
namespace Infrastraction.Services;

public class MooveService
{
    private readonly DapperContext _context;
    public void AddMoove(Moove moove)
    {
        var sql = "insert into moove(productid,tosklad,times)value(@productid,@tosklad,@times)";
        int sk = _context.Connection().QueryFirstOrDefault<int>("select skladid from product as p where p.id = @productid");
        _context.Connection().Execute(sql, moove);
        int sid = _context.Connection().QueryFirstOrDefault<int>("select max(id) from moove");
        _context.Connection().Execute(@"update from moove as m
                                        set atsklad=@sk
                                        where m.id=@sid", new { Sk = sk, Sid = sid });
    }
    
    public List<Story> MooveInfo()
    {
        var stor = _context.Connection().Query<Story>(@"select p.name as product, s.name as atsklad, sa.name as tosklad  
                                                        from moove as m
                                                        join product as p on m.productid=p.id
                                                        join sklad as s on m.atsklad=s.id
                                                        join sklad as sa on m.tosklad=sa.id").ToList();
        return stor;
    }
}
