using BaoCust.Services;
using Base.Models;
using BaseApi.Controllers;
using BaseApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaoCust.Controllers
{
    //每日報名統計
    [XgLogin]
    public class ChartDailyController : BaseCtrl
    {
        public async Task<ActionResult> Index()
        {
			ViewBag.Baos = await _XpCode.GetBaos();
            return View();
        }

        //id: Bao.Id
        [HttpPost]
        public async Task<List<IdNumDto>> GetData(string id)
        {
            return await new ChartDailyService().GetDataA(id);
        }

    }//class
}