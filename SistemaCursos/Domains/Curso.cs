using System;
using System.Collections.Generic;

namespace SistemaCursos.Domains;

public partial class Curso
{
    public int CursoID { get; set; }

    public string NomeCurso { get; set; } = null!;

    public string CargaHoraria { get; set; } = null!;

    public int? InstrutorID { get; set; }

    public bool? StatusCurso { get; set; }

    public virtual Instrutor? Instrutor { get; set; }

    public virtual ICollection<Matricula> Matricula { get; set; } = new List<Matricula>();
}
