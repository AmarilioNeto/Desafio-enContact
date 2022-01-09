using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Company;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;


namespace TesteBackendEnContact.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public CompanyRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<ICompany> SaveAsync(ICompany company)
        {
            // nesse momento, ele abre um tunel de conexão com o banco de dados
            // para então salvar os dados.
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            // aqui ele chama uma classe que tem a inteligencia para tomar decisões 
            // referente ao banco de dados
            var dao = new CompanyDao(company);

            if (dao.Id == 0)
            // aqui ele usa o tunel de conexão que ele abriu. e efetivamente vai salvar
            // se não der nada errado, ou faltar implementação
                dao.Id = await connection.InsertAsync(dao);
            else
                await connection.UpdateAsync(dao);

            return dao.Export();
        }

        public async Task DeleteAsync(int id)
        {         
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM Company WHERE Id = @id;");
            await connection.ExecuteAsync(sql.ToString(), new { id });       
        }

        public async Task<IEnumerable<ICompany>> GetAllAsync()
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Company ";
            var result = await connection.QueryAsync<CompanyDao>(query);

            return result?.Select(item => item.Export());
        }

        public async Task<ICompany> GetAsync(int id)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query =  new StringBuilder();
           query.AppendLine("SELECT * FROM Conpany where Id = @id");
         var result = await connection.QuerySingleOrDefaultAsync(query.ToString(), new {id});
           return result?.Export();
        }
    }

    [Table("Company")]
    public class CompanyDao : ICompany
    {
        [Key]
        public int Id { get; set; }
        public int ContactBookId { get; set; }
        public string Name { get; set; }

        public CompanyDao()
        {
        }

        public CompanyDao(ICompany company)
        {
            Id = company.Id;
            ContactBookId = company.ContactBookId;
            Name = company.Name;
        }

        public ICompany Export() => new Company(Id, ContactBookId, Name);
    }
}
