using BaoCust.Enums;
using BaoCust.Models;
using Base.Models;
using Base.Services;
using BaseApi.Services;
using BaseWeb.Attributes;
using BaseWeb.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaoCust.Controllers
{
    public class HomeController : Controller
    {
        [XgLogin]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string url = "")
        {
            return View(new LoginVo() { FromUrl = url });
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginVo vo)
        {
            #region 1.check input required
            //reset UI msg
            vo.AccountMsg = "";
            vo.PwdMsg = "";

            if (_Str.IsEmpty(vo.Account))
            {
                vo.AccountMsg = "field is required.";
                goto lab_exit;
            }
            /*
            if (_Str.IsEmpty(vo.Pwd))
            {
                vo.PwdMsg = "field is required.";
                goto lab_exit;
            }
            */
            #endregion

            #region 2.read DB & compare input
            var sql = @"
select Id as UserId, Name as UserName, Pwd
from dbo.UserCust
where Account=@Account
and Status=1
";
            #region compare input
            var status = false;
            var hasPwd = !_Str.IsEmpty(vo.Pwd);
            var row = await _Db.GetJsonAsync(sql, new List<object>() { "Account", vo.Account });
            if (row != null)
            {
                var dbPwd = row["Pwd"].ToString();
                status = (!hasPwd && dbPwd == "") ||
                    (hasPwd && dbPwd == _Str.Md5(vo.Pwd));
            }
            if (!status)
            {
                vo.AccountMsg = "input wrong.";
                goto lab_exit;
            }
            #endregion
            #endregion

            #region 3.set BaseUserDto model
            var userType = hasPwd
                ? UserTypeEstr.Normal
                : UserTypeEstr.NoPwd;
            var userId = row["UserId"].ToString();
            var userInfo = new BaseUserDto()
            {
                UserId = userId,
                UserName = row["UserName"].ToString(),
                //DeptId = "",
                //DeptName = "",
                Locale = _Fun.Config.Locale,
                //ProgAuthStrs = "",
                //IsLogin = true,
                UserType = userType,
            };
            #endregion

            //4.save BaseUser into session
            _Http.GetSession().Set(_Fun.BaseUser, userInfo);   //extension method

            //5.redirect
            var url = _Str.IsEmpty(vo.FromUrl) ? "/Home/Index" : vo.FromUrl;
            return Redirect(url);

        lab_exit:
            return View(vo);
        }

        public ActionResult Logout()
        {
            _Http.GetSession().Clear();
            return Redirect("/Home/Index");
        }

        public ActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return View("Error", (error == null) ? _Fun.SystemError : error.Error.Message);
        }

    }
}