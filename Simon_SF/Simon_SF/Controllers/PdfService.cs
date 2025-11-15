using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
using Blazor.SIMONStore.Shared;

public class PdfService
{
    public byte[] GenerateInvoicePdf(Order order)
    {
        return Document.Create(document =>
        {
            document.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .AlignCenter()
                    .Text($"Orden N° {order.OrderNumber}")
                    .Bold()
                    .FontSize(18);

                page.Content().Column(col =>
                {
                    col.Item().Text($"Fecha: {order.OrderDate:dd/MM/yyyy}");
                    col.Item().Text($"Cliente: {order.NombreCliente ?? "N/A"}");
                    col.Item().Text($"RIF: {order.RIFCliente ?? "N/A"}");

                    col.Item().PaddingVertical(10).LineHorizontal(1);

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.ConstantColumn(50);
                            columns.ConstantColumn(80);
                            columns.ConstantColumn(80);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Producto").Bold();
                            header.Cell().Text("Cant.").Bold();
                            header.Cell().Text("Precio").Bold();
                            header.Cell().Text("Subtotal").Bold();
                        });

                        if (order.Products != null)
                        {
                            foreach (var product in order.Products)
                            {
                                table.Cell().Text(product.Name);
                                table.Cell().Text(product.Quantity.ToString());
                                table.Cell().Text($"{Math.Round(product.Price, 2)}.$");  // Mostrar en dólares con 2 decimales

                                decimal subtotal = product.FacturaPor == 1
                                    ? Math.Round(product.Quantity * (decimal)product.promedio_peso * product.Price, 2)
                                    : Math.Round(product.Quantity * product.Price, 2);
                                table.Cell().Text($"{subtotal}.$");
                            }
                        }
                    });

                    col.Item().PaddingVertical(10).LineHorizontal(1);
                    col.Item().AlignRight().Text($"Total: {Math.Round(order.Total, 2)}.$")  // Mostrar en dólares con 2 decimales
                        .FontSize(14)
                        .Bold();
                });

                page.Footer()
                    .AlignCenter()
                    .Text("Gracias por su compra")
                    .FontSize(10);
            });
        }).GeneratePdf();
    }

}
