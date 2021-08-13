using Dapper.Contrib.Extensions;

namespace RepositoryDapper.Models
{
    [Table("Cad_UF")]
    public class Estado
    {
        [Key]
        public string UF { get; set; }

        public string DESCRICAO { get; set; }
    }
}
