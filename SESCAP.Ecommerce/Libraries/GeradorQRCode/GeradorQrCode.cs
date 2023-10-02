
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;


namespace SESCAP.Ecommerce.Libraries.GeradorQRCode
{
    public static class GeradorQrCode
    {

        public static string GerarImagem(string content)
        {
            QRCodeData qrCodeData;

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            }

            var imgType = Base64QRCode.ImageType.Png;
            var qrCode = new Base64QRCode(qrCodeData);

            string qrCodeImageAsBase64 = qrCode.GetGraphic(10, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.White, true, imgType);

            return string.Format($"data:image/{imgType};base64,{qrCodeImageAsBase64}");
        }

    }
}
