
//// Abrir el modal para actualizar el ticket
//function openTicketModal(ticketId) {
//    const ticket = tickets.find(t => t.id === ticketId);
//    $('#txtTicketStatus').val(ticket.estado);
//    $('#txtComentario').val('');
//    $('#ticketModal').modal('show');
//}

//// Guardar cambios en el ticket
//$('#saveTicketBtn').on('click', function () {
//    const ticketId = 1; // Simulación del ID del ticket
//    const newStatus = $('#txtTicketStatus').val();
//    const comment = $('#txtComentario').val();

//    // Actualizar el estado y comentario del ticket
//    const ticket = tickets.find(t => t.id === ticketId);
//    ticket.estado = newStatus;
//    // Aquí, el comentario se podría almacenar según la necesidad (simulado)
//    alert('Cambios guardados');
//    $('#ticketModal').modal('hide');
//    loadTickets(); // Recargar la tabla de tickets
//});

// Cargar los tickets cuando se cargue la página
//$(document).ready(function () {
//    loadTickets();
//});

//class Usuario {
//    constructor(id, Documento_Empleado, userName, Clave) {
//        this.id = id;
//        this.Documento_Empleado = Documento_Empleado;
//        this.userName = userName;
//        this.Clave = Clave;
//    }
//}
async function cargarDatosTabla() {
    const URLServicio = "http://localhost:63749/API/Tickets/ListarTickets"; // URL del servidor
    const TablaLlenar = "#ticketsTable"; // Selector de la tabla

    try {
        const Resultado = await fetch(URLServicio, {
            method: "GET",
            mode: "cors",
            headers: {
                "Content-Type": "application/json"
            }
        });

        const Respuesta = await Resultado.json(); // Obtener los datos en formato JSON

        // Verifica que haya datos
        if (Respuesta && Respuesta.length > 0) {
            // Generar las columnas de la tabla a partir de las claves de los objetos
            var Columnas = [];
            const NombreColumnas = Object.keys(Respuesta[0]); // Obtener los nombres de las columnas

            for (let i in NombreColumnas) {
                Columnas.push({
                    data: NombreColumnas[i],    // Datos que se usarán para cada columna
                    title: NombreColumnas[i]    // Título de la columna
                });
            }

            // Inicializar DataTable
            $(TablaLlenar).DataTable({
                data: Respuesta,    // Pasar los datos de la respuesta
                columns: Columnas,  // Pasar las columnas generadas
                destroy: true        // Permitir la recreación de la tabla si ya existe
            });
        } else {
            throw new Error("No se encontraron tickets.");
        }
    } catch (error) {
        // Mostrar el error en el div de mensajes
        $("#dvMensaje").html(`<span style="color:red">${error.message}</span>`);
    }
}

// Llamar a la función para cargar los datos en la tabla al cargar la página
$(document).ready(function () {
    cargarDatosTabla();
});
//jQuery(function ()
//{
//    //Se ejecuta al cargar la página
///*    LlenarComboXServicios("https://localhost:44323/api/Perfiles/LlenarCombo", "");*/
//    LlenarTabla();
//});
//function LlenarTabla()
//{
//    alert( LlenarTablaXServicios("http://localhost:63749/API/Tickets/ListarTickets", "#ticketTableBody"));
//    LlenarTablaXServicios("http://localhost:63749/API/Tickets/ListarTickets", "#ticketTableBody");
//}
//function Editar(Documento, Empleado, Cargo, Usuario, idPerfil, Activo, idUsuarioPerfil) {
//    $("#txtDocumento").val(Documento);
//    $("#txtNombre").val(Empleado);
//    $("#txtCargo").val(Cargo);
//    $("#txtUsuario").val(Usuario);
//    $("#cboPerfil").val(idPerfil);
//    $("#txtidUsuarioPerfil").val(idUsuarioPerfil);
//    $("#txtActivo").val(Activo == "True" ? "SI" : "NO");
//}
//
//async function EjecutarComando(Metodo, Funcion) {
//    let idPerfil = $("#cboPerfil").val();
//    let Clave = $("#txtClave").val();
//    let RepitaClave = $("#txtConfirmaClave").val();
//    if (Clave != RepitaClave) {
//        $("#dvMensaje").html("Las claves no son iguales");
//        return;
//    }
//    let URL = "https://localhost:44323/api/Usuarios/" + Funcion + "?Perfil=" + idPerfil;
//    const usuario = new Usuario(0, $("#txtDocumento").val(), $("#txtUsuario").val(), Clave);
//    await EjecutarServicio(Metodo, URL, usuario);
//    LlenarTabla();
//}
//async function Activar(Activo) {
//    let idUsuarioPerfil = $("#txtidUsuarioPerfil").val();
//    let URL = "https://localhost:44323/api/Usuarios/Activar?idUsuarioPerfil=" + idUsuarioPerfil + "&Activo=" + Activo;
//    const usuario = new Usuario(0, $("#txtDocumento").val(), $("#txtUsuario").val(), "");
//    await EjecutarServicio("PUT", URL, usuario);
//    LlenarTabla();
//}
//async function BuscarEmpleado() {
//    let Documento = $("#txtDocumento").val();
//    let URL = "https://localhost:44323/api/Empleados/ConsultarXDocumento?Documento=" + Documento;
//    const empleado = await ConsultarServicio(URL);
//    if (empleado == null) {
//        $("#dvMensaje").html("El documento del empleado no existe en la base de datos, o no tiene información completa");
//        $("#txtNombre").val("");
//        $("#txtCargo").val("");
//    }
//    else {
//        $("#dvMensaje").html("");
//        $("#txtNombre").val(empleado[0].Empleado);
//        $("#txtCargo").val(empleado[0].Cargo);
//    }
//}
