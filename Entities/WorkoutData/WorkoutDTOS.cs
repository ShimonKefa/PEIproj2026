namespace APIPEITESTE01.Entities
{
    public class WorkOutDTO
{
    public string GeneralNotes { get; set; }
    public List<WorkOutDayDTO> Days { get; set; }
}

public class WorkOutDayDTO
{
    public string DayLabel { get; set; }
    public List<ExerciseDTO> Exercises { get; set; }
}

public class ExerciseDTO
{
    public string Name { get; set; }
    public string MuscleGroup { get; set; }
    public int Sets { get; set; }
    public string Reps { get; set; }
    public int RestTime { get; set; }
    public string Notes { get; set; }
}
}