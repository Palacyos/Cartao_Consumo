using System;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public interface ICapdvRepositorio
    {
        CAPDV ObterCaixaPdv(int cdpdv);
    }
}
