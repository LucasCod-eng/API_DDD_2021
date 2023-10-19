using Dominio.Interface.Genericos;
using Infraestrutura.Configuracoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio.Genericos
{
    public class RepositorioGenerico<T> : IGenericos<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<Contexto> _OptionsBuilder;

        public RepositorioGenerico() 
        {
            _OptionsBuilder = new DbContextOptions<Contexto>();
        }
        public async Task Adicionar(T obj)
        {
            using (var data = new Contexto(_OptionsBuilder))
            {
                await data.Set<T>().AddAsync(obj);
                await data.SaveChangesAsync();
            }
        }

        public async Task Atualizar(T obj)
        {
            using (var data = new Contexto(_OptionsBuilder))
            {
                data.Set<T>().Update(obj);
                await data.SaveChangesAsync();
            }
        }

        public async Task<T> BuscarPorId(int Id)
        {
            using (var data = new Contexto(_OptionsBuilder))
            {              
                return await data.Set<T>().FindAsync(Id);
            }
        }

        public async Task Excluir(T obj)
        {
            using (var data = new Contexto(_OptionsBuilder))
            {
                data.Set<T>().Remove(obj);
                await data.SaveChangesAsync();
            }
        }

        public async Task<List<T>> Listar()
        {
            using (var data = new Contexto(_OptionsBuilder))
            {
                //AsNoTracking: Ele Faz o Select no Banco tranzendo tudo que está guardado, sendo muito mais
                //Perfotmático
                return await data.Set<T>().AsNoTracking().ToListAsync();
            }
        }



        bool disposed = false;
        SafeHandle Handle = new SafeFileHandle(IntPtr.Zero, true);
        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                return;
            if (disposing)
            {
                Handle.Dispose();
            }

            disposed = true;
        }


    }
}
