using Base.Models;
using Base.Services;
using BaseApi.Controllers;
using BaoCust.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseWeb.Attributes;

namespace BaoCust.Controllers
{
    [XgLogin]
    public class BaoController : ApiCtrl
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
            return JsonToCnt(await EditService().GetUpdJsonAsync(key));
        }

        [HttpPost]
        public async Task<ContentResult> GetViewJson(string key)
        {
            return JsonToCnt(await EditService().GetViewJsonAsync(key));
        }

        [HttpPost]
        //TODO: add your code, tSn_fid ex: t03_FileName
        public async Task<JsonResult> Create(string json, List<IFormFile> t00_FileName)
        {
            return Json(await EditService().CreateAsnyc(_Str.ToJson(json), t00_FileName));
        }

        [HttpPost]
        //TODO: add your code, tSn_fid ex: t03_FileName
        public async Task<JsonResult> Update(string key, string json, List<IFormFile> t00_FileName)
        {
            return Json(await EditService().UpdateAsnyc(key, _Str.ToJson(json), t00_FileName));
        }

        //TODO: add your code
        //get file/image
        public async Task<FileResult> ViewFile(string table, string fid, string key, string ext)
        {
            return await _Xp.ViewStageAsync(fid, key, ext);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string key)
        {
            return Json(await EditService().DeleteAsync(key));
        }

    }//class
}