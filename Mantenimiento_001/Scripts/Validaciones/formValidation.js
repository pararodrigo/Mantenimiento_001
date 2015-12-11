$(document).ready(function (){

    //formulario creacion maquina
    $("#maquinaForm").validate({
        rules: {
            maq_name: "required",
        },
        errorClass: "errorClass"
    });

    //formulario creacion tipo
    $("#tipoForm").validate({
        rules: {
            ty_name: "required",
        },
        errorClass: "errorClass"
    });

    //formulario creacion personal
    $("#personalForm").validate({
        rules: {
            users_name: "required",
            users_fname: "required",
            users_lname: "required",
            users_alias: "required"
        },
        errorClass: "errorClass"
    });

    //formulario edicion personal
    $("#personalEditForm").validate({
        rules: {
            users_name: "required",
            users_fname: "required",
            users_lname: "required",
            users_alias: "required"
           
        },
        errorClass: "errorClass"
    });

    //formulario edicion password
    $("#passwordForm").validate({
        rules: {
            users_password0:"required",
            users_password: "required",
            users_password2: {
                equalTo: "#passwordInput"
            }
        },
        errorClass: "errorClass"
    });
    
})
