using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Models;


namespace SESCAP.Ecommerce.Repositorios
{
    public class CartaoRepositorio: ICartaoRepositorio
    {
        private Db2Context Banco { get; }

        public CartaoRepositorio(Db2Context banco)
        {
            Banco = banco;
        }

        public CARTAO Cartao(int numCartao)
        {
           

            return Banco.Cartoes.Include(ct => ct.HSTMOVCARTS).ThenInclude(h => h.PRODUTOPDV)
                .Include(ct => ct.CLIENTELA)
                    .ThenInclude(cl => cl.UOP)
                        .ThenInclude(uop => uop.ADMIN)
                .Include(ct => ct.SALDOCARTAO)
                .FirstOrDefault(cart => cart.NUMCARTAO == numCartao);
            

        }
    }
}
