using InclusaoDiversidade.Domain;
using Microsoft.EntityFrameworkCore;

namespace InclusaoDiversidade.Infrastructure.Data;

public partial class AppDbContext
{
    // Implementa o hook gerado pelo scaffold.
    // TB_VAGAS não possui SEQUENCE/IDENTITY para a PK, então a aplicação define
    // o ID manualmente (ValueGeneratedNever garante que o EF envie o valor no INSERT).
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbVaga>()
            .Property(e => e.IdVaga)
            .ValueGeneratedNever();
    }
}
