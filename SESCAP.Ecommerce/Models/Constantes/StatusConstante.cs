using System;
using Cielo;

namespace SESCAP.Ecommerce.Models.Constantes
{
    public class StatusConstante
    {
        public const string Sucesso = "Pago";

        public static string GetNames(string descricao)
        {
            foreach(var field in typeof(StatusConstante).GetFields())
            {
                if ((string)field.GetValue(null) == descricao)
                    return field.Name.ToString();
            }

            return "";

        }

       

    }
}
