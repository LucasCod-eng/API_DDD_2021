using Aplicacao.Interfaces;
using Dominio.Interface;
using Dominio.Interface.InterfaceServicos;
using Entidades.Entidades;
using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoNoticia : IAplicacaoNoticia
    {
        INoticia _INoticia;
        IServicoNoticia _IServicoNoticia;

        public AplicacaoNoticia(INoticia INoticia, IServicoNoticia IServicoNoticia)
        { 
            _INoticia = INoticia;
            _IServicoNoticia = IServicoNoticia; 

        }
        
        public async  Task AdicionaNoticia(Noticia noticia)
        {
            await _IServicoNoticia.AdicionaNoticia(noticia);
        }

        public async Task Adicionar(Noticia obj)
        {
            await _INoticia.Adicionar(obj);
        }

        public async Task AtualizaNoticia(Noticia noticia)
        {
            await _IServicoNoticia.AtualizaNoticia(noticia);
        }

        public async Task Atualizar(Noticia obj)
        {
            await _INoticia.Atualizar(obj);
        }

        public async Task<Noticia> BuscarPorId(int Id)
        {
            return await _INoticia.BuscarPorId(Id);
        }

        public async Task Excluir(Noticia obj)
        {
            await _INoticia.Excluir(obj);
        }

        public async Task<List<Noticia>> Listar()
        {
            return await _INoticia.Listar();
        }

        public async Task<List<Noticia>> ListarNoticiasAtivas()
        {
           return await _IServicoNoticia.ListarNoticiasAtivas();
        }
    }
}
