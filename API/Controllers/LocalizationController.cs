using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class LocalizationController:BaseApiController
    {
        private readonly IStringLocalizer<LocalizationController> _localizer;

        public LocalizationController(IStringLocalizer<LocalizationController> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet]
        public string Get()
        {
            var ragaca = _localizer["WelcomeMsg"].Value;
            return ragaca;
        }
    }
}