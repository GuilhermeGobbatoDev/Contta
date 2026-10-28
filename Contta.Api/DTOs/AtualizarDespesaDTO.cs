namespace Contta.Api.DTOs
{
    public class AtualizarDespesaDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public DateTime Data { get; set; }
    
    }
}
