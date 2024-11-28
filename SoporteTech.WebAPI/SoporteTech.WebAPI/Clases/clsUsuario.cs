using Servicios_6_8.Models;
using Servicios_PD.Clases;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios_6_8.Clases
{
    public class clsUsuario
    {
        private DBSuperEntities dbSuper = new DBSuperEntities();
        public Usuario usuario { get; set; }
        public Usuario_Perfil usuarioPerfil { get; set; }
        public string Actualizar(int Perfil)
        {
            try
            {
                clsCypher cifrar = new clsCypher();
                cifrar.Password = usuario.Clave;
                if (cifrar.CifrarClave())
                {
                    //Se debe consultar el id del usuario y el id del usuario perfil
                    Usuario _usuario = Consultar(usuario.Documento_Empleado);
                    if (_usuario == null)
                    {
                        return "No existe el usuario";
                    }
                    usuario.id = _usuario.id;
                    usuario.Salt = cifrar.Salt;
                    usuario.Clave = cifrar.PasswordCifrado;
                    dbSuper.Usuarios.AddOrUpdate(usuario);
                    dbSuper.SaveChanges();

                    //Agregar el perfil del usuario
                    usuarioPerfil = ConsultarUsuarioPerfil(usuario.id);
                    usuarioPerfil.idUsuario = usuario.id;
                    usuarioPerfil.idPerfil = Perfil;
                    usuarioPerfil.Activo = true;
                    dbSuper.Usuario_Perfil.AddOrUpdate(usuarioPerfil);
                    dbSuper.SaveChanges();

                    return "Se actualizó el usuario: " + usuario.userName;
                }
                else
                {
                    return "No pudo cifrar la clave.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public Usuario Consultar(string Documento)
        {
            return dbSuper.Usuarios.FirstOrDefault(U => U.Documento_Empleado == Documento);
        }
        public Usuario_Perfil ConsultarUsuarioPerfil(int idUsuario)
        {
            return dbSuper.Usuario_Perfil.FirstOrDefault(UP => UP.idUsuario == idUsuario);
        }
        public string Insertar(int Perfil)
        {
            try
            {
                clsCypher cifrar = new clsCypher();
                cifrar.Password = usuario.Clave;
                if (cifrar.CifrarClave())
                {
                    usuario.Salt = cifrar.Salt;
                    usuario.Clave = cifrar.PasswordCifrado;
                    dbSuper.Usuarios.Add(usuario);
                    dbSuper.SaveChanges();

                    //Agregar el perfil del usuario
                    usuarioPerfil = new Usuario_Perfil();
                    usuarioPerfil.idUsuario = usuario.id;
                    usuarioPerfil.idPerfil = Perfil;
                    usuarioPerfil.Activo = true;
                    dbSuper.Usuario_Perfil.Add(usuarioPerfil);
                    dbSuper.SaveChanges();

                    return "Se insertó el usuario: " + usuario.userName;
                }
                else
                {
                    return "No pudo cifrar la clave.";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public string Activar(int idUsuarioPerfil, bool Activo)
        {
            try
            {
                Usuario_Perfil usuario_Perfil = dbSuper.Usuario_Perfil.FirstOrDefault(u => u.id == idUsuarioPerfil);
                if (usuario_Perfil != null)
                {
                    usuario_Perfil.Activo = Activo;
                    dbSuper.SaveChanges();
                    return "Se actualizó el estado del usario a " + (Activo ? "ACTIVO" : "INACTIVO");
                }
                return "El usuario no existe en la base de datos";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public IQueryable ListarUsuarios()
        {
            return from U in dbSuper.Set<Usuario>()
                   join UP in dbSuper.Set<Usuario_Perfil>()
                   on U.id equals UP.idUsuario
                   join P in dbSuper.Set<Perfil>()
                   on UP.idPerfil equals P.id
                   join E in dbSuper.Set<EMPLeado>()
                   on U.Documento_Empleado equals E.Documento
                   join EC in dbSuper.Set<EMpleadoCArgo>()
                   on E.Documento equals EC.Documento
                   join C in dbSuper.Set<CARGo>()
                   on EC.CodigoCargo equals C.Codigo
                   select new
                   {
                       Editar = "<button type=\"button\" id=\"btnEditar\" class=\"btn-block btn-lg btn-primary\" onclick=\"Editar('" + E.Documento + "', '" +
                                E.Nombre + " " + E.PrimerApellido + " " + E.SegundoApellido + "', '" + C.Nombre + "', '" + U.userName + "', '" + 
                                P.id + "', '" + UP.Activo + "', '" + UP.id + "')\">Edit</button>",
                       Documento = U.Documento_Empleado,
                       Empleado = E.Nombre + " " + E.PrimerApellido + " " + E.SegundoApellido,
                       Cargo = C.Nombre,
                       Usuario = U.userName,
                       Perfil = P.Nombre,
                       Activo = UP.Activo ? "SI" : "NO"
                   };
        }
    }
}