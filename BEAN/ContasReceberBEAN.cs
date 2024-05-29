using System;
using System.Collections.Generic;
using System.Text;

namespace BEAN
{
    public class ContasReceberBEAN : DmlBEAN
    {
        private int codigo;
        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private int contacontabil;
        public int ContaContabil
        {
            get { return contacontabil; }
            set { contacontabil = value; }
        }

        private int formarecebimento;
        public int FormaRecebimento
        {
            get { return formarecebimento; }
            set { formarecebimento = value; }
        }

        private int? mes;
        public int? Mes
        {
            get { return mes; }
            set { mes = value; }
        }

        private int? codigoorigem;
        public int? CodigoOrigem
        {
            get { return codigoorigem; }
            set { codigoorigem = value; }
        }


        private int? ano;
        public int? Ano
        {
            get { return ano; }
            set { ano = value; }
        }

        private double valor;
        public double Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        private int? tipocobranca;
        public int? TipoCobranca
        {
            get { return tipocobranca; }
            set { tipocobranca = value; }
        }

        private int? centrocusto;
        public int? CentroCusto
        {
            get { return centrocusto; }
            set { centrocusto = value; }
        }


        private double valorpago;
        public double ValorPago
        {
            get { return valorpago; }
            set { valorpago = value; }
        }

        private int? cliente;
        public int? Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        private string datavencimento;
        public string DataVencimento
        {
            get { return datavencimento; }
            set { datavencimento = value; }
        }

        private string datarecebimento;
        public string DataRecebimento
        {
            get { return datarecebimento; }
            set { datarecebimento = value; }
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

        private int codcoligada;
        public int CodColigada
        {
            get { return codcoligada; }
            set { codcoligada = value; }
        }

        private int codpes;
        public int CodPes
        {
            get { return codpes; }
            set { codpes = value; }
        }

        private int codbanco;
        public int CodBanco
        {
            get { return codbanco; }
            set { codbanco = value; }
        }

        private int contabancaria;
        public int ContaBancaria
        {
            get { return contabancaria; }
            set { contabancaria = value; }
        }

        private int codfrmpgt;
        public int CodFrmPgt
        {
            get { return codfrmpgt; }
            set { codfrmpgt = value; }
        }

        private int codcentrocusto;
        public int CodCentroCusto
        {
            get { return codcentrocusto; }
            set { codcentrocusto = value; }
        }

        private int codtipdoc;
        public int CodTipDoc
        {
            get { return codtipdoc; }
            set { codtipdoc = value; }
        }

        private string notafiscal;
        public string NotaFiscal
        {
            get { return notafiscal; }
            set { notafiscal = value; }
        }

        private double valornf;
        public double ValorNf
        {
            get { return valornf; }
            set { valornf = value; }
        }

        private string numorcamento;
        public string NumOrcamento
        {
            get { return numorcamento; }
            set { numorcamento = value; }
        }

        private string dtemissao;
        public string DtEmissao
        {
            get { return dtemissao; }
            set { dtemissao = value; }
        }

        private string dtVencimento;
        public string DtVencimento
        {
            get { return dtVencimento; }
            set { dtVencimento = value; }
        }

        private string dtpgto;
        public string DtPgto
        {
            get { return dtpgto; }
            set { dtpgto = value; }
        }

        private string dtvenda;
        public string dtVenda
        {
            get { return dtvenda; }
            set { dtvenda = value; }
        }

        private int parcelade;
        public int ParcelaDe
        {
            get { return parcelade; }
            set { parcelade = value; }
        }

        private int parcelaate;
        public int ParcelaAte
        {
            get { return parcelaate; }
            set { parcelaate = value; }
        }

        private string descricao;
        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        private double valorparcela;
        public double ValorParcela
        {
            get { return valorparcela; }
            set { valorparcela = value; }
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

        private double valorquitado;
        public double ValorQuitado
        {
            get { return valorquitado; }
            set { valorquitado = value; }
        }

        private string recebido;
        public string Recebido
        {
            get { return recebido; }
            set { recebido = value; }
        }

        private int boleto;
        public int Boleto
        {
            get { return boleto; }
            set { boleto = value; }
        }

        private int tarifa;
        public int Tarifa
        {
            get { return tarifa; }
            set { tarifa = value; }
        }

        private string observacao;
        public string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private int condicao;
        public int Condicao
        {
            get { return condicao; }
            set { condicao = value; }
        }

        private double multa;
        public double Multa
        {
            get { return multa; }
            set { multa = value; }
        }

        private double jurosboleto;
        public double JurosBoleto //ESSE JUROS SERVE PARA AS INSTRUCOES DO BOLETO
        {
            get { return jurosboleto; }
            set { jurosboleto = value; }
        }
    }
}
