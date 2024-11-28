using Servicios_6_8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Servicios_6_8.Clases
{
    public class clsTipoProducto
    {
        private DBSuperEntities dbSuper = new DBSuperEntities();
        public TIpoPRoducto TipoProducto { get; set; }
        public List<TIpoPRoducto> ListarTodos()
        {
            return dbSuper.TIpoPRoductoes
                .Where(t => t.Activo == true)
                .OrderBy(t => t.Nombre)
                .ToList();
        }
    }
}