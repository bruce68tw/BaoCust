using Base.Models;
using Base.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaoCust.Services
{
    //for dropdown input
    public static class _XpCode
    {
        #region master table to codes
        public static async Task<List<IdStrDto>> GetBaos(Db db = null)
        {
            //var userId = _Fun.GetBaseUser().UserId;
            var sql = string.Format($@"
select 
    Id, Name as Str
from dbo.Bao
where Creator='{_Fun.UserId()}'
order by Id");
            return await _Db.SqlToCodesA(sql, db);

        }
        #endregion

        #region get from XpCode table
        public static async Task<List<IdStrDto>> GetAuthRangesA(string locale, Db db = null)
        {
            return await TypeToListA(locale, "AuthRange", db);
        }
        #endregion

        #region others
        /*
        public static List<IdStrDto> GetGiftTypes()
        {
            return new List<IdStrDto>(){
                new(){ Id="G", Str="獎品"},
                new(){ Id="M", Str="獎金"},
            };
        }
        */
        #endregion

        private static async Task<List<IdStrDto>> TableToListA(string table, Db db = null)
        {
            //var userId = _Fun.GetBaseUser().UserId;
            var sql = string.Format($@"
select 
    Id, Name as Str
from dbo.[{table}]
where Creator='{_Fun.UserId()}'
order by Id");
            return await _Db.SqlToCodesA(sql, db);
        }

        /*
        //get codes from sql 
        private static async Task<List<IdStrDto>> SqlToListAsync(string sql, Db db = null)
        {
            var emptyDb = false;
            _Fun.CheckOpenDb(ref db, ref emptyDb);

            var rows = await db.GetModelsA<IdStrDto>(sql);
            await _Fun.CheckCloseDbA(db, emptyDb);
            return rows;
        }
        */

        //get code table rows
        private static async Task<List<IdStrDto>> TypeToListA(string locale, string type, Db db = null)
        {
            var sql = $@"
select 
    Value as Id, Name_{locale} as Str
from dbo.XpCode
where Type='{type}'
order by Sort";
            return await _Db.SqlToCodesA(sql, db);           
        }
        /*
        public static string GetValue(XpCode row, string locale)
        {
            var name = "Name_" + locale;
            //return _Linq.FnGetValue<XpCode>(name).ToString();
            return _Model.GetValue<XpCode>(row, name).ToString();
        }
        */

        /*
        public static List<IdStrExtModel> GetCodeExts(string type, Db db = null)
        {
            var emptyDb = (db == null);
            if (emptyDb)
                db = new Db();

            var sql = string.Format(@"
select 
    Value as Id, Name as Str, Ext
from dbo.XpCode
where Type='{0}'
and Ext='0'
order by Sort
", type);
            var rows = db.GetModels<IdStrExtModel>(sql);
            if (emptyDb)
                db.Dispose();
            return rows;
        }
        */

    }//class
}
