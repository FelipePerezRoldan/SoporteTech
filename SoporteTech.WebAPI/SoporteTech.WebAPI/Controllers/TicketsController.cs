using SoporteTech.WebAPI.Clases;
using SoporteTech.WebAPI.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SoporteTech.WebAPI.Controllers
{
    [EnableCors(origins: "https://localhost:63842", headers: "*", methods: "*")]
    [RoutePrefix("API/Tickets")]
    //[Authorize]
    public class TicketsController : ApiController
    {
        //[HttpGet]
        //[Route("Consultar")]
        //public PRODucto Consultar(int Codigo)
        //{
        //    clsProducto Producto = new clsProducto();
        //    return Producto.Consultar(Codigo);
        //}
        [HttpGet]
        [Route("ListarTickets")]
        public IQueryable<TicketRespuesta> ListarTickets()
        {
            clsTickets _tickets = new clsTickets();
            return _tickets.ListarTicketsAbiertos();
        }
    }
    //    [HttpGet]
    //    [Route("ListarProductosXTipo")]
    //    public IQueryable ListarProductosXTipo(int TipoProducto)
    //    {
    //        clsProducto Producto = new clsProducto();
    //        return Producto.ListarProductosXTipo(TipoProducto);
    //    }
    //    [HttpPost]
    //    [Route("Insertar")]
    //    public string Insertar([FromBody] PRODucto producto)
    //    {
    //        clsProducto Producto = new clsProducto();
    //        Producto.producto = producto;
    //        return Producto.Insertar();
    //    }
    //    [HttpPut]
    //    [Route("Actualizar")]
    //    public string Actualizar([FromBody] PRODucto producto)
    //    {
    //        clsProducto Producto = new clsProducto();
    //        Producto.producto = producto;
    //        return Producto.Actualizar();
    //    }
    //    [HttpDelete]
    //    [Route("Eliminar")]
    //    public string Eliminar([FromBody] PRODucto producto)
    //    {
    //        clsProducto Producto = new clsProducto();
    //        Producto.producto = producto;
    //        return Producto.Eliminar();
    //    }
    //}
}