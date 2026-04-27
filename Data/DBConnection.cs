using APIPEITESTE01.Entities;
using APIPEITESTE01.Entities.Enums;
using APIPEITESTE01.Enviroment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace APIPEITESTE01.Data
{
    public class DBConnection : DbContext
    {
        public string ContextDB {get; set;} = null!;
        
        public DBConnection()
        {
            EnviromentServices enviroment = new EnviromentServices();
            enviroment.EnsureCreated();
            ContextDB = $"Data Source={enviroment.DatabasePath}";
        }
        //Tabelas a serem criadas
        public DbSet<Client> clients {get; set;}
        public DbSet<Anamnese> anamneses {get; set;}
        public DbSet<Comorbity> comorbities {get; set;}
        public DbSet<WorkOut> workOuts {get; set;}
        public DbSet<WorkOutDay> workOutDays {get; set;}
        public DbSet<Excercices> excercices {get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ContextDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Anamnese>()
        .HasOne(a => a.client)
        .WithMany(c => c.Anamneses)
        .HasForeignKey(a => a.ClientID)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comorbity>()
        .HasOne(c => c.client)
        .WithMany(c => c.comorbities)
        .HasForeignKey(c => c.ClientID)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkOut>()
        .HasOne(w => w.client)
        .WithMany(c => c.workOuts)
        .HasForeignKey(w => w.ClientID)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkOutDay>()
        .HasOne(wd => wd.workOut)
        .WithMany(w => w.Days)
        .HasForeignKey(wd => wd.WorkOutID)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Excercices>()
        .HasOne(e => e.workOutDay)
        .WithMany(wd => wd.excercices)
        .HasForeignKey(e => e.WorkOutDayID)
        .OnDelete(DeleteBehavior.Cascade);
        }       


    }
}