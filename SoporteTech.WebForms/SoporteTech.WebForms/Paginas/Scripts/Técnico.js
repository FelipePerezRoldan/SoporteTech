const tickets = [
    { id: 1, fecha: '2024-11-01', equipo: 'PC01', estado: 'En progreso' },
    { id: 2, fecha: '2024-11-03', equipo: 'PC02', estado: 'Pendiente' },
    { id: 3, fecha: '2024-11-05', equipo: 'PC03', estado: 'Completado' }
];

// Función para cargar los tickets en la tabla
//function loadTickets() {
//    let tableBody = $('#ticketTableBody');
//    tableBody.empty(); // Limpiar la tabla antes de cargar los datos

//    tickets.forEach(ticket => {
//        let row = `<tr>
//                    <td>${ticket.id}</td>
//                    <td>${ticket.fecha}</td>
//                    <td>${ticket.equipo}</td>
//                    <td>${ticket.estado}</td>
//                    <td><button class="btn btn-info" onclick="openTicketModal(${ticket.id})">Actualizar</button></td>
//                </tr>`;
//        tableBody.append(row);
//    });
//}
// Función para cargar los tickets desde un servidor externo
function loadTickets() {
    let tableBody = $('#ticketTableBody');
    tableBody.empty(); // Limpiar la tabla antes de cargar los datos

    // Realizar la solicitud fetch al servidor para obtener los tickets
    fetch('https://api.ejemplo.com/tickets')  // Reemplaza con la URL de tu API
        .then(response => {
            if (!response.ok) {
                throw new Error('Error al cargar los tickets');
            }
            return response.json();  // Parsear la respuesta JSON
        })
        .then(tickets => {
            // Recorrer los tickets obtenidos y agregar las filas a la tabla
            tickets.forEach(ticket => {
                let row = `<tr>
                    <td>${ticket.id}</td>
                    <td>${ticket.fecha}</td>
                    <td>${ticket.equipo}</td>
                    <td>${ticket.estado}</td>
                    <td><button class="btn btn-info" onclick="openTicketModal(${ticket.id})">Actualizar</button></td>
                </tr>`;
                tableBody.append(row);
            });
        })
        .catch(error => {
            console.error('Error al cargar los tickets:', error);
            alert('Hubo un problema al cargar los tickets.');
        });
}


// Abrir el modal para actualizar el ticket
function openTicketModal(ticketId) {
    const ticket = tickets.find(t => t.id === ticketId);
    $('#txtTicketStatus').val(ticket.estado);
    $('#txtComentario').val('');
    $('#ticketModal').modal('show');
}

// Guardar cambios en el ticket
$('#saveTicketBtn').on('click', function () {
    const ticketId = 1; // Simulación del ID del ticket
    const newStatus = $('#txtTicketStatus').val();
    const comment = $('#txtComentario').val();

    // Actualizar el estado y comentario del ticket
    const ticket = tickets.find(t => t.id === ticketId);
    ticket.estado = newStatus;
    // Aquí, el comentario se podría almacenar según la necesidad (simulado)
    alert('Cambios guardados');
    $('#ticketModal').modal('hide');
    loadTickets(); // Recargar la tabla de tickets
});

// Cargar los tickets cuando se cargue la página
$(document).ready(function () {
    loadTickets();
});