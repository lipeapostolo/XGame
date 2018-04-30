using System;
using prmToolkit.NotificationPattern;
using XGame.Domain.Enum;
using XGame.Domain.Resources;
using XGame.Domain.ValueObjects;
using XGame.Domain.Extensions;
using XGame.Domain.Entities.Base;

namespace XGame.Domain.Entities
{
    public class Jogador : EntityBase
    {
        public Jogador(Email email, string senha)
        {
            Email = email;
            Senha = senha;
            new AddNotifications<Jogador>(this)
                .IfNullOrInvalidLength(x => x.Senha, 6, 32, Message.X_SENHA);

            if (IsInvalid())
            {
                Senha = Senha.ConvertToMD5();
            }
        }

        public Jogador(Nome nome, Email email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Status = EnumSituacaoJogador.EmAnalise;

            new AddNotifications<Jogador>(this)
                .IfNullOrInvalidLength(x => x.Senha, 6, 32);

            if (IsValid())
            {
                Senha = Senha.ConvertToMD5();
            }
            
            AddNotifications(nome, email);
        }

        public void AlterarJogador(Nome nome, Email email)
        {
            Nome = nome;
            Email = email;

            new AddNotifications<Jogador>(this).IfFalse(Status == EnumSituacaoJogador.Ativo, "Só é possivel alterar jogador se ele estiver ativo");

            AddNotifications(nome, email);
        }

        public Guid Id { get; private set; }
        public Nome Nome { get; private set; }
        public Email Email { get; private set; }
        public string Senha { get; private set; }
        public EnumSituacaoJogador Status { get; private set; }
        public override string ToString()
        {
            return this.Nome.PrimeiroNome + " " + this.Nome.UltimoNome;
        }

    }
}
