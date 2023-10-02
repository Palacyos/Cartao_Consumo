using System;
namespace SESCAP.Ecommerce.Models.Constantes
{
    public class TipoPagamentoConstante
    {
        public const string CartaoCredito = "Crédito";
        public const string Pix = "Pix";


        public static string GetNames(string descricao)
        {
            foreach (var field in typeof(TipoPagamentoConstante).GetFields())
            {
                if ((string)field.GetValue(null) == descricao)
                    return field.Name.ToString();
            }

            return "";

        }
    }
}
