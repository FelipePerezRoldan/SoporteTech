using SoporteTech.WebAPI.Models;
using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
namespace SoporteTech.WebAPI.Clases
{
    public class clsTickets
    {
        private SoporteTechDBEntities dbSoporteTech = new SoporteTechDBEntities();
        public Ticket ticket { get; set; }
        public TicketRespuesta ticketRespuesta { get; set; }
        public clsTickets() 
        {
            ticketRespuesta = new TicketRespuesta();
        }
        //public string Insertar()
        //{
        //    try
        //    {
        //        dbSuper.PRODuctoes.Add(producto);
        //        dbSuper.SaveChanges();
        //        return "Se insertó el producto: " + producto.Nombre;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        //IQueryable es un objeto en formato de lista que me permite devolver información que no está definida como objetos
        //en mi modelo (Construido por el Entity Framework)
        //En el Iqueryable, que tiene formato Linq, funciona FROM - WHERE - SELECT
        public IQueryable<TicketRespuesta> ListarTicketsAbiertos()
        {
            return from T in dbSoporteTech.Set<Ticket>()
                   join E in dbSoporteTech.Set<Equipos>()
                   on T.ClienteID equals E.ClienteID
                   where T.Estado == "Abierto"
                   orderby T.FechaCreacion descending
                   select new  TicketRespuesta
                   {
                       TicketID = T.TicketID,
                       Equipo = E.Nombre,
                       FechaCreacion = (Nullable<System.DateTime>)T.FechaCreacion,
                       Descripcion = T.Descripcion
                   };
        }
        //public IQueryable ListarProductosConTipo()
        //{
        //    //Se inicia con el origen de los datos: 
        //    //from Alias in Conjunto de datos del primer origen
        //    //join Alias in Conjunto de datos del segundo origen
        //    //on Campo del primer conjunto que es igual al campo del segundo conjunto, normalmente la PK = FK
        //    return from TP in dbSuper.Set<TIpoPRoducto>()
        //           join P in dbSuper.Set<PRODucto>()
        //           on TP.Codigo equals P.CodigoTipoProducto
        //           orderby TP.Nombre, P.Nombre
        //           select new //El select se aplica al final del conjunto de datos
        //           {
        //               //Se pone el alias con el que se va a presentar la información = Campo con el dato
        //               Cod_TipoProducto = TP.Codigo,
        //               Tipo_Producto = TP.Nombre,
        //               Codigo = P.Codigo,
        //               Producto = P.Nombre,
        //               Descripcion = P.Descripcion,
        //               Cantidad = P.Cantidad,
        //               Valor_Unitario = P.ValorUnitario
        //           };
        //}
        //public string Actualizar()
        //{
        //    try
        //    {
        //        dbSuper.PRODuctoes.AddOrUpdate(producto);
        //        dbSuper.SaveChanges();
        //        return "Se actualizaron los datos del producto: " + producto.Nombre;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        //public string Eliminar()
        //{
        //    try
        //    {
        //        PRODucto _producto = Consultar(producto.Codigo);
        //        if (_producto == null)
        //        {
        //            return "El código del producto no existe en la base de datos";
        //        }
        //        else
        //        {
        //            dbSuper.PRODuctoes.Remove(_producto);
        //            dbSuper.SaveChanges();
        //            return "Se eliminó el producto: " + producto.Nombre;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        //public PRODucto Consultar(int Codigo)
        //{
        //    return dbSuper.PRODuctoes.FirstOrDefault(p => p.Codigo == Codigo);
        //}
        //public void ModificarInventarioProducto(int CodigoProducto, int Cantidad, string Operacion)
        //{
        //    PRODucto Producto = dbSuper.PRODuctoes.FirstOrDefault(p => p.Codigo == CodigoProducto);
        //    if (Operacion == "Suma")
        //    {
        //        Producto.Cantidad += Cantidad;
        //    }
        //    else
        //    {
        //        Producto.Cantidad -= Cantidad;
        //    }
        //    dbSuper.SaveChanges();
        //}
    }
}