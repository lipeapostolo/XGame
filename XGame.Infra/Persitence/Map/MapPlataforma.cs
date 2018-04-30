using System.Data.Entity.ModelConfiguration;
using XGame.Domain.Entities;

namespace XGame.Infra.Persitence.Map
{
    public class MapPlataforma : EntityTypeConfiguration<Platafoma>
    {
        public MapPlataforma()
        {
            ToTable("Plataforma");

            Property(p => p.Nome).HasMaxLength(50).IsRequired();

    
        }
    }
}
