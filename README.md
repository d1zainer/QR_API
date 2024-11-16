# QR_Api
Этот API предоставляет возможность генерации QR-кодов на основе пользовательских данных. Он позволяет пользователям закодировать текстовые сообщения в QR-коды с возможностью настройки цветового оформления (цвет фона и переднего плана). Сгенерированные QR-коды возвращаются в виде изображений в формате PNG, что позволяет легко интегрировать их в веб-приложения и мобильные приложения.API разработан с учетом простоты использования и быстрой интеграции. Он идеально подходит для приложений, которые требуют создания QR-кодов для различных целей, таких как маркетинг, учёт, или обмен информации. Пользователи могут отправлять текстовые сообщения и получать визуализированные QR-коды, которые могут быть сканированы с помощью мобильных устройств.

- Генерация QR-кодов для веб-сайтов и приложений.
- Создание QR-кодов для визиток, плакатов и рекламных материалов.
- Обмен контактной информацией, ссылками и текстовыми сообщениями через QR-коды.
### Request (запрос)
##### Конечная точка
**POST** http://helpful-orca-worthy.ngrok-free.app/api/GetQr
##### Описание
Эндпоинт для генерации QR-кодов на основе предоставленных данных. Пользователь отправляет JSON-запрос с текстовыми данными и цветами для QR-кода, а API возвращает изображение QR-кода в формате Base64.

### Параметры запроса
- **InputData** (string): данные, которые будут закодированы в QR-код.
- **BgColor** (string): цвет фона QR-кода в формате HEX (например, `#FFFFFF`).
- **FgColor** (string): цвет переднего плана QR-кода в формате HEX (например, `#000000`).

### Пример запроса
```json
{
    "InputData": "Example data for QR code",
    "BgColor": "#FFFFFF",
    "FgColor": "#000000"
}
```

### Response (запрос)
### Параметры ответа
- **outputData** (string): картинка, в формате string base64,
- format (string): формат картинки.

### Пример ответа

```json
{    "outputData": "iVBORw0KGgoAAAANSUhEUgAAALUAAAC1CAYAAAAZU76pAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAABG7SURBVHhe7ZzBjuPGEgT9/z/tt5xTTSjRoZxujt7CDCB0UGYWtQYvxhr+599//vn3Vz1NesaUWE7Y39VImymxnFjfcmJ95nd7PTIGd3qa9IwpsZywv6uRNlNiObG+5cT6zO/2emQM7vQ06RlTYjlhf1cjbabEcmJ9y4n1md/t9cgY3Olp0jOmxHLC/q5G2kyJ5cT6lhPrM7/b65Hpy6PYfeaUpM60Jd2Ykt3cOL3flVhO2n5LuP+81OnGlOzmxun9rsRy0vZbwv3npU43pmQ3N07vdyWWk7bfEu4/L3W6MSW7uXF6vyuxnLT9lnDfX2rmJmnz1pbdvdHeZ7/VON23nFifuUlC/rzUp2nvs99qnO5bTqzP3CQhf17q07T32W81TvctJ9ZnbpKQPy/1adr77Lcap/uWE+szN0nI//",   
 "format": "Png"}
```

Чтобы получить и отобразить QR-код, строку, которую вы получили в поле `OutputData` ответа, нужно декодировать из **Base64** и преобразовать в изображение. Вот шаги для этого:

1. **Получите строку** `OutputData` из JSON-ответа вашего API.
2. **Декодируйте строку из Base64** в массив байтов.
3. **Создайте изображение** на основе массива байтов.
### Пример для C# #

Если вы хотите сохранить или отобразить QR-код, можно использовать следующий код на C#:

C#

```csharp
using System;
using System.Drawing;
using System.IO;
string base64String = "<Ваша строка OutputData из ответа>";
byte[] imageBytes = Convert.FromBase64String(base64String);
using (MemoryStream ms = new MemoryStream(imageBytes))
{    Image qrImage = Image.FromStream(ms);  
 // Сохранить изображение на диск    
 qrImage.Save("qrCode.png",
  System.Drawing.Imaging.ImageFormat.Png);   
   // Либо отобразить изображение в UI, если вы используете WinForms или WPF}
}
```

### Пример для JavaScript

Если вы работаете с фронтендом, на JavaScript, можно использовать следующий код для отображения изображения в браузере:

JavaScript

```javascript
// Получите строку OutputData из ответа API
const base64String = "<Ваша строка OutputData из ответа>";
const imgElement = document.createElement("img");
imgElement.src = `data:image/png;base64,${base64String}`;
document.body.appendChild(imgElement);

```
