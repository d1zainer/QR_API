using Swashbuckle.AspNetCore.Annotations;
using System.Drawing;
using System.Drawing.Imaging;

namespace QR_API.Models
{
    /// <summary>
    /// Модель запроса
    /// </summary>
    public class QrCodeRequest
    {
        /// <summary>
        /// Данные для генерации QR-кода.
        /// </summary>
        /// <example>Hello World</example>
        public string? InputData { get; set; } 
        /// <summary>
        /// Цвет фона QR-кода в формате HEX.
        /// </summary>
        /// <example>#FF0000</example>
        public string? BgColor { get; set; }
        /// <summary>
        /// Цвет переднего плана QR-кода в формате HEX.
        /// </summary>
        /// <example>#000000</example>
        public string? FgColor { get; set; } 

    }

    /// <summary>
    /// Модель ответа
    /// </summary>
    public class QrCodeResponse
    {
        /// <summary>
        /// Результат генерации QR-кода.
        /// </summary>
        public string? OutputData { get; set; } // То, что нужно возразщается клиенту
       /// <summary>
       /// Формат изображения
       /// </summary>
        public string? Format { get; set; } // Формат картинки - Png
    }
}
