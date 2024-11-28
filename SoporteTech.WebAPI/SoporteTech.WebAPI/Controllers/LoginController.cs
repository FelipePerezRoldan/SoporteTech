﻿using SoporteTech.WebAPI.Clases;
using SoporteTech.WebAPI.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SoporteTech.WebAPI.Controllers
{
    [EnableCors(origins: "https://localhost:44387", headers: "*", methods: "*")]
    [RoutePrefix("api/Login")]
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("Ingresar")]
        public IQueryable<LoginRespuesta> Ingresar([FromBody] Login login)
        {
            clsLogin _login = new clsLogin();
            _login.login = login;
            return _login.Ingresar();
        }
    }
}