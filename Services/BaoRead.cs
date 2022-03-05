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
select * from dbo.Bao
where Creator='{_Fun.UserId()}'
order by Id
",
            Items = new [] {
                new QitemDto { Fid = "Name", Op = ItemOpEstr.Like },
            },
        };

        public async Task<JObject> GetPage(string ctrl, DtDto dt)
        {
            return await new CrudRead().GetPageAsync(dto, dt, ctrl);
        }

    } //class
}