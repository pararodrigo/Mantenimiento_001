$(document).ready(function (){

    $("#loginForm").validate({
        rules: {
            user: "required",
        },
        errorClass: "errorClass"
    });

})
