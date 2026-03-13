using SistemaCursos.Domains;
using SistemaCursos.DTOs.AlunoDTO;
using SistemaCursos.DTOs.InstrutorDTO;
using SistemaCursos.Exceptions;
using SistemaCursos.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SistemaCursos.Applications.Services
{
    public class AlunoService
    {
        private readonly IAlunoRepository _repository;

        public AlunoService(IAlunoRepository repository)
        {
            _repository = repository;
        }

        private static LerAlunoDto LerDto(Aluno aluno)
        {
            LerAlunoDto lerAluno = new LerAlunoDto
            {
                AlunoID = aluno.AlunoID,
                Nome = aluno.NomeAluno,
                Email = aluno.EmailAluno,
                StatusAluno = aluno.StatusAluno ?? true
            };
            return lerAluno;
        }

        public List<LerAlunoDto> Listar()
        {
            List<Aluno> alunos = _repository.Listar();

            List<LerAlunoDto> alunosDto = alunos
                .Select(alunoBanco => LerDto(alunoBanco)).ToList();
            return alunosDto;
        }

        private static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new DomainException("E-mail inválido.");
            }
        }

        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new DomainException("Senha é obrigatória.");
            }
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerAlunoDto ObterPorId(int id)
        {
            Aluno? aluno = _repository.ObterPorID(id);
            if (aluno == null)
            {
                throw new DomainException("Aluno não existe.");
            }
            return LerDto(aluno);
        }

        public LerAlunoDto ObterPorEmail(string email)
        {
            Aluno? aluno = _repository.ObterPorEmail(email);
            if (aluno == null)
            {
                throw new DomainException("Aluno não existe.");
            }
            return LerDto(aluno);
        }

        public LerAlunoDto Adicionar(CriarAlunoDto alunoDto)
        {
            ValidarEmail(alunoDto.EmailAluno);
            if (_repository.EmailExiste(alunoDto.EmailAluno))
            {
                throw new DomainException("Já existe um usuário com esse e-mail.");
            }

            Aluno aluno = new Aluno
            {
                NomeAluno = alunoDto.nomeAluno,
                EmailAluno = alunoDto.EmailAluno,
                Senha = HashSenha(alunoDto.Senha),
                StatusAluno = true
            };
            _repository.Adicionar(aluno);
            return LerDto(aluno);
        }

        public LerAlunoDto Atualizar(int id, CriarAlunoDto alunoDto)
        {
            ValidarEmail(alunoDto.EmailAluno);

            Aluno alunoBanco = _repository.ObterPorID(id);

            if (alunoBanco == null)
            {
                throw new DomainException("Aluno não encontrado.");
            }

            ValidarEmail(alunoDto.EmailAluno);

            Aluno alunoComMesmoEmail = _repository.ObterPorEmail(alunoDto.EmailAluno);

            if (alunoComMesmoEmail != null && alunoComMesmoEmail.AlunoID != id)
            {
                throw new DomainException("Já existe um aluno com este e-mail.");
            }

            alunoBanco.NomeAluno = alunoDto.nomeAluno;
            alunoBanco.EmailAluno = alunoDto.EmailAluno;
            alunoBanco.Senha = HashSenha(alunoDto.Senha);

            _repository.Atualizar(alunoBanco);
            return LerDto(alunoBanco);
        }

        public void Remover(int id)
        {
            Aluno aluno = _repository.ObterPorID(id);

            if (aluno == null)
            {
                throw new DomainException("Aluno não encontrado.");
            }

            _repository.Remover(id);
        }
    }
}
