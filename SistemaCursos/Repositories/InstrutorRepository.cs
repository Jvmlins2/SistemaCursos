using SistemaCursos.Contexts;
using SistemaCursos.Domains;
using SistemaCursos.Interfaces;

namespace SistemaCursos.Repositories
{
    public class InstrutorRepository : IInstrutorRepository
    {
        private readonly SistemaCursosContext _context;

        public InstrutorRepository(SistemaCursosContext context)
        {
            _context = context;
        }

        public List<Instrutor> Listar()
        {
            return _context.Instrutor.ToList();
        }

        public Instrutor? ObterPorID(int id)
        {
            return _context.Instrutor.Find(id);
        }

        public Instrutor? ObterPorEmail(string email)
        {
            return _context.Instrutor.FirstOrDefault(instrutor => instrutor.EmailInstrutor == email);
        }

        public Instrutor? ObterPorEspecializacao(string especializacao)
        {
            return _context.Instrutor.Find(especializacao);
        }

        public bool EmailExiste(string email)
        {
            return _context.Instrutor.Any(instrutor => instrutor.EmailInstrutor == email);
        }

        public void Adicionar(Instrutor instrutor)
        {
            _context.Add(instrutor);
        }

        public void Atualizar(Instrutor instrutor)
        {
            Instrutor? instrutorBanco = _context.Instrutor.FirstOrDefault(instrutorAux => instrutorAux.InstrutorID == instrutor.InstrutorID);

            if (instrutorBanco == null)
            {
                return;
            }

            instrutorBanco.NomeInstrutor = instrutor.NomeInstrutor;
            instrutorBanco.EmailInstrutor = instrutor.EmailInstrutor;
            instrutorBanco.Senha = instrutor.Senha;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Instrutor? instrutor = _context.Instrutor.FirstOrDefault(instrutorAux => instrutorAux.InstrutorID == id);
        }


    }
}