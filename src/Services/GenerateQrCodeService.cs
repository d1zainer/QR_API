using MessagingToolkit.QRCode.Codec;
<<<<<<< HEAD
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using QR_API.Models;
using System.Text.RegularExpressions;
=======
using System.Text;
using QR_API.Models;
using IronSoftware.Drawing;
>>>>>>> main
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
<<<<<<< HEAD
            encoder.QRCodeBackgroundColor = ColorTranslator.FromHtml(request.BgColor);
            encoder.QRCodeForegroundColor = ColorTranslator.FromHtml(request.FgColor);
            try
            {
                Bitmap qrcode = encoder.Encode(request.InputData, Encoding.UTF8);
                QrCodeResponse response = new QrCodeResponse
                {
                    OutputData = Convert.ToBase64String(GetArray(qrcode)), // Конвертируем в Base64
                    Format = ImageFormat.Png.ToString() // Устанавливаем формат
=======
            encoder.QRCodeBackgroundColor = FromHexToColor(request.BgColor);
            encoder.QRCodeForegroundColor = FromHexToColor(request.FgColor);
            try
            {
                IronSoftware.Drawing.AnyBitmap qrcode = encoder.Encode(request.InputData, Encoding.UTF8);
                QrCodeResponse response = new QrCodeResponse
                {
                    OutputData = Convert.ToBase64String(qrcode.GetBytes()), // Конвертируем в Base64
                    Format = AnyBitmap.ImageFormat.Png.ToString() // Устанавливаем формат
>>>>>>> main
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
<<<<<<< HEAD
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
       
=======
        /// <summary>
        /// Преобразует HEX-цвет в System.Drawing.Color
        /// </summary>
        /// <param name="hexColor">Цвет в HEX-формате</param>
        /// <returns>Объект System.Drawing.Color</returns>
        private static IronSoftware.Drawing.Color FromHexToColor(string hexColor)
        {
            // Убираем символ "#" если он есть
            hexColor = hexColor.Replace("#", string.Empty);

            // Преобразуем HEX в RGB
            byte r = Convert.ToByte(hexColor.Substring(0, 2), 16);
            byte g = Convert.ToByte(hexColor.Substring(2, 2), 16);
            byte b = Convert.ToByte(hexColor.Substring(4, 2), 16);

            return System.Drawing.Color.FromArgb(r, g, b);
        }

>>>>>>> main
    }
}
