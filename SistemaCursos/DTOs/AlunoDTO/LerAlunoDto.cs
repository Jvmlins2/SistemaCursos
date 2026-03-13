namespace SistemaCursos.DTOs.AlunoDTO
{
    public class LerAlunoDto
    {       
            public int AlunoID { get; set; }
            public string Nome { get; set; } = null!;
            public string Email { get; set; } = null!;
            public bool StatusAluno{ get; set; }
    }
}
