using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XGame.Domain.Interfaces.Repositories.Base
{
    /*
     * É um repositorio generico onde a 
     * TEntidade -> é a class na qual eu to representando na quele momento
     * TId -> é uma representação do Id para podermos usar na busca
     */
    public interface IRepositoryBase<TEntidade, TId>
        where TEntidade : class
        where TId : struct
    {
        /*
         * Expression<Func<TEntidade, bool>> -> Permita a expressão labda e o retorno é um booleano
         * includeProperties -> inclue o objetos que está dentro do objeto
         */
        IQueryable<TEntidade> ListarPor(Expression<Func<TEntidade, bool>> where, params Expression<Func<TEntidade, object>>[] includeProperties);

        IQueryable<TEntidade> ListarEOrdenadosPor<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, object>>[] includeProperties);

        TEntidade ObterPor(Func<TEntidade, bool> where, params Expression<Func<TEntidade, object>>[] includeProperties);

        bool Existe(Func<TEntidade, bool> where);

        IQueryable<TEntidade> Listar(params Expression<Func<TEntidade, object>>[] includeProperties);

        IQueryable<TEntidade> ListarOrdenadosPor<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, object>>[] includeProperties, bool ascendente = true);

        TEntidade ObterPorId(TId id, params Expression<Func<TEntidade, object>>[] includeProperties);

        TEntidade Adicionar(TEntidade entidade);

        TEntidade Editar(TEntidade entidade);

        void Remover(TEntidade entidade);

        IEnumerable<TEntidade> AdicionarLista(IEnumerable<TEntidade> entidades);
    }
}
