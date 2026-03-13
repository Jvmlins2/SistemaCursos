using System;
using System.Collections.Generic;

namespace SistemaCursos.Domains;

public partial class Instrutor
{
    public int InstrutorID { get; set; }

    public string NomeInstrutor { get; set; } = null!;

    public string EmailInstrutor { get; set; } = null!;

    public byte[] Senha { get; set; } = null!;

    public string Especializacao { get; set; } = null!;

    public bool? StatusInstrutor { get; set; }

    public virtual ICollection<Curso> Curso { get; set; } = new List<Curso>();
}
