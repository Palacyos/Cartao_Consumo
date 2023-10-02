using System;
using AutoMapper;
using Cielo;
using Newtonsoft.Json;
using SESCAP.Ecommerce.Models;
using SESCAP.Ecommerce.Models.Constantes;

namespace SESCAP.Ecommerce.Libraries.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, PagamentoOnline>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(orig => orig.MerchantOrderId))
                .ForMember(dest => dest.FormaPgto, opt => opt.MapFrom(orig =>

                    (orig.Payment.GetPaymentType().ToString() == "CreditCard")
                        ? TipoPagamentoConstante.CartaoCredito : TipoPagamentoConstante.Pix))

                .ForMember(dest => dest.Total, opt => opt.MapFrom(orig => orig.Payment.GetAmount()))
                .ForMember(dest => dest.Transacao, opt => opt.MapFrom(orig => JsonConvert.SerializeObject(orig)))
                .ForMember(dest => dest.DataPgto, opt => opt.MapFrom(orig => DateTime.Now.ToShortDateString()));
                


        }
    }
}
