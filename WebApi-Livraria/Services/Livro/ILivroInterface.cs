using WebApi_Livraria.DTO.Autor;
using WebApi_Livraria.DTO.Livro;
using WebApi_Livraria.Models;

namespace WebApi_Livraria.Services.Livro
{
    public interface ILivroInterface
    {

        Task<ResponseModel<List<LivroModel>>> ListarLivros();

        Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro);
        Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor);
        Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDTO livroCriacaoDTO);
        Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDTO autorEdicaoDTO);
        Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro);

    }
}
