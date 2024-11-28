using Servicios_6_8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios_6_8.Clases
{
    public class clsCargo
    {
        private DBSuperEntities dbSuper = new DBSuperEntities();
        public CARGo cargo { get; set; }
        public string Insertar()
        {
            try
            {
                dbSuper.CARGoes.Add(cargo);
                dbSuper.SaveChanges();
                return "Se insertó el cargo: " + cargo.Nombre;
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
                dbSuper.CARGoes.AddOrUpdate(cargo); 
                dbSuper.SaveChanges();
                return "Se actualizaron los datos del cargo: " + cargo.Nombre;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Eliminar()
        {
            //Hay que consultar primero
            try
            {
                CARGo _cargo = Consultar(cargo.Codigo);
                if (_cargo == null)
                {
                    return "El código del cargo que se quiere eliminar, no existe en la base de datos";
                }
                else
                {
                    dbSuper.CARGoes.Remove(_cargo);
                    dbSuper.SaveChanges();
                    return "Se eliminaron los datos del cargo: " + cargo.Nombre;
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public CARGo Consultar(int Codigo)
        {
            return dbSuper.CARGoes.FirstOrDefault(c => c.Codigo == Codigo);
        }
    }
}