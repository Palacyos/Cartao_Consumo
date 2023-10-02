using System;
using Newtonsoft.Json;
using SESCAP.Ecommerce.Models;

namespace SESCAP.Ecommerce.Libraries.Login
{
    public class LoginClientela
    {
        private Sessao.Sessao Sessao { get; }
        private string Chave = "Login.Clientela";

        public LoginClientela(Sessao.Sessao sessao)
        {
            Sessao = sessao;
        }

        public void Entrar(CLIENTELA clientela)
        {
            string clientelaJsonString = JsonConvert.SerializeObject(clientela);
            Sessao.CadastrarSessao(Chave, clientelaJsonString);
        }

        public CLIENTELA Obter()
        {
            if (Sessao.ExisteSessao(Chave))
            {
                string clientelaJsonString = Sessao.ConsultarSessao(Chave);
                return JsonConvert.DeserializeObject<CLIENTELA>(clientelaJsonString);

            }
            else
            {
                return null;
            }
        }

        public void Sair()
        {
            Sessao.RemoverTodosDaSessao();
        }

    }
}
