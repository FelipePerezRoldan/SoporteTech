class Login {
    constructor(Correo, Clave, PaginaSolicitud) {
        this.Correo = Correo;
        this.Clave = Clave;
        this.PaginaSolicitud = PaginaSolicitud;
    }
}
async function Ingresar() {
    const login = new Login($("#txtUsuario").val(), $("#txtClave").val(), $("#cboRol").val());   
    const Respuesta = await EjecutarServicioRpta("POST", "http://localhost:63749/API/Login/Ingresar", login);
    $("#dvMensaje").removeClass("alert alert-success");
    $("#dvMensaje").addClass("alert alert-danger");
    if (Respuesta == undefined)
    {
        document.cookie = "token=0;path=/";
        $("#dvMensaje").html("Usuario o contraseña errados");       
    }
    else
    {
        if (Respuesta.length == 0)
        {
            $("#dvMensaje").html("El usuario no está registrado, olvidó la clave o seleccionó mal su rol");
            return;
        }
        if (Respuesta[0].Autenticado)
        {
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
        else
        {
            $("#dvMensaje").html("El usuario no tiene permisos");
        }
    }
}
