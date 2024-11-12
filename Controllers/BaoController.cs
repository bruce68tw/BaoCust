using Base.Models;
using Base.Services;
using BaseApi.Controllers;
using BaoCust.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseApi.Attributes;

namespace BaoCust.Controllers
{
    [XgLogin]
    public class BaoController : BaseCtrl
    {
        public ActionResult Read()
        {
			//for edit view
			//ViewBag.GiftTypes = _XpCode.GetGiftTypes();
            return View();
        }

        [HttpPost]
        public async Task<ContentResult> GetPage(DtDto dt)
        {
            return JsonToCnt(await new BaoRead().GetPage(Ctrl, dt));
        }

        private BaoEdit EditService()
        {
            return new BaoEdit(Ctrl);
        }

        [HttpPost]
        public async Task<ContentResult> GetUpdJson(string key)
        {
            return JsonToCnt(await EditService().GetUpdJsonA(key));
        }

        [HttpPost]
        public async Task<ContentResult> GetViewJson(string key)
        {
            return JsonToCnt(await EditService().GetViewJsonA(key));
        }

        [HttpPost]
        //TODO: add your code, tSn_fid ex: t03_FileName
        public async Task<JsonResult> Create(string json, List<IFormFile> t00_FileName)
        {
            return Json(await EditService().CreateAsnyc(_Str.ToJson(json)!, t00_FileName));
        }

        [HttpPost]
        //TODO: add your code, tSn_fid ex: t03_FileName
        public async Task<JsonResult> Update(string key, string json, List<IFormFile> t00_FileName)
        {
            return Json(await EditService().UpdateAsnyc(key, _Str.ToJson(json)!, t00_FileName));
        }

        //TODO: add your code
        //get file/image
        public async Task<FileResult?> ViewFile(string table, string fid, string key, string ext)
        {
            return await _Xp.ViewStageAsync(fid, key, ext);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string key)
        {
            return Json(await EditService().DeleteA(key));
        }

        //檢查答案
        [HttpPost]
        public async Task<string> CheckAnswer(string id, string input)
        {
            var sql = @"select Answer from dbo.BaoStage where Id=@Id";
            var data = await _Db.GetStrA(sql, new() { "Id", id });
            return (data == _Str.Md5(input)) ? "1" : "0";
        }

    }//class
}