using Microsoft.EntityFrameworkCore;
using SistemaCursos.Contexts;
using SistemaCursos.Domains;
using SistemaCursos.Interfaces;

namespace SistemaCursos.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly SistemaCursosContext _context;

        public CursoRepository(SistemaCursosContext context)
        {
            _context = context;
        }

        public List<Curso> Listar()
        {
            List<Curso> cursos = _context.Curso.ToList();
            return cursos;
        }

        public Curso ObterPorId(int id)
        {
            Curso? curso = _context.Curso.Find(id);
            return curso;
        }

        public Curso NomeExiste(string nome, int? cursoIdAtual = null)
        {
            var cursoConsultado = _context.Curso.AsQueryable();

            if(cursoIdAtual.HasValue)
            {
                cursoConsultado = cursoConsultado.Where(curso => curso.CursoID != cursoIdAtual.Value);
            }
            return cursoConsultado.Any(curso => curso.NomeCurso == nome);
        }

        public void Adicionar(Curso curso)
        {
            _context.Add(curso);
        }

        public void Atualizar(Curso curso)
        {
            Curso? cursoBanco = _context.Curso.FirstOrDefault(cursoAux => cursoAux.InstrutorID == curso.CursoID);

            if (cursoBanco == null)
            {
                return;
            }

            cursoBanco.NomeCurso = curso.NomeCurso;
            cursoBanco.Instrutor = curso.Instrutor;
            cursoBanco.CargaHoraria = curso.CargaHoraria;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Curso? curso = _context.Curso.FirstOrDefault(cursoAux => cursoAux.CursoID == id);
        }


    }
}
