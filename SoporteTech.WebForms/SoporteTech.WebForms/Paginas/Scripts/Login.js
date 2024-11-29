class Login {
    constructor(Correo, Clave, Rol) {
        this.Correo = Correo;
        this.Clave = Clave;
        this.Rol = Rol;
    }
}
async function Ingresar() {
    /*let URL = "http://localhost:63749/API/Login/Ingresar";*/
    const login = new Login($("#txtUsuario").val(), $("#txtClave").val(), $("#cboRol").val()); 
    try {
        const Resultado = await fetch("http://localhost:63749/API/Login/Ingresar",
            {
                method: "POST",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(login)
            });
        const Respuesta = await Resultado.json();
        return Respuesta;
    }
    catch (error) {
        $("#dvMensaje").html(error);
    }
    alert("Datos: " + JSON.stringify(Respuesta))
    $("#dvMensaje").removeClass("alert alert-success");
    $("#dvMensaje").addClass("alert alert-danger");
    if (Respuesta == undefined) {
        document.cookie = "token=0;path=/";
        //Hubo un error al procesar el comando
        $("#dvMensaje").html("Usuario o contraseña errados");
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
