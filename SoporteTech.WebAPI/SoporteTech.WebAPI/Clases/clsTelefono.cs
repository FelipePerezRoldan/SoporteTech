using Servicios_6_8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_6_8.Clases
{
    public class clsTelefono
    {
        DBSuperEntities dBSuper = new DBSuperEntities();
        public TELEfono telefono { get; set; }
        public IQueryable TelefonosXCliente(string Documento)
        {
            return from C in dBSuper.Set<CLIEnte>()
                   join T in dBSuper.Set<TELEfono>()
                   on C.Documento equals T.Documento
                   join TT in dBSuper.Set<TIpoTElefono>()
                   on T.CodigoTipoTelefono equals TT.Codigo
                   where C.Documento == Documento
                   orderby TT.Nombre
                   select new
                   {
                       Edit = "<img src=\"../Imagenes/Editar.png\" onclick=\"EditarTelefono(" + T.Codigo + ", " + TT.Codigo + ", " + T.Numero + ") \"style=\"cursor:grab\"/>",
                       Tipo_Telefono = TT.Nombre,
                       Numero = T.Numero
                   };
        }
        public string Insertar()
        {
            dBSuper.TELEfonoes.Add(telefono);
            dBSuper.SaveChanges();
            return "Se insertó el teléfono: " + telefono.Numero;
        }
        public string Eliminar()
        {
            TELEfono _telefono = dBSuper.TELEfonoes.FirstOrDefault(t => t.Codigo == telefono.Codigo);
            dBSuper.TELEfonoes.Remove(_telefono);
            dBSuper.SaveChanges();
            return "Se eliminó el teléfono con código: " + telefono.Codigo;
        }
        public string Actualizar()
        {
            TELEfono _telefono = dBSuper.TELEfonoes.FirstOrDefault(t => t.Codigo == telefono.Codigo);
            _telefono.CodigoTipoTelefono = telefono.CodigoTipoTelefono;
            _telefono.Numero = telefono.Numero;

            dBSuper.SaveChanges();
            return "Se actualizó el teléfono con código: " + telefono.Codigo;
        }
    }
}