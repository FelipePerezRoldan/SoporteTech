using Servicios_6_8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_6_8.Clases
{
    public class clsFactura
    {
        private DBSuperEntities dbSuper = new DBSuperEntities();
        public FACTura factura { get; set; }
        public DEtalleFActura detalleFactura { get; set; }
        public string GrabarFactura()
        {
            if (factura.Numero == 0)
            {
                //Se graba el encabezado de la factura
                return GrabarEncabezado();
            }
            return GrabarDetalle();
        }
        private string GrabarEncabezado()
        {
            factura.Numero = ObtenerNumeroFactura();
            factura.Fecha = DateTime.Now;
            dbSuper.FACTuras.Add(factura);
            dbSuper.SaveChanges();
            ModificarInventarioProducto(factura.DEtalleFActuras.First().CodigoProducto, factura.DEtalleFActuras.First().Cantidad, "Resta");
            return factura.Numero.ToString();
        }
        private void ModificarInventarioProducto(int CodigoProducto, int Cantidad, string Operacion)
        {
            clsProducto Producto = new clsProducto();
            Producto.ModificarInventarioProducto(CodigoProducto, Cantidad, Operacion);
        }
        private string GrabarDetalle()
        {
            detalleFactura = factura.DEtalleFActuras.FirstOrDefault();
            detalleFactura.Numero = factura.Numero;
            dbSuper.DEtalleFActuras.Add(detalleFactura);
            dbSuper.SaveChanges();
            ModificarInventarioProducto(detalleFactura.CodigoProducto, detalleFactura.Cantidad, "Resta");
            return factura.Numero.ToString();
        }
        private int ObtenerNumeroFactura()
        {
            return dbSuper.FACTuras.Select(f => f.Numero).DefaultIfEmpty(0).Max() + 1;
            /*
            int Numero = 0;
            try
            {
                Numero = dbSuper.FACTuras.Max(x => x.Numero);
            }
            catch (Exception)
            {
                Numero = 0;
            }
            return Numero + 1;
            */
        }
        public IQueryable MostrarDetalleFactura(int NumeroFactura)
        {
            return from DF in dbSuper.Set<DEtalleFActura>()
                   join P in dbSuper.Set<PRODucto>()
                   on DF.CodigoProducto equals P.Codigo
                   join TP in dbSuper.Set<TIpoPRoducto>()
                   on P.CodigoTipoProducto equals TP.Codigo
                   where DF.Numero == NumeroFactura
                   select new
                   {
                       Eliminar = "<img src=\"../Imagenes/Eliminar.png\" onclick=\"Eliminar(" + DF.Codigo + ", " + DF.Cantidad + ", " + DF.ValorUnitario + ")\" style=\"cursor:grabbing\"/>",
                       Tipo_Producto = TP.Nombre,
                       Producto = P.Nombre,
                       Cantidad = DF.Cantidad,
                       Valor_Unitario = DF.ValorUnitario,
                       SubTotal = DF.Cantidad * DF.ValorUnitario
                   };
        }
        public string EliminarDetalle()
        {
            DEtalleFActura _detalle = factura.DEtalleFActuras.First();
            detalleFactura = dbSuper.DEtalleFActuras.FirstOrDefault(d => d.Codigo == _detalle.Codigo);
            dbSuper.DEtalleFActuras.Remove(detalleFactura);
            dbSuper.SaveChanges();
            ModificarInventarioProducto(detalleFactura.CodigoProducto, detalleFactura.Cantidad, "Suma");
            return factura.Numero.ToString();
        }
    }
}