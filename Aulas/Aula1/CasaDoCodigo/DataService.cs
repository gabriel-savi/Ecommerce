using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo
{
    class DataService : IDataService
    {
        private readonly ApplicationContext contexto;
        private readonly IProdutoRepository produtoRepository;

        public DataService(ApplicationContext contexto, IProdutoRepository produtoRepository)
        {
            this.contexto = contexto;
            this.produtoRepository = produtoRepository;
        }

        public void InicializaDB()
        {
            //Quando a aplicação for startada verifica se o banco de dados foi criado, caso não tenha sido, ele cria automaticamente sem precisar rodas as migrations
            contexto.Database.EnsureCreated();

            List<livro> livros = GetLivros();

            produtoRepository.SaveProdutos(livros);
        }


        private static List<livro> GetLivros()
        {
            var json = File.ReadAllText("livros.json");
            var livros = JsonConvert.DeserializeObject<List<livro>>(json);
            return livros;
        }
    }
}
