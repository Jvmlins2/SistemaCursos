using SistemaCursos.Applications.Autenticacao;
using SistemaCursos.Domains;
using SistemaCursos.DTOs.AutenticacaoDTO;
using SistemaCursos.Exceptions;
using SistemaCursos.Interfaces;

namespace SistemaCursos.Applications.Services
{
        public class AutenticacaoService
        {
            private readonly IInstrutorRepository _repository;
            private readonly GeradorTokenJwt _tokenJwt;

            public AutenticacaoService(IInstrutorRepository repository, GeradorTokenJwt tokenJwt)
            {
                _repository = repository;
                _tokenJwt = tokenJwt;
            }

            private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
            {
                using var sha = System.Security.Cryptography.SHA256.Create();
                var hashDigitado = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senhaDigitada));

                return hashDigitado.SequenceEqual(senhaHashBanco);
            }

            public TokenDto Login(LoginDto loginDto)
            {
                Instrutor instrutor = _repository.ObterPorEmail(loginDto.Email);

                if (instrutor == null)
                {
                    throw new DomainException("Email ou senha inválidos");
                }

                if (!VerificarSenha(loginDto.Senha, instrutor.Senha))
                {
                    throw new DomainException("Email ou senha inválidos");
                }

                if (instrutor.StatusInstrutor != true)
                {
                    throw new DomainException("Usuário não autenticado");
                }

                var token = _tokenJwt.GerarToken(instrutor);

                TokenDto novoToken = new TokenDto { Token = token };

                return novoToken;
            }
        }
}
