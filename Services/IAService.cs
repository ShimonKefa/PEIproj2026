using Google.GenAI;
using Google.GenAI.Types;
using APIPEITESTE01.Entities;
using APIPEITESTE01.Enviroment;
using System.Text.Json;
using System.Text;
using APIPEITESTE01.Entities.Enums;

namespace APIPEITESTE01.Services
{
    public class AIService
    {
       private readonly SelectServices _select;
       private readonly InsertServices _insert;
         public AIService()
        {
            _select = new SelectServices();
            _insert = new InsertServices();
        }
        private string BuildPrompt(Entities.Client client, int daysPerWeek, WorkOut workOut)
        {
            //calcula a idade
            var age = DateTime.Now.Year - client.BirthDate.Year;
            //Monta uma lista das comorbidades
            var comorbidadesTexto = client.comorbities.Any() //o Any() verifica se existe pelo menos 1 item
            ? string.Join(", ", client.comorbities.Select(c => //apartir daqui é utilizado um op ternário para atender uma condição/ retorno do any() retorno ? true : false
            $"{c.Description} (risco: {c.RiskLevelEnum})"))
            : "nenhuma";

            //pega o objetivo da anamnese
            var objetivo = client.Anamneses.LastOrDefault()?.objectiveTypeEnum.ToString()
            ?? "não definido";

            //traduz os enumeradores para a IA entender.
            //O switch tem a mesma função normal, ele avalia uma condição e retorna um valor. porém em um formato diferente
            var nivel = workOut.experienceLevel switch
            {
                ExperienceLevel.EXPERIENTE => "Experiente",
                ExperienceLevel.INTERMEDIARIO => "Intermediário",
                ExperienceLevel.INICIANTE => "Iniciante",
                _ => "iniciante"
            };

            var intensidade = workOut.intencityWorkOut switch
            {
                IntencityWorkOut.ALTO => "Alta",
                IntencityWorkOut.MEDIO => "Media",
                IntencityWorkOut.BAIXO => "Baixa",
                _ => "Baixa"
            };
            //Essa '?' tem a função de tratar qualquer entrada nula nesse cenário.
            var grupoMuscular = workOut.Days.FirstOrDefault()?.bodyParts switch
            {
                BodyParts.SUPERIORES => "membros superiores (peito, costas, ombros e braços)",
                BodyParts.INFERIORES => "membros inferiores (quadríceps, posterior, glúteo e panturrilha)",
                BodyParts.PERNAS     => "pernas",
                BodyParts.CORE       => "core e abdômen",
                _ => "corpo completo"
            };

                    return $@"
                    Você é um personal trainer especialista.
                    Gere um treino personalizado em JSON puro, sem markdown, sem explicações.

                    Dados do cliente:
                    - Idade: {age} anos
                    - Altura: {client.Height}m
                    - Peso: {client.Weight}kg
                    - Percentual de gordura: {client.FatPercentage}%
                    - Biotipo: {client.BodyType}
                    - Comorbidades: {comorbidadesTexto}
                    - Objetivo: {objetivo}

                    Parâmetros definidos pelo cliente:
                    - Nível de experiência: {nivel}
                    - Intensidade: {intensidade}
                    - Foco muscular: {grupoMuscular}
                    - Dias de treino por semana: {daysPerWeek}

                    Regras:
                    - Gere EXATAMENTE {daysPerWeek} dias de treino
                    - Foque nos grupos musculares de {grupoMuscular}
                    - Respeite o nível {nivel} na escolha e volume dos exercícios
                    - Respeite a intensidade {intensidade} nas séries, repetições e descanso
                    - Adapte ou substitua exercícios que possam agravar as comorbidades
                    - Escreva as observações em português

                    Retorne APENAS este JSON:
                    {{
                    ""GeneralNotes"": ""observações gerais sobre o treino considerando as comorbidades"",
                    ""Days"": [
                        {{
                        ""DayLabel"": ""Dia A"",
                        ""Exercises"": [
                            {{
                            ""Name"": ""nome do exercício"",
                            ""MuscleGroup"": ""músculo trabalhado"",
                            ""Sets"": 3,
                            ""Reps"": ""10-12"",
                            ""RestTime"": 60,
                            ""Notes"": ""observação ou deixe vazio""
                            }}
                        ]
                        }}
                    ]
                    }}
                    ";
        }
        
        
    
        public async Task<WorkOut> AIRequest(Guid clientID, WorkOut workOut, int daysPerWeek)
        {
            //esse client dessa classe pertence ao Gemini, não pensei nisso quando criei a classe client
            Google.GenAI.Client client = new Google.GenAI.Client();
            var clientGen = _select.GetClientbyID(clientID);
            var prompt = BuildPrompt(clientGen, daysPerWeek, workOut);
            var response = await client.Models.GenerateContentAsync(
                model: "gemini-3-flash-preview",
                contents: prompt
            );  

            var json = response.Text
            .Replace("```json", "")
            .Replace("```", "")
            .Trim();

            var dto = JsonSerializer.Deserialize<WorkOutDTO>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            workOut.ClientID = clientID;
            workOut.DaysPerWeek = daysPerWeek;
            workOut.GeneralNotes = dto.GeneralNotes;
            workOut.Days = dto.Days.Select(d => new WorkOutDay
            {
                DayLabel = d.DayLabel,
                bodyParts = workOut.Days.FirstOrDefault()?.bodyParts ?? BodyParts.SUPERIORES,
                excercices = d.Exercises.Select(e => new Excercices
                {
                    Name = e.Name,
                    MuscleGroup = e.MuscleGroup,
                    Sets = e.Sets,
                    Reps = e.Reps,
                    RestTime = e.RestTime,
                    Notes = e.Notes
                }).ToList()
            }).ToList();
            return _insert.InsertWorkOut(workOut);
        }
    }
}