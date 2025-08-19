namespace WebApplication1.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; } //propiedad para almacenar el ID de la solicitud

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        //define una accion que utiliza una expresion lambda que retorna true si RequestId no es nulo o vacío, lo que indica que se debe mostrar el ID de la solicitud en la vista de error.
        //procedimientos anonimos
        //esto es útil para depurar problemas en la aplicación, ya que permite rastrear solicitudes específicas que pueden haber fallado.
    }
}
