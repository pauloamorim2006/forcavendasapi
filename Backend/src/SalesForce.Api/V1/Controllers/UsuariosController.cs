using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ERP.Api.Controllers;
using ERP.Api.Models;
using ERP.Business.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesForce.Api.Services;

namespace ERP.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/usuarios")]
    public class UsuariosController : MainController
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UsuariosController(UserManager<IdentityUser> userManager,
            IMapper mapper,
            IUriService uriService,
            INotificador notificador,
            IUser user) : base(mapper, uriService, notificador, user)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IEnumerable<IdentityUser> ObterTodos()
        {
            return _userManager.Users.ToList();
        }
    }
}