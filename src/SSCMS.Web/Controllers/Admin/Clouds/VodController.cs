﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using SSCMS.Configuration;
using SSCMS.Repositories;
using SSCMS.Services;

namespace SSCMS.Web.Controllers.Admin.Clouds
{
    [OpenApiIgnore]
    [Authorize(Roles = Types.Roles.Administrator)]
    [Route(Constants.ApiAdminPrefix)]
    public partial class VodController : ControllerBase
    {
        private const string Route = "clouds/vod";

        private readonly IAuthManager _authManager;
        private readonly IConfigRepository _configRepository;

        public VodController(
            IAuthManager authManager, 
            IConfigRepository configRepository
        )
        {
            _authManager = authManager;
            _configRepository = configRepository;
        }

        public class GetResult
        {
            public bool IsCloudVod { get; set; }
        }

        public class SubmitRequest
        {
            public bool IsCloudVod { get; set; }
        }
    }
}
