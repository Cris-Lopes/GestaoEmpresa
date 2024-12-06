namespace GestaoEmpresa.Models
{
    public class Fatura
    {
        public int Id { get; set; }
        public int FornecedorId { get; set; }
        public DateTime DataFatura { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorTotal { get; set; }
    }
}