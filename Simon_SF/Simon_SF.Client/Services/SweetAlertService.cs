using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

public class SweetAlertService
{
    private readonly IJSRuntime _jsRuntime;

    public SweetAlertService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    // Muestra el SweetAlert de confirmación y devuelve un valor booleano (true si confirma, false si cancela)
    public async Task<bool> ShowConfirmationDialog(string title, string text)
    {
        // Cambiar el tipo de retorno a dynamic
        var result = await _jsRuntime.InvokeAsync<JsonElement>("Swal.fire", new
        {
            title = title,
            text = text,
            icon = "error",
            showCancelButton = true,
            confirmButtonText = "Eliminar",
            cancelButtonText = "Cancelar",
            reverseButtons = true
        });

        // Verificar si la propiedad 'isConfirmed' existe y convertirla a booleano
        if (result.TryGetProperty("isConfirmed", out var isConfirmedProperty))
        {
            return isConfirmedProperty.GetBoolean();
        }

        return false;
    }
}
