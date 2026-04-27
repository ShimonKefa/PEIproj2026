using APIPEITESTE01.Entities.Enums;
using Microsoft.Data.Sqlite;
namespace APIPEITESTE01.Entities
{
    public class Comorbity
    {
        //PK
        public Guid ID { get; set; } = Guid.NewGuid();
        //FK
        public Guid ClientID {get; set;}
        //navegação do obj client
        public Client? client {get; set;}
        public string Description { get; set; }
        public RiskLevelEnum RiskLevelEnum {get; set;}
        public Comorbity(){ }
        public Comorbity(string description, RiskLevelEnum riskLevelEnum, Guid clientID)
        {
            ClientID = clientID;
            Description = description;
            RiskLevelEnum = riskLevelEnum;
        }
    }
}