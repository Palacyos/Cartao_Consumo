using Microsoft.AspNetCore.Http;

namespace SESCAP.Ecommerce.Libraries.Sessao
{
    public class Sessao
    {
        private IHttpContextAccessor Contexto { get; }

        public Sessao(IHttpContextAccessor contexto)
        {
            Contexto = contexto;
        }


        public void CadastrarSessao(string chave, string valor)
        {
            Contexto.HttpContext.Session.SetString(chave, valor);
        }

        public void AtualizarSessao(string chave, string valor)
        {
            if (ExisteSessao(chave))
            {
                RemoverSessao(chave);
            }

            Contexto.HttpContext.Session.SetString(chave, valor);
        }

        public void RemoverSessao(string chave)
        {
            Contexto.HttpContext.Session.Remove(chave);
        }

        public string ConsultarSessao(string chave)
        {
            return Contexto.HttpContext.Session.GetString(chave);
        }

        public bool ExisteSessao(string chave)
        {
            if(Contexto.HttpContext.Session.GetString(chave) == null)
            {
                return false;
            }
            return true;
        }

        public void RemoverTodosDaSessao()
        {
            Contexto.HttpContext.Session.Clear();
        }

    }
}
