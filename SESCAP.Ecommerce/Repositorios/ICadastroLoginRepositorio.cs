using System;
using System.Collections.Generic;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Repositorios
{
    public interface ICadastroLoginRepositorio
    {

        CadastroLoginSescAP Login(string matricula, string senha);

        void CadastroLogin(CadastroLoginSescAP cadastroLogin);

        void Atualizar(CadastroLoginSescAP cadastroLogin);

        CadastroLoginSescAP VerificarCadastro(string matricula);

        CadastroLoginSescAP ObterCadastro(int Id);

        CadastroLoginSescAP ObterCadastroPorEmail(string email);

        CadastroLoginSescAP ObterCadastroPorCpf(string cpf);
    }
}
