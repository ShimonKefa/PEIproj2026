using APIPEITESTE01.Entities.Enums;
using Microsoft.Data.Sqlite;

namespace APIPEITESTE01.Entities
{
    public class Anamnese
    {
        //PK
        public Guid ID { get; set; } = Guid.NewGuid();
        //FK
        public Guid ClientID {get; set;}
        //obj de acesso
        public Client? client {get; set;}
        //objetivo do treino
        public ObjectiveTypeEnum objectiveTypeEnum {get; set;}
        public Anamnese(){ }
        public Anamnese(Guid clientID, ObjectiveTypeEnum objectiveTypeEnum)
        {     
            ClientID = clientID;
            this.objectiveTypeEnum = objectiveTypeEnum;
        }
    }
}