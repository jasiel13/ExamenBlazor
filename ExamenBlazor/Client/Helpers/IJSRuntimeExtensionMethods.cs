using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenBlazor.Client.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        //hacer un metodo de extension para reutilizar el confirm al borrar cualquier cosa
        public static async ValueTask<bool> Confirm(this IJSRuntime JS, string titulo, string mensaje, TipoMensajeSweetAlert tipoMensajeSweetAlert)
        {
            /*aqui con await esperamos el resultado de la promesa que como valor nos devolvera un bool pasamos como parametro el nombre del
             metodo, el titulo,el mensaje y el tipo de valor que maneja sweetalert*/
            return await JS.InvokeAsync<bool>("CustomConfirm", titulo, mensaje, tipoMensajeSweetAlert.ToString());
        }
        //creamos un enum con los tipos que maneja sweetalert para pasarselos al metodo Confirm
        public enum TipoMensajeSweetAlert
        {
            question, warning, error, success, info
        }    
    }
}
