using Dominio.Interface;
using Dominio.Interface.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoNoticia : IServicoNoticia
    {
        private readonly INoticia _INoticia;
        
        public ServicoNoticia(INoticia INoticia)//Recebe instancia da classe INoticia
        {
            _INoticia = INoticia; //injecao de dependencia
        }
            
        public async Task AdicionaNoticia(Noticia noticia)
        {
            var ValidarTitulo = noticia.ValidarPropriedadeString(noticia.Titulo, "Titulo");
            var ValidarInformacoes = noticia.ValidarPropriedadeString(noticia.Informacao, "Informacao");

            if(ValidarInformacoes && ValidarTitulo)
            {
                noticia.DataAlteracao = DateTime.Now;
                noticia.DataCadastro = DateTime.Now;
                noticia.Ativo = true;
                await _INoticia.Adicionar(noticia); //await = Aguarda de forma assincrona ou seja ele executa a tarefa em conjunto
            }

        }

        public async Task AtualizaNoticia(Noticia noticia)
        {
            var ValidarTitulo = noticia.ValidarPropriedadeString(noticia.Titulo, "Titulo");
            var ValidarInformacoes = noticia.ValidarPropriedadeString(noticia.Informacao, "Informacao");

            if (ValidarInformacoes && ValidarTitulo)
            {
                noticia.DataAlteracao = DateTime.Now;
                noticia.DataCadastro = DateTime.Now;
                noticia.Ativo = true;
                await _INoticia.Atualizar(noticia); //await = Aguarda de forma assincrona ou seja ele executa a tarefa em conjunto
            }

        }

        public async Task<List<Noticia>> ListarNoticiasAtivas()
        {
            return await _INoticia.ListarNoticias(n => n.Ativo);
        }
    }
}
