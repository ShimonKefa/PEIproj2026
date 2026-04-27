using Microsoft.EntityFrameworkCore;
using APIPEITESTE01.Entities;
using APIPEITESTE01.Data;
using APIPEITESTE01.Entities.Enums;
using Microsoft.Data.Sqlite;

namespace APIPEITESTE01.Services
{
    public class InsertServices
    {
        public Client InsertClient(Client client)
        {
            try
            {
            using var context = new DBConnection();
            context.Database.EnsureCreated();

            client.comorbities = new();
            client.Anamneses = new();
            context.clients.Add(client);  
            context.SaveChanges();

            return client;   
            }
            catch(SqliteException ex)
            {
                throw new Exception("Error no InsertClient: " + ex.Message);               
            }
        }
        
        public Comorbity InsertComorbity(Comorbity comorbity)
        {
            try
            {
            using var context = new DBConnection();
            context.Database.EnsureCreated();

            comorbity.client = null;

            context.Entry(comorbity).State = EntityState.Added;

            context.SaveChanges();
            return comorbity;    
            }
            catch(SqliteException ex)
            {
                throw new Exception("Error no InsertComorbity:" + ex.Message);
            }
            
        }

        public Anamnese InsertAnamnese (Anamnese anamnese)
        {
            try
            {
            using var context = new DBConnection();
            context.Database.EnsureCreated();
            anamnese.client = null;
            context.Entry(anamnese).State = EntityState.Added;
            context.anamneses.Add(anamnese);

            context.SaveChanges();

            return anamnese;   
            }
            catch(SqliteException ex)
            {
                throw new Exception("Error no Insert Anamnese" + ex.Message);
            }
            
        }

        public WorkOut InsertWorkOut(WorkOut workOut)
        {
            try
            {
                using var context = new DBConnection();
                context.Database.EnsureCreated();

                workOut.client = null;
                
                 foreach (var day in workOut.Days)
                {
                    day.WorkOutID = workOut.ID;
                    day.workOut = null;

                    foreach (var exercise in day.excercices)
                    {
                        exercise.WorkOutDayID = day.ID;
                        exercise.workOutDay = null;
                    }
                }
                context.workOuts.Add(workOut);
                context.SaveChanges();
                return workOut;
            }
            catch(SqliteException ex)
            {
                throw new Exception("Error no InsertWorkOut:" + ex.Message);
            }
        }
    }
}