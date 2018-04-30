using System;
using System.Collections.Generic;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using XGame.Domain.Arguments.Jogador;
using XGame.Domain.Entities;
using XGame.Domain.Interfaces.Repositories;
using XGame.Domain.Interfaces.Services;
using XGame.Domain.Resources;
using XGame.Domain.ValueObjects;
using System.Linq;
using XGame.Domain.Arguments.Jogador.Base;

namespace XGame.Domain.Services
{
    public class ServiceJogador : Notifiable, IServiceJogador
    {
        private readonly IRepositoryJogador _repositoryJogador;

        public ServiceJogador()
        {
            
        }

        public ServiceJogador(IRepositoryJogador repositoryJogador)
        {
            _repositoryJogador = repositoryJogador;
        }

        public AdicionarJogadorResponse AdicionarJogador(AdicionarJogadorRequest request)
        {
            if (request == null)
            {
                AddNotification("Jogador", Message.X0_E_OBRIGATORIO.ToFormat("Jogador"));
            }

            var nome = new Nome(request.PrimeiroNome, request.UltimoNome);
            var email = new Email(request.Email);

            Jogador jogador = new Jogador(nome, email, request.Senha);
            if (this.IsInvalid())
            {
                return null;
            }
            jogador = _repositoryJogador.Adicionar(jogador);

            return (AdicionarJogadorResponse) jogador;
        }

        public AlterarJogadorResponse AlterarJogador(AlterarJogadorRequest request)
        {
            if (request == null)
            {
                AddNotification("Jogador", Message.X0_E_OBRIGATORIO.ToFormat("Jogador"));
            }

            Jogador jogador = _repositoryJogador.ObterPorId(request.Id);

            if(jogador == null)
            {
                AddNotification("Id", "O jogador não foi encontrado");
                return null;
            }

            var nome = new Nome(request.PrimeiroNome, request.UltimoNome);
            var email = new Email(request.Email);

            jogador.AlterarJogador(nome, email);

            AddNotifications(jogador);

            if (this.IsInvalid())
            {
                return null;
            }

            _repositoryJogador.Editar(jogador);

            return (AlterarJogadorResponse)jogador;
        }

        public AutenticarJogadorResponse AutenticarJogador(AutenticarJogadorRequest request)
        {
            if (request == null)
            {
                AddNotification("Jogador", Message.X0_E_OBRIGATORIO.ToFormat("Jogador"));
            }

            var email = new Email(request.Email);
            var jogador = new Jogador(email, request.Senha);

            AddNotifications(jogador,email);

            if (jogador.IsInvalid())
            {
                return null;
            }

            jogador = _repositoryJogador.ObterPor(
                x => x.Email.Endereco == jogador.Email.Endereco,
                x => x.Senha == jogador.Senha);
            //jogador = _repositoryJogador.AutenticarJogador(jogador.Email.Endereco, jogador.Senha);



            return (AutenticarJogadorResponse) jogador;
        }

        public ResponseBase ExcluirJogador(Guid id)
        {
            Jogador jogador = _repositoryJogador.ObterPorId(id);
            if(jogador == null)
            {
                AddNotification("Id", "Dados não encontrado");
                return null;
            }
            _repositoryJogador.Remover(jogador);

            return new ResponseBase();
        }

        public IEnumerable<JogadorResponse> ListarJogador()
        {
            return _repositoryJogador.Listar().Select(jogador => (JogadorResponse)jogador).ToList();
        }
    }
}