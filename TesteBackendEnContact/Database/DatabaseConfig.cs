using Microsoft.Extensions.Configuration;

namespace TesteBackendEnContact.Database
{
    public class DatabaseConfig
    {
        // para pegar as paradas do appsettings, tu vai fazer um parse daquela classe para ca

        // no c#, a gente chama de injeção de dependencia no construtor, assim tu especifica o tip
        /* na pratica muda muita coisa não, é como se tu instanciasse Configuration config = new Configuration()
        mas dai tu só vai auto instanciar.
        
        vamo fazer um teste*/
       
        public DatabaseConfig()
        {
        }

    
        public string ConnectionString { get; set; }

        

    }  

}


