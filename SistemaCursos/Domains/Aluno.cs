using System;
using System.Collections.Generic;

namespace SistemaCursos.Domains;

public partial class Aluno
{
    public int AlunoID { get; set; }

    public string NomeAluno { get; set; } = null!;

    public string EmailAluno { get; set; } = null!;

    public byte[] Senha { get; set; } = null!;

    public bool? StatusAluno { get; set; }

    public virtual ICollection<Matricula> Matricula { get; set; } = new List<Matricula>();
}
