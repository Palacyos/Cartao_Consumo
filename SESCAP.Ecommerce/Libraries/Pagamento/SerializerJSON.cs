using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Cielo;
using Newtonsoft.Json;

namespace SESCAP.Ecommerce.Libraries.Pagamento
{
    public class SerializerJSON: ISerializerJSON
    {
        public SerializerJSON()
        {
        }

        public T Deserialize<T>(string jsonText)
        {
            return JsonConvert.DeserializeObject<T>(jsonText);
        }

        public async Task<T> DeserializeAsync<T>(HttpContent content)
        {
            var serializer = new JsonSerializer();
            using (var textReader = new StreamReader(await content.ReadAsStreamAsync().ConfigureAwait(false)))
            using (var jsonReader = new JsonTextReader(textReader))
            {
                return serializer.Deserialize<T>(jsonReader);
            }
        }

        public string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
