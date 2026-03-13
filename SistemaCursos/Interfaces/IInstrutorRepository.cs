using SistemaCursos.Domains;

namespace SistemaCursos.Interfaces
{
    public interface IInstrutorRepository
    {
        List<Instrutor> Listar();

        Instrutor ObterPorID(int id);
        Instrutor ObterPorEmail(string email);
        Instrutor ObterPorEspecializacao(string especializacao);
        bool EmailExiste(string email);

        void Adicionar(Instrutor instrutor);
        void Atualizar(Instrutor instrutor);
        void Remover(int id);

    }
}
