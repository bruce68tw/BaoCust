using BaoCust.Models;
using Base.Services;
using BaseApi.Services;
using BaseWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BaoCust.Services
{
    //project service
    public static class _Xp
    {
        //public const string SiteVer = "20201228f";     //for my.js/css
        public static string MyVer = _Date.NowSecStr(); //for my.js/css
        public const string LibVer = "20211105a";       //for lib.js/css

        public static string NoImagePath = _Fun.DirRoot + "wwwroot/image/noImage.jpg";

        //dir
        //public static string DirUpload = _Fun.DirRoot + "_upload/";
        //public static string DirStage = DirUpload + "Stage/";

        //from config file
        public static XpConfigDto Config;

        public static string DirStageImage()
        {
            return Config.DirStageImage;
        }

        #region get file content
        public static async Task<FileResult> ViewStageAsync(string fid, string key, string ext)
        {
            return await ViewFileAsync(DirStageImage(), fid, key, ext);
        }
        
        private static async Task<FileResult> ViewFileAsync(string dir, string fid, string key, string ext)
        {
            var path = $"{dir}{fid}_{key}.{ext}";
            return await _WebFile.ViewFileAsync(path, $"{fid}.{ext}");
        }

        #endregion

        /// <summary>
        /// get locale code without dash sign
        /// </summary>
        /// <returns></returns>
        public static string GetLocale0()
        {
            return _Locale.GetLocaleByUser(false);
        }

    }//class
}