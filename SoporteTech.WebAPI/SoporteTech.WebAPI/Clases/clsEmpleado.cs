using Servicios_6_8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_6_8.Clases
{
    public class clsEmpleado
    {
        private DBSuperEntities dbSuper = new DBSuperEntities();
        public EMPLeado empleado { get; set; }
        public IQueryable Consultar(string Documento)
        {
            return from E in dbSuper.Set<EMPLeado>()
                   join EC in dbSuper.Set<EMpleadoCArgo>()
                   on E.Documento equals EC.Documento
                   join C in dbSuper.Set<CARGo>()
                   on EC.CodigoCargo equals C.Codigo
                   where E.Documento == Documento
                   select new
                   {
                       Empleado = E.Nombre + " " + E.PrimerApellido + " " + E.SegundoApellido,
                       Cargo = C.Nombre
                   };
        }
        public IQueryable ConsultarXUsuario(string Usuario)
        {
            return from E in dbSuper.Set<EMPLeado>()
                   join EC in dbSuper.Set<EMpleadoCArgo>()
                   on E.Documento equals EC.Documento
                   join C in dbSuper.Set<CARGo>()
                   on EC.CodigoCargo equals C.Codigo
                   join U in dbSuper.Set<Usuario>()
                   on E.Documento equals U.Documento_Empleado
                   where U.userName == Usuario
                   select new
                   {
                       idEmpleadoCargo = EC.Codigo,
                       Empleado = E.Nombre + " " + E.PrimerApellido + " " + E.SegundoApellido,
                       Cargo = C.Nombre
                   };
        }
    }
}