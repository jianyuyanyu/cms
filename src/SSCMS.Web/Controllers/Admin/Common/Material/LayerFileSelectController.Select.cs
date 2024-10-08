﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Dto;
using SSCMS.Enums;
using SSCMS.Utils;

namespace SSCMS.Web.Controllers.Admin.Common.Material
{
    public partial class LayerFileSelectController
    {
        [HttpPost, Route(RouteSelect)]
        public async Task<ActionResult<StringResult>> Select([FromBody] SelectRequest request)
        {
            var site = await _siteRepository.GetAsync(request.SiteId);
            var file = await _materialFileRepository.GetAsync(request.MaterialId);

            var materialFilePath = PathUtils.Combine(_settingsManager.WebRootPath, file.Url);
            if (!FileUtils.IsFileExists(materialFilePath))
            {
                return this.Error("文件不存在，请重新选择");
            }

            var localDirectoryPath = await _pathManager.GetUploadDirectoryPathAsync(site, UploadType.File);

            string fileName;
            if (site.IsFileUploadChangeFileName)
            {
                fileName = _pathManager.GetUploadFileName(site, materialFilePath);
            }
            else
            {
                fileName = $"{file.Title}.{StringUtils.ToLower(file.FileType)}";
            }
            
            var filePath = PathUtils.Combine(localDirectoryPath, fileName);

            DirectoryUtils.CreateDirectoryIfNotExists(filePath);
            FileUtils.CopyFile(materialFilePath, filePath);

            var fileUrl = await _pathManager.GetVirtualUrlByPhysicalPathAsync(site, filePath);

            return new StringResult
            {
                Value = fileUrl
            };
        }
    }
}
