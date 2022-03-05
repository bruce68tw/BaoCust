using Base.Models;
using Base.Services;
using BaseApi.Controllers;
using BaseWeb.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaoCust.Controllers
{
    [XgLogin]
    public class ChartAttendController : ApiCtrl
    {
        public ActionResult Index()
        {
            return View();
        }

        //id: Bao.Id
        [HttpPost]
        public async Task<List<IdNumDto>> GetData()
        {
            var sql = $@"
select 
	Id=b.Name,
	Num=(
		select count(*)
		from dbo.BaoAttend  
        where BaoId=b.Id
	)
from dbo.Bao b
where b.Creator='{_Fun.UserId()}'
";
            return await _Db.GetModelsAsync<IdNumDto>(sql);
        }

    }//class
}