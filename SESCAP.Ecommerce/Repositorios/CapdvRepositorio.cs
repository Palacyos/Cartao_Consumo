using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public class CapdvRepositorio: ICapdvRepositorio
    {
        private Db2Context Banco { get; }
       

        public CapdvRepositorio(Db2Context banco)
        {
            Banco = banco;
           
           
        }

        public CAPDV ObterCaixaPdv(int cdpdv)
        {
            return Banco.Capdvs.Include(cpdv => cpdv.LOCALVENDA)
                .ThenInclude(lv => lv.UOP).FirstOrDefault(cpdv => cpdv.CDPDV.Equals(cdpdv));
        }
    }
}
