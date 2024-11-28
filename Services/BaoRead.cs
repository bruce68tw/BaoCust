using BaoLib.Services;
using Base.Enums;
using Base.Models;
using Base.Services;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace BaoCust.Services
{
    public class BaoRead
    {
        private readonly ReadDto dto = new()
        {
            ReadSql = $@"
select b.*, 
    ReplyTypeName=x.Name,
    PrizeTypeName=x2.Name
from dbo.Bao b
join dbo.XpCode x on x.Type='{_XpLib.ReplyType}' and b.ReplyType=x.Value
join dbo.XpCode x2 on x2.Type='{_XpLib.PrizeType}' and b.PrizeType=x2.Value
where Creator='{_Fun.UserId()}'
order by Id
",
            Items = new [] {
                new QitemDto { Fid = "Name", Op = ItemOpEstr.Like },
            },
        };

        public async Task<JObject?> GetPage(string ctrl, DtDto dt)
        {
            return await new CrudReadSvc().GetPageA(dto, dt, ctrl);
        }

    } //class
}