using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SESCAP.Ecommerce.Database;
using SESCAP.Ecommerce.Libraries.CriptoSenha;
using SESCAP.Ecommerce.Models;


namespace SESCAP.Ecommerce.Repositorios
{
    public class CadastroLoginRepositorio: ICadastroLoginRepositorio
    {
        private Db2Context Banco { get; }

        public CadastroLoginRepositorio(Db2Context banco)
        {
            Banco = banco;
        }

        public void CadastroLogin(CadastroLoginSescAP cadastroLogin)
        {
            cadastroLogin.SENHA = CriptografiaSenha.HashSenha(cadastroLogin.SENHA);
            Banco.Add(cadastroLogin);
            Banco.SaveChanges();
        }

        public CadastroLoginSescAP Login(string matricula, string senha)
        {
            var hashSenha = CriptografiaSenha.HashSenha(senha);
            return Banco.Cadastros.FirstOrDefault(c => c.MATRICULA.Equals(matricula) && c.SENHA.Equals(hashSenha));
            
        }

        public CadastroLoginSescAP VerificarCadastro(string matricula)
        {
            return Banco.Cadastros.FirstOrDefault(cd => cd.MATRICULA.Equals(matricula));
        }

        public CadastroLoginSescAP ObterCadastro(int Id)
        {
            return Banco.Cadastros.Find(Id);
        }

        public CadastroLoginSescAP ObterCadastroPorEmail(string email)
        {
            return Banco.Cadastros.FirstOrDefault(c => c.EMAIL.Equals(email));
        }

        public void Atualizar(CadastroLoginSescAP cadastroLogin)
        {
            cadastroLogin.SENHA = CriptografiaSenha.HashSenha(cadastroLogin.SENHA);
            Banco.Update(cadastroLogin);
            Banco.SaveChanges();
        }

     
    }
}
