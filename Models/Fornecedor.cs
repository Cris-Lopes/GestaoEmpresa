﻿namespace GestaoEmpresa.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Contribuinte { get; set; } = string.Empty;
        public string CondicoesPagamento { get; set; } = string.Empty;
    }
}