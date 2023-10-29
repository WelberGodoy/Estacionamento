using Estacionamento.Domain.Enums;
using System;

namespace Estacionamento.Domain.Entities
{
    public class Vaga : BaseEntity
    {
        public int Quantidade { get; set; }
        public TipoVaga TipoVaga { get; set; }
        public virtual ICollection<Veiculo> Veiculos { get; set; }
    }
}
