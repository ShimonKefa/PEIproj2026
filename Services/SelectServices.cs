using APIPEITESTE01.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using APIPEITESTE01.Entities;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using APIPEITESTE01.Entities.Enums;
namespace APIPEITESTE01.Services
{
    public class SelectServices
    {
        //List all data by client 
        public List<Client> ReadAllClients()
        {
            using var context = new DBConnection();
            context.Database.EnsureCreated();
            //gera um retorno com join, *no lugar do include*
            return context.clients
            .Include(c => c.comorbities)
            .Include(c => c.Anamneses)
            .ToList();
        }

        public Client GetClientbyID(Guid clientID)
        {
            using var context = new DBConnection();

            return context.clients
            .Include(c => c.comorbities)
            .Include(c => c.Anamneses)
            .FirstOrDefault(c => c.ID == clientID);
        }
        
        //Lista os exercícios
        public List<WorkOut> GetWorkOuts(Guid clientID)
        {
            using var context = new DBConnection();

            return context.workOuts
            .Include(w => w.Days)
            .ThenInclude(d => d.excercices)
            .Where(w => w.ClientID == clientID)
            .ToList();
        }
    }
}