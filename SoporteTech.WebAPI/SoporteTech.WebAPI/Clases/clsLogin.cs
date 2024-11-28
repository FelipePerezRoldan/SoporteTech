﻿using Servicios_PD.Clases;
using SoporteTech.WebAPI.Models;
using System;
using System.Linq;

namespace SoporteTech.WebAPI.Clases
{
    public class clsLogin
    {
        public SoporteTechDBEntities dbSoporteTech = new SoporteTechDBEntities();
        public Login login { get; set; }
        public LoginRespuesta loginRespuesta { get; set; }
        public clsLogin()
        {
            loginRespuesta = new LoginRespuesta();
        }
        public IQueryable<LoginRespuesta> Ingresar()
        {
            if (ValidarUsuario())
            {
                string token = TokenGenerator.GenerateTokenJwt(login.Correo);
                return from U in dbSoporteTech.Set<Usuario>()
                       join R in dbSoporteTech.Set<Role>()
                       on U.RolID equals R.RolID
                       where U.Correo == login.Correo &&
                             U.ContraseñaHash == login.Clave
                       select new LoginRespuesta
                       {
                           Usuario = U.Nombre,
                           Correo = U.Correo,
                           Autenticado = true,
                           Rol = R.Nombre,
                           //PaginaInicio = P.PaginaNavegar,
                           Token = token,
                           Mensaje = ""
                       };
            }
            else
            {
                //return (IQueryable<LoginRespuesta>)loginRespuesta;
                return null;
            }
        }
        public bool ValidarUsuario()
        {
            try
            {
                clsCypher cifrar = new clsCypher();
                Usuario usuario = dbSoporteTech.Usuarios.FirstOrDefault(u => u.Correo == login.Correo);
                if (usuario == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Usuario no existe";
                    return false;
                }
                byte[] arrBytesSalt = Convert.FromBase64String(usuario.Salt);
                string ClaveCifrada = cifrar.HashPassword(login.Clave, arrBytesSalt);
                login.Clave = ClaveCifrada;
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }

    }
}