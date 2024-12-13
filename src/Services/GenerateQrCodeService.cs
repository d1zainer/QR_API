using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using QR_API.Models;

using Microsoft.AspNetCore.Mvc;
using SkiaSharp.QrCode;
using SkiaSharp;
using System.Text;

namespace QR_API.Services
{
    public class GenerateQrCodeService
    {
        /// <summary>
        /// Возвращает QR из данных о qr
        /// </summary>
        /// <param name="request">Запрос с данными для QR-кода</param>
        /// <returns>Ответ с данными QR-кода или ошибкой</returns>
        public static ActionResult<QrCodeResponse> GetQrByName(QrCodeRequest request)
        {
            using var generator = new QRCodeGenerator();
            var level = ECCLevel.H;
            var qr = generator.CreateQrCode(request.InputData, level);
            var info = new SKImageInfo(512, 512);
            using var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            canvas.Render(qr, 512, 512, SKColors.White, SKColor.Parse(request.FgColor), SKColor.Parse(request.BgColor)) ;
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            var base64String = Convert.ToBase64String(data.ToArray());
            var utf8Bytes = Encoding.UTF8.GetBytes(base64String);
            var utf8Base64String = Encoding.UTF8.GetString(utf8Bytes);
            var response = new QrCodeResponse()
            {
                OutputData = utf8Base64String, // Убедимся, что строка в UTF-8
                Format = SKEncodedImageFormat.Png.ToString()
            };
            return response;
           
        }
    }
}
