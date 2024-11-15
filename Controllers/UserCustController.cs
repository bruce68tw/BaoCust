﻿using BaoCust.Services;
using Base.Services;
using BaseApi.Controllers;
using BaseApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BaoCust.Controllers
{
    [XgLogin]
    public class UserCustController : BaseCtrl
    {
        public ActionResult Edit()
        {
            return View();
        }

        private UserCustEdit EditService()
        {
            return new UserCustEdit(Ctrl);
        }

        [HttpPost]
        public async Task<ContentResult> GetUpdJson(string key)
        {
            return JsonToCnt(await EditService().GetUpdJsonA(_Fun.UserId()));
        }

        [HttpPost]
        public async Task<JsonResult> Update(string key, string json)
        {
            return Json(await EditService().UpdateA(_Fun.UserId(), _Str.ToJson(json)!));
        }

    }//class
}