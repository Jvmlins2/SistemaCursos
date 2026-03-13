using SistemaCursos.Domains;

namespace SistemaCursos.Interfaces
{
    public interface IAlunoRepository
    {
        List<Aluno> Listar();

        Aluno ObterPorID(int id);
        Aluno ObterPorEmail(string email);
        bool EmailExiste(string email);

        void Adicionar(Aluno aluno);
        void Atualizar(Aluno aluno);
        void Remover(int id);
    }
}
