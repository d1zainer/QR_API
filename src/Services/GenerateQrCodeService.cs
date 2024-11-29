using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using QR_API.Models;

using IronSoftware.Drawing;

using Microsoft.AspNetCore.Mvc;

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
            QRCodeEncoder encoder = new QRCodeEncoder();

            encoder.QRCodeBackgroundColor = ColorTranslator.FromHtml(request.BgColor);
            
            encoder.QRCodeForegroundColor = ColorTranslator.FromHtml(request.FgColor);
            try
            {
                Bitmap qrcode = encoder.Encode(request.InputData, Encoding.UTF8);
                QrCodeResponse response = new QrCodeResponse
                {
                    OutputData = Convert.ToBase64String(GetArray(qrcode)), // Конвертируем в Base64
                    Format = AnyBitmap.ImageFormat.Png.ToString() // Устанавливаем формат
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new QrCodeResponse
                {
                    OutputData = ex.Message
                });
            }
        }

        /// Создает массив байтов из битмапа
        /// </summary>
        /// <param name="qrcode"></param>
        /// <returns></returns>
        private static byte[] GetArray(Bitmap qrcode)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                qrcode.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
        
    }
}
