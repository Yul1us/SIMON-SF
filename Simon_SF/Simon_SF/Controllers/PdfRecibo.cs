using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using Blazor.SIMONStore.Client.Models;

public class PdfRecibo
{
    public byte[] GenerateReceiptPdf(payment payment)
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
                    .Text("Recibo de Pago")
                    .Bold()
                    .FontSize(18);

                page.Content().Column(col =>
                {
                    col.Item().Text($"Fecha: {payment.Fecha:dd/MM/yyyy}");
                    col.Item().Text($"Referencia: {payment.Referencia ?? "N/A"}");
                    col.Item().Text($"Beneficiario: {payment.Beneficiario ?? "N/A"}");
                    col.Item().Text($"Banco: {payment.Banco ?? "N/A"}");
                    col.Item().Text($"Moneda: {payment.Sg_Moneda ?? "N/A"}");

                    col.Item().PaddingVertical(10).LineHorizontal(1);

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.ConstantColumn(100);
                        });

                        table.Cell().Text("Debe").Bold();
                        table.Cell().Text($"{payment.Debe:C}");

           

                        table.Cell().Text("Tasa").Bold();
                        table.Cell().Text($"{payment.Tasa:N2}");

                        table.Cell().Text("Monto en Divisa").Bold();
                        table.Cell().Text($"{payment.Monto_Divisa:N2}");
                    });

                    col.Item().PaddingVertical(10).LineHorizontal(1);

                    col.Item().Text($"Comentario: {payment.Comentario ?? "Sin comentarios"}")
                        .Italic();
                });

                page.Footer()
                    .AlignCenter()
                    .Text("Este documento es un comprobante de pago.")
                    .FontSize(10);
            });
        }).GeneratePdf();
    }
}
