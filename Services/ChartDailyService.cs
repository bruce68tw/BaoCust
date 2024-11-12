using Base.Models;
using Base.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaoCust.Services
{
    public class ChartDailyService
    {
        public async Task<List<IdNumDto>> GetDataAsync(string baoId)
        {
            var sql = @"
--declare @StartDate date, @EndDate date, @BaoId varchar
--select @StartDate = '2021-11-16'
--select @EndDate = '2021-12-15'
--select @BaoId = 'B001'

-- 1.get range dates
;with result(rowDate) as (
	select @StartDate
    union all
    select dateAdd(day, 1, rowDate)
    from result 
    where rowDate < @EndDate)

-- 2.get data
select 
	Id=convert(char(5), a.rowDate, 1),
	Num=(
		select count(*)
		from dbo.BaoAttend  
        where BaoId=@BaoId
		and convert(date, Created)=a.rowDate
	)
from result a
";
            //3.查詢資料庫
            var today = DateTime.Today;
            var args = new List<object>() {
                "BaoId", baoId,
                "StartDate", today.AddMonths(-1).AddDays(1),
                "EndDate", today,
            };
            return (await _Db.GetModelsA<IdNumDto>(sql, args))!;
        }
    }
}
