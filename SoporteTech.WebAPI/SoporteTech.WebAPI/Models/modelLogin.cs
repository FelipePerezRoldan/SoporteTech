using System;

namespace SoporteTech.WebAPI.Models
{
    public class Login
    {
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string PaginaSolicitud { get; set; }
    }
    public class LoginRespuesta
    {
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public string Rol { get; set; }
        public string PaginaInicio { get; set; }
        public bool Autenticado { get; set; }
        public string Token { get; set; }
        public string Mensaje { get; set; }
    }
    public class TicketRespuesta
    {
        public int TicketID { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string Equipo {  get; set; }
        public string Descripcion { get; set; }
    }
}