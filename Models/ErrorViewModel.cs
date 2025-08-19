namespace WebApplication1.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; } //propiedad para almacenar el ID de la solicitud

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        //define una accion que utiliza una expresion lambda que retorna true si RequestId no es nulo o vac�o, lo que indica que se debe mostrar el ID de la solicitud en la vista de error.
        //procedimientos anonimos
        //esto es �til para depurar problemas en la aplicaci�n, ya que permite rastrear solicitudes espec�ficas que pueden haber fallado.
    }
}
