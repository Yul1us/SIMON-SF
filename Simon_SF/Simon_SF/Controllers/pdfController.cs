using Blazor.SIMONStore.Client.Models;
using Blazor.SIMONStore.Shared;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/pdf")]
public class PdfController : ControllerBase
{
    private readonly PdfService _pdfService;
    private readonly PdfRecibo _PdfRecibo;

    public PdfController(PdfService pdfService, PdfRecibo pdfRecibo)
    {
        _pdfService = pdfService;
        _PdfRecibo = pdfRecibo;
    }

    [HttpPost("invoice")]
    public IActionResult GenerateInvoice([FromBody] Order order)
    {
        if (order == null || order.Products.Count == 0)
        {
            return BadRequest("La order no puede estar vacía.");
        }

        var pdfBytes = _pdfService.GenerateInvoicePdf(order);
        return File(pdfBytes, "application/pdf", "order.pdf");
    }
    [HttpPost("receipt")]
    public IActionResult GenerateReceipt([FromBody] payment payment)
    {
        if (payment == null)
        {
            return BadRequest("El pago no puede estar vacío.");
        }

        var pdfBytes = _PdfRecibo.GenerateReceiptPdf(payment);
        return File(pdfBytes, "application/pdf", "receipt.pdf");
    }
}
