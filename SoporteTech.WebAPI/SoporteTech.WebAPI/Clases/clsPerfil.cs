using Servicios_6_8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_6_8.Clases
{
    public class clsPerfil
    {
        private DBSuperEntities dbSuper = new DBSuperEntities();
        public Perfil perfil { get; set; }
        public IQueryable LlenarCombo()
        {
            return from P in dbSuper.Set<Perfil>()
                   orderby P.Nombre
                   select new
                   {
                       Codigo = P.id,
                       Nombre = P.Nombre
                   };
        }
    }
}