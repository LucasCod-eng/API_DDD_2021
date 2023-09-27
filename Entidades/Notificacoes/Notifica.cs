using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Notificacoes
{
    public class Notifica //Aqui é aonde passa todas as notificações do sistema, com as validações de string e decimal
    {
        
        public Notifica() 
        {
            Notificacoes = new List<Notifica>();
        
        }

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string Mensagem { get; set; }

        [NotMapped]
        public List<Notifica> Notificacoes;


        //Aqui se o valor ou nome da propriedade for nulo ou não for preenchido, ele ira retornar uma mensgem de
        //campo obrigatorio, retornando em Notifica
        public bool ValidarPropriedadeString(string valor, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifica
                {
                    Mensagem = "Campo Obrigatório" , 
                    NomePropriedade = nomePropriedade
                }) ;

                return false;
            }

            return true;

        }


        public bool ValidarPropriedadeDecimal(decimal valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifica
                {
                    Mensagem = "Valor deve ser maior que 0",
                    NomePropriedade = nomePropriedade
                });

                return false;
            }

            return true;

        }


    }
}
