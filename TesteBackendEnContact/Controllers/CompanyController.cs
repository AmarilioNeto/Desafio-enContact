using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Controllers.Models;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ILogger<CompanyController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ICompany>> Post(SaveCompanyRequest company, [FromServices] ICompanyRepository companyRepository)
        {
            if(company.Name.Length <=  6)
            {
                return NotFound("Dados invalidos");
            }

            return Ok(await companyRepository.SaveAsync(company.ToCompany()));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromServices] ICompanyRepository companyRepository)
        {
            if(id == null)
            {
                return BadRequest(new { Message = "Id invalido", Success= false});
            }
            await companyRepository.DeleteAsync(id);

            return Ok(new { Message = $"o {id} foi excluido com sucesso", Success= false});
        }

        [HttpGet]
        public async Task<IEnumerable<ICompany>> Get([FromServices] ICompanyRepository companyRepository)
        {
            return await companyRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ICompany> Get(int id, [FromServices] ICompanyRepository companyRepository)
        {
            return await companyRepository.GetAsync(id);
        }

        // aqui eu vou retornar uma string, poderia retornar um int se quisesse
        // dai do mesmo jeito eu retorno o que eu preciso, tipo se eu quero retornar um company
        [HttpGet("pegarmeunome")]
        public string PegarMeuNome(string primeiroNome, string segundoNome)
        {
            return primeiroNome + " " + segundoNome;
        }

        // ai agora eu tenho que retornar um SaveCompany, para não dar erro
    }
}
