using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface.Genericos
{
    public interface IGenericos<T> where T : class //Usando o T para ser o elemento generico 
    {
        //
        Task Adicionar(T obj); 

        Task Atualizar(T obj);

        Task Excluir(T obj);

        Task<T> BuscarPorId(int Id);// 

        Task<List<T>> Listar(); //Faz a listagem dos metodos
    }
}
