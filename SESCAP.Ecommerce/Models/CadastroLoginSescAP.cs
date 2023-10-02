using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using SESCAP.Ecommerce.Libraries.Lang;


namespace SESCAP.Ecommerce.Models
{

    public class CadastroLoginSescAP
    {

        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E01")]
        [MinLength(13, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E02")]
        [Display(Name = "Matrícula")]
        public string MATRICULA { get; set; }


        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E01")]
        [EmailAddress(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E03")]
        [Display(Name = "E-Mail")]
        public string EMAIL { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E01")]
        [Display(Name = "CPF")]
        [Remote(action: "ValidaCPF", controller: "Home")]
        public string CPF { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E01")]
        [MinLength(6, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E02")]
        [Display(Name = "Senha")]
        public string SENHA { get; set; }

        [NotMapped]
        [Display(Name = "Confirme a Senha")]
        [Compare("SENHA", ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E04")]
        public string CONFIRMACAOSENHA { get; set; }


       
    }
  
}
