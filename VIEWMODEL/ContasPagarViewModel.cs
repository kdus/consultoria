using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MODELS
{
    public class ContasPagarViewModel : DmlViewModel
    {

        public ContasPagarViewModel()
        {
        }
        
        public int? Documento                { get; set; }
        public string MaterDia               { get; set; }
        public string Cheque                 { get; set; }
        public int? Favorecido               { get; set; }
        public string BuscaProcesso { get; set; }
        public string BuscaFixa { get; set; }
        public string BuscaProcessoCodigo { get; set; }
        public string BuscaFavorecido { get; set; }
        public string BuscaNegarAcaoProcesso { get; set; }
        public string BuscaPagoSimNao { get; set; }
        public string BuscaDescricao { get; set; }
        public string BuscaEhSucumbencia { get; set; }
        public string BuscaMatrizFilial { get; set; }
        public string BuscaContaBancaria { get; set; }
        public string BuscaAssessoria { get; set; }

        public string FavorecidoNome { get; set; }
        public string DscDocumento           { get; set; }
        public int? Fornecedor               { get; set; }
        public int? ParcelaX                  { get; set; }
        public int? ParcelaY                  { get; set; }
        public String Pedido                 { get; set; }
        public String Descricao              { get; set; }
        public String Fixa                   { get; set; }
        public string Pago              { get; set; }
        public int? ContaContabil            { get; set; }
        public int? FormaPagamentoRecebimento           { get; set; }
        public int? TipoRecebimentoPagamento { get; set; }

        public int? CondicaoPagamento        { get; set; }
        public String ContaMae               { get; set; }
        public int TipoCobranca              { get; set; }
        public double? Valor                  { get; set; }
        public double Desconto               { get; set; }
        public double Juros                  { get; set; }
        public decimal Taxa                  { get; set; }
        public double? ValorPago              { get; set; }
        public DateTime? Vencimento         { get; set; }

        public DateTime? BuscaDataVencimentoDe { get; set; }
        public DateTime? BuscaDataVencimentoAte { get; set; }


        public DateTime? DataPagamento          { get; set; }
        public DateTime? BuscaDataPagamentoDe { get; set; }
        public DateTime? BuscaDataPagamentoAte { get; set; }
        public DateTime? DataRecebimento      { get; set; }
        public int? ContaBancaria             { get; set; }
        public string ContaBancariaNome { get; set; }        
        public string Observacao             { get; set; }
        public int? CentroCusto              { get; set; }
        public string Nominal                { get; set; }
        public string Status                 { get; set; }
        public int? ContaPai                 { get; set; }
        public int? MesReferencia            { get; set; }
        public int? AnoReferencia            { get; set; }
        public int TipoCobrancaRecebimento   { get; set; }
        public int? Banco                    { get; set; }
        public string Agencia                { get; set; }
        public string Conta                  { get; set; }
        public string AutorizaPagamento      { get; set; }

        public string Sucumbencia { get; set; }

        public string BuscaAcaoTipo { get; set; }
        public string BuscaPorUsuarioPermitido { get; set; }
        public string BuscaAnoVencimento { get; set; }
        public string BuscaMesVencimento { get; set; }
        public string BuscaAnoPagamento { get; set; }
        public string BuscaMesPagamento { get; set; }

        public string RequerenteNome { get; set; }
        
        public List<MesViewModel> Meses { get; set; }
        public List<AnoViewModel> Ano { get; set; }
        public List<PessoaViewModel> Pessoa { get; set; }
        public List<ContaBancariaViewModel> ContasBancaria { get; set; }
        public List<FormaPagamentoRecebimentoViewModel> Formas { get; set; }
        public List<OrigemCadastroViewModel> Origens { get; set; }
        public List<EmpresaViewModel> Empresas { get; set; }

        public List<TipoCobrancaRecebimentoViewModel> TipoCobrancaRecebimentos { get; set; }
        public List<CentroCustoViewModel> CentroCustos { get; set; }
        public List<ContaContabilViewModel> ContaContabeis { get; set; }
        public List<OrigemCadastroViewModel> TiposRecebimentoPagamento { get; set; }//usei a model origem por te o campo codigo e nome

    }
}
