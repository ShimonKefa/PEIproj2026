using Microsoft.EntityFrameworkCore;
using APIPEITESTE01.Data;
using APIPEITESTE01.Entities;
using APIPEITESTE01.Entities.Enums;
using Microsoft.Data.Sqlite;

namespace APIPEITESTE01.Services
{
    public class DeleteServices
    {
        //deleta o client, quando deleta o cliente somem todos os outros obj associados a esse ID.
    public Client DeleteClient(Guid clientID)
    {
            try
            {
               using var context = new DBConnection();
                context.Database.EnsureCreated();

            //Carrega os relacionamentos
             var clientB = context.clients
            .Include(c => c.comorbities)
            .Include(c => c.Anamneses)
            .FirstOrDefault(c => c.ID == clientID);

                if(clientB == null)
                {
                    throw new Exception("Cliente não encontrado");
                }

            context.clients.Remove(clientB);
            context.SaveChanges();
            return clientB;
            }
            catch(SqliteException ex)
            {
                throw new Exception("Error on DeleteClient" + ex.Message);
            }
    }
        

        public Anamnese DeleteAnamnese(Guid anamID)
        {
            try
            {
            using var context = new DBConnection();
            context.Database.EnsureCreated();
            var anam = context.anamneses.Find(anamID);
            if(anam == null)
            {
                throw new Exception("Anamnese  não encontrada");
            }
            context.anamneses.Remove(anam);

            context.SaveChanges();

            return anam;
            }
            catch(SqliteException ex)
            {
                throw new Exception("Error no DeleteAnamnese:" + ex.Message);
            }
            
        }


        public Comorbity DeleteComorbities(Guid comID)
        {
            try
            {
            using var context = new DBConnection();
            context.Database.EnsureCreated();
            var com = context.comorbities.Find(comID);
            if(com == null)
            {
                throw new Exception("Comorbidade não encontrada");
            }

            context.comorbities.Remove(com);
            context.SaveChanges();

            return com;   
            }
            catch(SqliteException ex)
            {
                throw new Exception("Error no DeleteComorbities:" + ex.Message);
            }
        }

        public WorkOut DeleteWorkout(Guid workoutID)
        {
            try
            {
                using var context = new DBConnection();
                context.Database.EnsureCreated();

                var workOut = context.workOuts
                    .Include(w => w.Days)
                    .ThenInclude(d => d.excercices)
                    .FirstOrDefault(w => w.ID == workoutID);

                if (workOut == null)
                {
                    throw new Exception("Treino Inválido");
                } 

                context.workOuts.Remove(workOut);
                context.SaveChanges();

                return workOut;
            }
            catch (SqliteException ex)
            {
                throw new Exception("Error no DeleteWorkOut: " + ex.Message);
            }
        }
    }
}