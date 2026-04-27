
using APIPEITESTE01.Entities.Enums;

namespace APIPEITESTE01.Entities
{
    public class WorkOutDay
    {
        public Guid ID{get; set;} = Guid.NewGuid();
        public Guid WorkOutID { get; set; }
        public WorkOut? workOut{get; set;}
        //Dia A, Dia B, dia C
        public string DayLabel{get; set;}
        //superiores ,inferiores, core e pernas
        public BodyParts bodyParts {get; set;}
        public List<Excercices> excercices {get;set;} = new();
    }
}