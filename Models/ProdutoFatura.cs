namespace GestaoEmpresa.Models
{
    public class ProdutoFatura
    {
        public int Id { get; set; }
        public int FaturaId { get; set; }
        public string ReferenciaArtigo { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal ValorComIVA { get; set; }
        public decimal Desconto { get; set; }
        public decimal IVA { get; set; }
    }
}