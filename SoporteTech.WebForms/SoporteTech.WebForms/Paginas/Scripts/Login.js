async function Ingresar() {
    let URL = "https://localhost:44323/api/Login/Ingresar";
    const login = new Login($("#txtUsuario").val(), $("#txtClave").val());
    const Respuesta = await EjecutarServicioRpta("POST", URL, login);
    $("#dvMensaje").removeClass("alert alert-success");
    $("#dvMensaje").addClass("alert alert-danger");
    if (Respuesta == undefined) {
        document.cookie = "token=0;path=/";
        //Hubo un error al procesar el comando
        $("#dvMensaje").html("El usuario no está registrado u olvidó la clave");
    }
    else {
        if (Respuesta.length == 0) {
            $("#dvMensaje").html("El usuario no está registrado u olvidó la clave");
            return;
        }
        if (Respuesta[0].Autenticado == true) {
            const extdays = 5;
            const d = new Date();
            d.setTime(d.getTime() + (extdays * 24 * 60 * 60 * 1000));
            let expires = ";expires=" + d.toUTCString();
            document.cookie = "token=" + Respuesta[0].Token + expires + ";path=/";
            //let token = getCookie("token");
            $("#dvMensaje").removeClass("alert alert-danger");
            $("#dvMensaje").addClass("alert alert-success");
            $("#dvMensaje").html(Respuesta[0].Autenticado);
            document.cookie = "Perfil=" + Respuesta[0].Perfil;
            document.cookie = "Usuario=" + Respuesta[0].Usuario;
            //alert(Respuesta[0].Perfil);
            window.location.href = Respuesta[0].PaginaInicio;
        }
        else {
            $("#dvMensaje").html("El usuario no tiene permisos");
        }
    }
}
class Login {
    constructor(Usuario, Clave) {
        this.Usuario = Usuario;
        this.Clave = Clave;
    }
}