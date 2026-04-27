using APIPEITESTE01.Entities.Enums;

namespace APIPEITESTE01.Entities
{
    public class Excercices
    {
        public Guid ID {get; set;} = Guid.NewGuid();
        public Guid WorkOutDayID {get; set;}
        public WorkOutDay? workOutDay {get; set;}


        //será preenchido pela IA
        public string Name {get; set;}
        public string MuscleGroup { get; set;} //grupo muscular
        //series
        public int Sets {get; set;}
        //repetções
        public string Reps {get; set;}
        public int RestTime{get; set;}
       public string Notes{get; set;} 

    }
}