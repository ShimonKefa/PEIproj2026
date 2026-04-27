using APIPEITESTE01.Entities.Enums;
using Microsoft.Data.Sqlite;
namespace APIPEITESTE01.Entities
{
    public class Client
    {
        //pk
        public Guid ID { get; set; } = Guid.NewGuid();
        //Relacionamentos
        public List<Comorbity> comorbities {get; set;} = new(); // new(); é  a mesma coisa que new list<>()
        public List<Anamnese> Anamneses { get; set; } = new();
        public List<WorkOut> workOuts {get; set;} = new();
        
        public string Name { get; set; }
        public string CPF {get; set;}
        public DateTime BirthDate { get; set; }
        //altura
        public double Height { get; set; }
        //peso
        public double Weight { get; set; }
        public double FatPercentage { get; set; }
        public BodyTypeEnum BodyType { get; set; }


        public Client(){ }
        public Client(string name, string cpf, DateTime date, double height, double weight, double fatPercentage, BodyTypeEnum bodyType) : this()
        {
            Name = name;
            CPF = cpf;
            BirthDate = date;
            Height = height;
            Weight = weight;
            FatPercentage = fatPercentage;
            BodyType = bodyType;
        }

    }
}