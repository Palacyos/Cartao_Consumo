using System;
using System.Collections.Generic;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public interface ICartaoRepositorio
    {
        CARTAO Cartao(int numCartao);
    }
}
