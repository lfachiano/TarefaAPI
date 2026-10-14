namespace TarefaAPI.DTO
{
    public class TarefaResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao {  get; set; } = string.Empty;
        public string Status {  get; set; } = string.Empty;
    }
}
