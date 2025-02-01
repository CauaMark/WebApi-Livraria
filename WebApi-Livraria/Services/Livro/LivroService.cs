using Microsoft.EntityFrameworkCore;
using WebApi_Livraria.Data;
using WebApi_Livraria.DTO.Autor;
using WebApi_Livraria.DTO.Livro;
using WebApi_Livraria.Models;

namespace WebApi_Livraria.Services.Livro
{
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContext _context;

        public LivroService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros.Include(a => a.Autor).Where(livroBanco => livroBanco.Id == idAutor).ToListAsync();

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum registro localizado";
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livros localizados";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDTO livroCriacaoDTO)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {

                var autor = await _context.Autores.
                    FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDTO.Autor.Id);

                if (autor == null)
                {
                    resposta.Mensagem = "Autor não localizado";
                    return resposta;
                }

                var livro = new LivroModel()
                {
                    Titulo = livroCriacaoDTO.Titulo,
                    Autor = autor
                };

                _context.Add(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
                resposta.Mensagem = "Livro criado com sucesso!";
                return resposta;

            }
            catch (Exception ex)
            {

                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }

        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDTO livroEdicaoDTO)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros.
                    Include(a => a.Autor).
                    FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDTO.Id);

                var autor = await _context.Autores.
                    FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroEdicaoDTO.Autor.Id);

                if (livro == null)
                {
                    resposta.Mensagem = "Livro não encontrado";
                    return resposta;
                }

                if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado";
                    return resposta;
                }

                livro.Titulo = livroEdicaoDTO.Titulo;
                livro.Autor = autor;

                _context.Update(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
                resposta.Mensagem = "Livro editado com sucesso.";
                return resposta;

            }
            catch (Exception ex)
            {

                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros.FirstOrDefaultAsync(livrobanco => livrobanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Livro não encontrado!";
                    return resposta;
                }

                _context.Remove(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
                resposta.Mensagem = "Livro excluido com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            {
                var livro = await _context.Livros.Include(a => a.Autor).FirstOrDefaultAsync(livrobanco => livrobanco.Id == idLivro);
                if (livro == null)
                {
                    resposta.Mensagem = "Livro não econtrado!";
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livro econtrado.";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
			ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
			try
			{
                var livros = await _context.Livros.Include(a => a.Autor).ToListAsync();

                if (livros == null)
                {
                    resposta.Mensagem = "Nenhum livro encontrado!";
                    return resposta;
                }

                resposta.Dados = livros;
                return resposta;
            }
			catch (Exception ex)
			{
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }
    }
}
