using System;
using System.Collections.Generic;
using System.Text;

namespace BEAN
{
    public  class ContasPagarBEAN : DmlBEAN
    {
        private int codigo;
        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private int? documento;
        public int? Documento
        {
            get { return documento; }
            set { documento = value; }
        }

        private string materdia;
        public string MaterDia
        {
            get { return materdia; }
            set { materdia = value; }
        }

        private string cheque;
        public string Cheque
        {
            get { return cheque; }
            set { cheque = value; }
        }

        private string codigoantigo;
        public string CodigoAntigo
        {
            get { return codigoantigo; }
            set { codigoantigo = value; }
        }

        private int? favorecido;
        public int? Favorecido
        {
            get { return favorecido; }
            set { favorecido = value; }
        }

        private string dscdocumento;
        public string DscDocumento
        {
            get { return dscdocumento; }
            set { dscdocumento = value; }
        }

        private int? fornecedor;
        public int? Fornecedor
        {
            get { return fornecedor; }
            set { fornecedor = value; }
        }

        private int parcelax;
        public int ParcelaX
        {
            get { return parcelax; }
            set { parcelax = value; }
        }

        private int parcelay;
        public int ParcelaY
        {
            get { return parcelay; }
            set { parcelay = value; }
        }

        private String pedido;
        public String Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }

        private String descricao;
        public String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        private string origem;
        public string Origem
        {
            get { return origem; }
            set { origem = value; }
        }

        private int? codorigem;
        public int? OrigemCodigo
        {
            get { return codorigem; }
            set { codorigem = value; }
        }

        private String fixa;
        public String Fixa
        {
            get { return fixa; }
            set { fixa = value; }
        }

        private string contapaga;
        public string ContaPaga
        {
            get { return contapaga; }
            set { contapaga = value; }
        }

        private int? contacontabil;
        public int? ContaContabil
        {
            get { return contacontabil; }
            set { contacontabil = value; }
        }

        private int? formapagamento;
        public int? FormaPagamento
        {
            get { return formapagamento; }
            set { formapagamento = value; }
        }

        private int? condicaopagamento;
        public int? CondicaoPagamento
        {
            get { return condicaopagamento; }
            set { condicaopagamento = value; }
        }

        private String contamae;
        public String ContaMae
        {
            get { return contamae; }
            set { contamae = value; }
        }

        private int tipocobranca;
        public int TipoCobranca
        {
            get { return tipocobranca; }
            set { tipocobranca = value; }
        }

        //Esse campo diz respeito ao valor a pagar
        private double valor;
        public double Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        private double desconto;
        public double Desconto
        {
            get { return desconto; }
            set { desconto = value; }
        }

        private double juros;
        public double Juros
        {
            get { return juros; }
            set { juros = value; }
        }

        private decimal taxa;
        public decimal Taxa
        {
            get { return taxa; }
            set { taxa = value; }
        }

        //Valor Pago
        private double valorpago;
        public double ValorPago
        {
            get { return valorpago; }
            set { valorpago = value; }
        }


        private string datavencimento;
        public string DataVencimento
        {
            get { return datavencimento; }
            set { datavencimento = value; }
        }

        private string datapagamento;
        public string DataPagamento
        {
            get { return datapagamento; }
            set { datapagamento = value; }
        }

        private DateTime datarecebimento;
        public DateTime DataRecebimento
        {
            get { return datarecebimento; }
            set { datarecebimento = value; }
        }


        private int contabancaria;
        public int ContaBancaria
        {
            get { return contabancaria; }
            set { contabancaria = value; }
        }

        private int formarecebimento;
        public int FormaRecebimento
        {
            get { return formarecebimento; }
            set { formarecebimento = value; }
        }

        private string observacao;
        public string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        private int? centrocusto;
        public int? CentroCusto
        {
            get { return centrocusto; }
            set { centrocusto = value; }
        }


        private string nominal;
        public string Nominal
        {
            get { return nominal; }
            set { nominal = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private int? contapai;
        public int? ContaPai
        {
            get { return contapai; }
            set { contapai = value; }
        }

        private int? mesreferencia;
        public int? MesReferencia
        {
            get { return mesreferencia; }
            set { mesreferencia = value; }
        }

        private int? anoreferencia;
        public int? AnoReferencia
        {
            get { return anoreferencia; }
            set { anoreferencia = value; }
        }

        private int tipocobrancarecebimento;
        public int TipoCobrancaRecebimento
        {
            get { return tipocobrancarecebimento; }
            set { tipocobrancarecebimento = value; }
        }

        private int? banco;
        public int? Banco
        {
            get { return banco; }
            set { banco = value; }
        }

        private string agencia;
        public string Agencia
        {
            get { return agencia; }
            set { agencia = value; }
        }

        private string conta;
        public string Conta
        {
            get { return conta; }
            set { conta = value; }
        }

        private string autorizapagamento;
        public string AutorizaPagamento
        {
            get { return autorizapagamento; }
            set { autorizapagamento = value; }
        }

        private int? empreendimento;
        public int? Empreendimento
        {
            get { return empreendimento; }
            set { empreendimento = value; }
        }
    }
}
