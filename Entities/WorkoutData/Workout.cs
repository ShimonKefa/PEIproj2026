using APIPEITESTE01.Entities.Enums;

namespace APIPEITESTE01.Entities
{
    public class WorkOut
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public Guid ClientID {get; set;}
        public Client? client {get; set;}
        public ExperienceLevel experienceLevel {get; set;}
        public IntencityWorkOut intencityWorkOut {get; set;}
        public string GeneralNotes { get; set;}
        public int DaysPerWeek {get; set;}
        public List<WorkOutDay> Days {get; set;} = new();
        public WorkOut(){ }
        public WorkOut(Guid iD, ExperienceLevel experienceLevel, IntencityWorkOut intencityWorkOut, String GeneralNotes, int DaysPerWeek)
        {
            ID = iD;
            this.DaysPerWeek = DaysPerWeek;
            this.GeneralNotes = GeneralNotes;
            this.experienceLevel = experienceLevel;
            this.intencityWorkOut = intencityWorkOut;            
        }
    }
}