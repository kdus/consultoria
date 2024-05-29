using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MODELS
{
    public class ResumoViewModel : DmlViewModel
    {

        public ResumoViewModel()
        {
        }

        public int? CodigoAcao { get; set; }
        public DateTime? AcaoData { get; set; }
        public int? Documento                { get; set; }
        public string Assessoria { get; set; }
        public double AssessoriaPagamento { get; set; }
        public string Numeracao { get; set; }
        public string Cliente { get; set; }
        public int? Favorecido               { get; set; }
        public string BuscaProcesso { get; set; }
        public string BuscaAssessoria { get; set; }
        public string BuscaProcessoCodigo { get; set; }
        public string BuscaFavorecido { get; set; }
        public string BuscaNegarAcaoProcesso { get; set; }
        public string BuscaMatrizFilial { get; set; }
        public string BuscaPagoSimNao { get; set; }
        public string BuscaDescricao { get; set; }
        public string BuscaEhSucumbencia { get; set; }
        public string FavorecidoNome { get; set; }
        public string DscDocumento           { get; set; }
        public int? Fornecedor               { get; set; }
        public int? ParcelaX                  { get; set; }
        public int? ParcelaY                  { get; set; }
        public String Pedido                 { get; set; }
        public String Descricao              { get; set; }
        public String Fixa                   { get; set; }
        public string Pago              { get; set; }

        public double? ValorContasPagar { get; set; }

        public double? ValorAhPagarReceber { get; set; }
        public double? ValorPagoRecebido { get; set; }


        public DateTime? VencimentoContasPagar { get; set; }

        public DateTime? BuscaDataVencimentoDe { get; set; }
        public DateTime? BuscaDataVencimentoAte { get; set; }


        public DateTime? DataPagamento          { get; set; }
        public DateTime? BuscaDataPagamentoDe { get; set; }
        public DateTime? BuscaDataPagamentoAte { get; set; }
        public DateTime? DataRecebimento      { get; set; }
        public DateTime? DataVencimento       { get; set; }
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
        public double Pagamentos { get; set; }
        public double Recebimento { get; set; }
        public double Saldo { get; set; }
        public string AutorizaPagamento      { get; set; }
        public string Origem { get; set; }

        public double Sucumbencia { get; set; }

        public string BuscaAcaoTipo { get; set; }
        public string BuscaPorUsuarioPermitido { get; set; }

        public string RequerenteNome { get; set; }

        public string FiltrosUtilizados { get; set; }

        public double Principal { get; set; }

        public List<MesViewModel> Meses { get; set; }
        public List<AnoViewModel> Ano { get; set; }
        public List<PessoaViewModel> Pessoa { get; set; }
        public List<ContaBancariaViewModel> ContasBancaria { get; set; }
        public List<FormaPagamentoRecebimentoViewModel> Formas { get; set; }
        public List<OrigemCadastroViewModel> Origens { get; set; }
        public List<EmpresaViewModel> Empresas { get; set; }



    }
}
