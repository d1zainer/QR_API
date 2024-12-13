using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using QR_API.Models;
using QR_API.Services;

namespace QR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QrCodeController : ControllerBase
    {
        /// <summary>
        /// Получить QR
        /// </summary>
        /// <remarks>7
        /// Sample request:
        ///
        ///     POST /api/QRCode
        ///     {
        ///        "InputData": "Hello QR",
        ///        "BgColor": "#FF5733",
        ///        "FgColor": "#FFFFFF"
        ///     }
        /// </remarks>
        /// <response code="200">Запрос успешный, qr создан</response>
        [HttpPost("GetQr")] //api/GetQr
        public ActionResult<QrCodeResponse> GeQrByName([FromBody] QrCodeRequest qr)
        {
            var validationResult = ValidationService.GetValidationResult(qr);
            if (validationResult != null) // Если валидация не прошла, возвращаем ошибку
            {
                return validationResult;
            }
            var result = GenerateQrCodeService.GetQrByName(qr);
            return result;
        }
    }
}
