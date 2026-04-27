using APIPEITESTE01.Data;
using APIPEITESTE01.Entities;
using APIPEITESTE01.Entities.Enums;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace APIPEITESTE01.Services
{
    public class UpdateServices
    {
        public Client UpdateClient(Guid clientID, Client client)
        {
            using var context = new DBConnection();
            context.Database.EnsureCreated();
            var clientsB = context.clients.Find(clientID); 
            if(clientsB == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            clientsB.Name = client.Name;
            clientsB.CPF = client.CPF;
            clientsB.BirthDate = client.BirthDate;
            clientsB.Height = client.Height;
            clientsB.Weight = client.Weight;
            clientsB.FatPercentage = client.FatPercentage;
            clientsB.BodyType = client.BodyType;

            context.SaveChanges();


            return clientsB;
        }

        public Anamnese UpdateAnamnese(Guid anamneseID, Anamnese anamnese)
        {
            using var context = new DBConnection();
            context.Database.EnsureCreated();
            var anam = context.anamneses.Find(anamneseID);
            if(anam == null)
            {
                throw new Exception("Anamnese não encontrada");
            }
            anam.objectiveTypeEnum = anamnese.objectiveTypeEnum;

            context.SaveChanges();

            return anam;
        }

        public Comorbity UpdateComorbitiy(Guid comorbityID, Comorbity comorbity)
        {
            using var context = new DBConnection();
            context.Database.EnsureCreated();
            var com = context.comorbities.Find(comorbityID);
            if(com == null)
            {
                throw new Exception("Anamnese não encontrada");
            }
            com.Description = comorbity.Description;
            com.RiskLevelEnum = comorbity.RiskLevelEnum;

            context.SaveChanges();

            return com;
        }

        public WorkOut UpdateWorkOut(Guid workOutID, WorkOut workOut)
        {
            using var context = new DBConnection();
            context.Database.EnsureCreated();

            var existing = context.workOuts
            .Include(w => w.Days)
            .ThenInclude(d => d.excercices)
            .FirstOrDefault(w => w.ID == workOutID);

            if(existing == null)
            {
                throw new Exception("Treino Inválido");
            }

            existing.experienceLevel = workOut.experienceLevel;
            existing.intencityWorkOut = workOut.intencityWorkOut;
            existing.GeneralNotes = workOut.GeneralNotes;
            existing.DaysPerWeek = workOut.DaysPerWeek;

            context.SaveChanges();

            return existing;
        }
    }
}