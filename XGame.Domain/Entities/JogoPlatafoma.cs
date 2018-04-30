using System;

namespace XGame.Domain.Entities
{
    public class JogoPlatafoma
    {
        public Guid Id { get; set; }
        public Jogo Jogo { get; set; }
        public Platafoma Platafoma { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
