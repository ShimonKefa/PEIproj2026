using System.IO;
using Microsoft.Data.Sqlite;
namespace APIPEITESTE01.Enviroment
{
 public  class EnviromentServices
    {
        //pega o local da pasta especial
        public  string FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PEIPROJ2026");

        public  string DatabasePath => Path.Combine(FolderPath, "PEIPROJ2026.db");
        public string API_KEY = "Nice try";
        public EnviromentServices()
        {
            try
            {
                Environment.SetEnvironmentVariable("GOOGLE_API_KEY", API_KEY);
            }
            catch(IOException ex)
            {
                throw new Exception("Erro na chave api:" + ex.Message);
            }
            
        }
        public  void EnsureCreated()
        {
            try
            {
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            if (!File.Exists(DatabasePath))
            {
                File.Create(DatabasePath).Dispose();
            }
            }
            catch(IOException ex)
            {
                throw new Exception("Error no EnsureCreated em Enviroment:" + ex.Message);
            }
            catch(SqliteException ex)
            {
                throw new Exception("Error no ensureCreated" + ex.Message);
            }
            
        }
    }
    
}