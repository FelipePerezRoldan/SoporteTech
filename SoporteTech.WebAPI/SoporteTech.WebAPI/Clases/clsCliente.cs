using Servicios_6_8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios_6_8.Clases
{
    public class clsCliente
    {
        private DBSuperEntities dbSuper = new DBSuperEntities();
        public CLIEnte cliente { get; set; }
        public string Insertar()
        {
            try
            {
                //Para grabar en la base de datos con EF, solo se invoca el método .add de la clase que se quiere gestionar
                dbSuper.CLIEntes.Add(cliente);
                //Se debe grabar en la información con el método .savechanges();
                dbSuper.SaveChanges();
                //Retorna la respuesta
                return "Se grabó el cliente: " + cliente.Nombre + " " + cliente.PrimerApellido;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Actualizar()
        {
            try
            {
                dbSuper.CLIEntes.AddOrUpdate(cliente);
                dbSuper.SaveChanges();
                return "Se actualizaron los datos del cliente con documento: " + cliente.Documento;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Eliminar()
        {
            try
            {
                //Hay que consultar el cliente, como el método devuelve un objeto de tipo cliente, debo crear una instancia de la clase CLIEnte
                CLIEnte _cliente = Consultar(cliente.Documento);
                //Se valida si el cliente existe para eliminarlo
                if (_cliente == null)
                {
                    //El cliente no existe
                    return "El cliente no se encuentra en la base de datos";
                }
                else
                {
                    dbSuper.CLIEntes.Remove(_cliente);
                    dbSuper.SaveChanges();
                    return "Se eliminó el cliente: " + _cliente.Nombre + " " + _cliente.PrimerApellido;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public CLIEnte Consultar(string Documento)
        {
            //Para consultar se utiliza la funcion FirstOrDefault() con las condiciones de la consulta dentro del paréntesis
            //Para elaborar el filtro, se utilizan las expresioens lambda
            //Las expresiones lambda, son "variables" que se convierten en una instancia del objeto que se está "trabajando"
            //Se escribe la "variable" seguido de la instrución "=>"
            return dbSuper.CLIEntes.FirstOrDefault(c => c.Documento == Documento);
        }
        public IQueryable ClientesConTelefonos()
        {
            return from C in dbSuper.Set<CLIEnte>()
                   join T in dbSuper.Set<TELEfono>()
                   on C.Documento equals T.Documento into TeN
                   from x in TeN.DefaultIfEmpty()
                   orderby C.Nombre, C.PrimerApellido, C.SegundoApellido
                   group TeN by new { C.Documento, C.Nombre, C.PrimerApellido, C.SegundoApellido, C.FechaNacimiento, C.Email, C.Direccion }
                   into g
                   select new
                   {
                       Editar = "<img src=\"../Imagenes/Editar.png\" onclick=\"Editar('" + g.Key.Documento + "', '" + g.Key.Nombre + "', '" + g.Key.PrimerApellido + 
                                 "', '" + g.Key.SegundoApellido + "', '" + g.Key.Direccion + "', '" + g.Key.Email + "', '" + g.Key.FechaNacimiento + "') \"style=\"cursor:grab\"/>",
                       //Editar = "<button type='button' class='btn btn-primary' data-toggle='modal' data-target='#modTelefono'>Edit</button>",
                       NroTelefonos = g.Count(),
                       Documento = g.Key.Documento,
                       Nombre = g.Key.Nombre + " " + g.Key.PrimerApellido + " " + g.Key.SegundoApellido,
                       Direccion = g.Key.Direccion,
                       Email = g.Key.Email,
                       FechaNacimiento = g.Key.FechaNacimiento
                   };
        }
    }
}