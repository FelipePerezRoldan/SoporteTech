﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoporteTech.WebAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SoporteTechDBEntities : DbContext
    {
        public SoporteTechDBEntities()
            : base("name=SoporteTechDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Equipos> Equipos { get; set; }
        public virtual DbSet<HistorialTicket> HistorialTickets { get; set; }
        public virtual DbSet<Permiso> Permisos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TokensActivo> TokensActivos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
    }
}
