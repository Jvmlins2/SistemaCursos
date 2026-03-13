using SistemaCursos.Domains;
using SistemaCursos.DTOs.InstrutorDTO;
using SistemaCursos.Exceptions;
using SistemaCursos.Interfaces;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace SistemaCursos.Applications.Services
{
    public class InstrutorService
    {
        private readonly IInstrutorRepository _repository;

        public InstrutorService(IInstrutorRepository repository)
        {
            _repository = repository;
        }

        private static LerInstrutorDto LerDto(Instrutor instrutor)
        {
            LerInstrutorDto lerInstrutor = new LerInstrutorDto
            {
                InstrutorId = instrutor.InstrutorID,
                Nome = instrutor.NomeInstrutor,
                Email = instrutor.EmailInstrutor,
                Especializacao = instrutor.Especializacao,
                StatusInstrutor = instrutor.StatusInstrutor ?? true
            };
            return lerInstrutor;
        }

        public List<LerInstrutorDto> Listar()
        {
            List<Instrutor> instrutores = _repository.Listar();

            List<LerInstrutorDto> instrutoresDto = instrutores
                .Select(instrutorBanco => LerDto(instrutorBanco)).ToList();
            return instrutoresDto;
        }

        private static void ValidarEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new DomainException("E-mail inválido.");
            }
        }

        private static byte[] HashSenha(string senha)
        {
            if(string.IsNullOrWhiteSpace(senha))
            {
                throw new DomainException("Senha é obrigatória.");
            }
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerInstrutorDto ObterPorId(int id)
        {
            Instrutor? instrutor = _repository.ObterPorID(id);
            if(instrutor == null)
            {
                throw new DomainException("Instrutor não existe.");
            }
            return LerDto(instrutor);
        }

        public LerInstrutorDto ObterPorEmail(string email)
        {
            Instrutor? instrutor = _repository.ObterPorEmail(email);
            if (instrutor == null)
            {
                throw new DomainException("Instrutor não existe.");
            }
            return LerDto(instrutor);
        }

        public LerInstrutorDto Adicionar(CriarInstrutorDto instrutorDto)
        {
            ValidarEmail(instrutorDto.EmailInstrutor);
            if(_repository.EmailExiste(instrutorDto.EmailInstrutor))
            {
                throw new DomainException("Já existe um usuário com esse e-mail.");
            }

            Instrutor instrutor = new Instrutor
            {
                NomeInstrutor = instrutorDto.nomeInstrutor,
                EmailInstrutor = instrutorDto.EmailInstrutor,
                Especializacao = instrutorDto.Especializacao,
                Senha = HashSenha(instrutorDto.Senha),
                StatusInstrutor = true
            };
            _repository.Adicionar(instrutor);
            return LerDto(instrutor);
        }

        public LerInstrutorDto Atualizar(int id, CriarInstrutorDto instrutorDto)
        {
            ValidarEmail(instrutorDto.EmailInstrutor);

            Instrutor instrutorBanco = _repository.ObterPorID(id);

            if(instrutorBanco == null)
            {
                throw new DomainException("Instrutor não encontrado.");
            }

            ValidarEmail(instrutorDto.EmailInstrutor);

            Instrutor instrutorComMesmoEmail = _repository.ObterPorEmail(instrutorDto.EmailInstrutor);

            if (instrutorComMesmoEmail != null && instrutorComMesmoEmail.InstrutorID != id)
            {
                throw new DomainException("Já existe um instrutor com este e-mail.");
            }

            instrutorBanco.NomeInstrutor = instrutorDto.nomeInstrutor;
            instrutorBanco.EmailInstrutor = instrutorDto.EmailInstrutor;
            instrutorBanco.Especializacao = instrutorDto.Especializacao;
            instrutorBanco.Senha = HashSenha(instrutorDto.Senha);

            _repository.Atualizar(instrutorBanco);
            return LerDto(instrutorBanco);
        }

        public void Remover(int id)
        {
            Instrutor instrutor = _repository.ObterPorID(id);

            if(instrutor == null)
            {
                throw new DomainException("Instrutor não encontrado.");
            }

            _repository.Remover(id);
        }
    }
}
