using SistemaCursos.Contexts;
using SistemaCursos.Domains;
using SistemaCursos.Interfaces;

namespace SistemaCursos.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly SistemaCursosContext _context;

        public AlunoRepository(SistemaCursosContext context)
        {
            _context = context;
        }

        public List<Aluno> Listar()
        {
            return _context.Aluno.ToList();
        }

        public Aluno? ObterPorID(int id)
        {
            return _context.Aluno.Find(id);
        }

        public Aluno? ObterPorEmail(string email)
        {
            return _context.Aluno.FirstOrDefault(aluno => aluno.EmailAluno == email);
        }
        public bool EmailExiste(string email)
        {
            return _context.Aluno.Any(aluno => aluno.EmailAluno == email);
        }

        public void Adicionar(Aluno aluno)
        {
            _context.Add(aluno);
        }

        public void Atualizar(Aluno aluno)
        {
            Aluno? alunoBanco = _context.Aluno.FirstOrDefault(alunoAux => alunoAux.AlunoID == aluno.AlunoID);

            if (alunoBanco == null)
            {
                return;
            }

            alunoBanco.NomeAluno = aluno.NomeAluno;
            alunoBanco.EmailAluno = aluno.EmailAluno;
            alunoBanco.Senha = aluno.Senha;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Aluno? aluno = _context.Aluno.FirstOrDefault(alunoAux => alunoAux.AlunoID == id);
        }


    }
}