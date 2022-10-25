using Base.Models;
using Base.Services;
using BaseApi.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaoCust.Services
{
    public class BaoEdit : XgEdit
    {
        public BaoEdit(string ctrl) : base(ctrl) { }

        override public EditDto GetDto()
        {
            return new EditDto
            {
                //1.設定Bao資料表
                Table = "dbo.[Bao]",
                PkeyFid = "Id",
                Col4 = new string[] { "Creator", "Revised", null, "Revised" },
                //2.設定Bao欄位
                Items = new EitemDto[] 
				{
					new() { Fid = "Id" },
					new() { Fid = "Name", Required = true },
					new() { Fid = "StartTime", Required = true },
					new() { Fid = "EndTime", Required = true },
                    new() { Fid = "IsBatch" },
					new() { Fid = "IsMove" },
					new() { Fid = "IsMoney" },
					new() { Fid = "GiftName", Required = true },
					new() { Fid = "Note" },
					new() { Fid = "StageCount", Value = 0 },
                    new() { Fid = "Status" },
                    new() { Fid = "Creator" },
                    new() { Fid = "Revised" },
                },
                //3.設定BaoStage
                Childs = new EditDto[]
                {
                    new()
                    {
                        Table = "dbo.BaoStage",
                        PkeyFid = "Id",
                        FkeyFid = "BaoId",
						OrderBy = "Sort",
                        Col4 = null,
                        Items = new EitemDto[] 
						{
							new() { Fid = "Id" },
							new() { Fid = "BaoId" },
							new() { Fid = "FileName", Required = true },
                            new() { Fid = "AppHint" },
                            new() { Fid = "CustHint" },
                            new() { Fid = "Answer", Required = true },
							new() { Fid = "Sort", Required = true },
                        },
                    },
                },
            };
        }

        //4.儲存新增記錄
        //TODO: add your code
        //t03_FileName: t + table serial _ + fid
        public async Task<ResultDto> CreateAsnyc(JObject json, List<IFormFile> t00_FileName)
        {
            var service = EditService();
            Md5Answer(json);
            var result = await service.CreateA(json);
            if (_Valid.ResultStatus(result))
                await _WebFile.SaveCrudFilesA(json, service.GetNewKeyJson(), _Xp.DirStageImage(), t00_FileName, nameof(t00_FileName));
            return result;
        }

        //TODO: add your code
        //t03_FileName: t + table serial _ + fid
        public async Task<ResultDto> UpdateAsnyc(string key, JObject json, List<IFormFile> t00_FileName)
        {
            var service = EditService();
            Md5Answer(json);
            var result = await service.UpdateA(key, json);
            if (_Valid.ResultStatus(result))
                await _WebFile.SaveCrudFilesA(json, service.GetNewKeyJson(), _Xp.DirStageImage(), t00_FileName, nameof(t00_FileName));
            return result;
        }

        /// <summary>
        /// md5 encode Answer field
        /// </summary>
        /// <param name="json"></param>
        private void Md5Answer(JObject json)
        {
            var stages = _Json.GetChildRows(json, 0);
            if (stages != null)
            {
                var fid = "Answer";
                foreach (var stage in stages)
                {
                    if (!_Object.IsEmpty(stage[fid]))
                        stage[fid] = _Str.Md5(stage[fid].ToString());
                }
            }
        }

    } //class
}
