namespace GestaoEmpresa.Models
{
    public class Artigo
    {
        public int Id { get; set; }
        public string Referencia { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal ValorSemIVA { get; set; }
        public decimal Desconto { get; set; }
        public decimal IVA { get; set; }
        public decimal ValorComIVA => ValorSemIVA * (1 + IVA / 100) * (1 - Desconto / 100);
        public int FornecedorId { get; set; }
        public byte[]? Imagem { get; set; } // Suporte a imagens para os artigos.
    }
}