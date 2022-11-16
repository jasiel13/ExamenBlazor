/*aqui creamos el metodo customconfirm para editar a nuestro gusto el sweetalert y poder pasar los parametros necesarios*/
function CustomConfirm(titulo, mensaje, tipo) {
    //retornamos una promesa
    return new Promise((resolve) => {
        Swal.fire({
            title: titulo,//le pasamos el titulo
            text: mensaje,//le pasamos el mensaje
            icon: tipo,//le pasamos el tipo questions,info,warning
            showCancelButton: true,
            confirmButtonColor: '#02889B',
            cancelButtonColor: '#dc3545',
            confirmButtonText: 'Confirmar'
        }).then((result) => {
            //resolve true es que la promesa retorno exitosamente el valor en este caso true
            if (result.value) {
                resolve(true);
            }
            else {
                resolve(false);
            }
        });
    });
}