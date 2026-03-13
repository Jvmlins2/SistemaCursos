using SistemaCursos.Domains;

namespace SistemaCursos.Interfaces
{
    public interface ICursoRepository
    {
        List<Curso> Listar();
        Curso ObterPorId(int id);
        bool NomeExiste(string nome, int? cursoIdAtual = null);

        void Adicionar(Curso curso);
        void Atualizar(Curso curso);
        void Remover(int id);
    }
}
