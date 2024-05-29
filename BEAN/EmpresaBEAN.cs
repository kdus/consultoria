using System;
using System.Collections.Generic;
using System.Text;

namespace BEAN
{
    public class EmpresaBEAN : DmlBEAN
    {

        private int codigo;
        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string codigoservicorps;
        public string CodigoServicoRps
        {
            get { return codigoservicorps; }
            set { codigoservicorps = value; }
        }
        private string regime;
        public string Regime
        {
            get { return regime; }
            set { regime = value; }
        }



        private String nome;
        public String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private String discricaorecibo;
        public String DiscricaoRecibo
        {
            get { return discricaorecibo; }
            set { discricaorecibo = value; }
        }

        private String discricaorps;
        public String DiscricaoRps
        {
            get { return discricaorps; }
            set { discricaorps = value; }
        }

        private String discricaorpsorcamento;
        public String DiscricaoRpsOrcamento
        {
            get { return discricaorpsorcamento; }
            set { discricaorpsorcamento = value; }
        }

        private String descricaorpspedido;
        public String DescricaoRpsPedido
        {
            get { return descricaorpspedido; }
            set { descricaorpspedido = value; }
        }

        private string descricaorpsretencao;
        public string DescricaoRpsRetencao
        {
            get { return descricaorpsretencao; }
            set { descricaorpsretencao = value; }
        }

        private string descricaorpsnf;
        public string DescricaoRpsNf
        {
            get { return descricaorpsnf; }
            set { descricaorpsnf = value; }
        }

        private String smtp;
        public String Smtp
        {
            get { return smtp; }
            set { smtp = value; }
        }

        private String email;
        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        private String email2;
        public String Email2
        {
            get { return email2; }
            set { email2 = value; }
        }

        private String emailsenha;
        public String Emailsenha
        {
            get { return emailsenha; }
            set { emailsenha = value; }
        }

        private String ddd;
        public String Ddd
        {
            get { return ddd; }
            set { ddd = value; }
        }

        private String telefone;
        public String Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }

        private String faturarpor;
        public String FaturarPor
        {
            get { return faturarpor; }
            set { faturarpor = value; }
        }

        private String fantasia;
        public String Fantasia
        {
            get { return fantasia; }
            set { fantasia = value; }
        }

        private String cnpj;
        public String Cnpj
        {
            get { return cnpj; }
            set { cnpj = value; }
        }

        private String ie;
        public String Ie
        {
            get { return ie; }
            set { ie = value; }
        }

        private String im;
        public String Im
        {
            get { return im; }
            set { im = value; }
        }

        private String endereco;
        public String Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }

        private String numero;
        public String Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        private String complemento;
        public String Complemento
        {
            get { return complemento; }
            set { complemento = value; }
        }

        private String bairro;
        public String Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        private String cidade;
        public String Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }

        private String cep;
        public String Cep
        {
            get { return cep; }
            set { cep = value; }
        }

        private String uf;
        public String Uf
        {
            get { return uf; }
            set { uf = value; }
        }

        private string contru;
        public string Contru
        {
            get { return contru; }
            set { contru = value; }
        }

        private String cnae;
        public String Cnae
        {
            get { return cnae; }
            set { cnae = value; }
        }

        private String crea;
        public String Crea
        {
            get { return crea; }
            set { crea = value; }
        }

        private String seciesp;
        public String Seciesp
        {
            get { return seciesp; }
            set { seciesp = value; }
        }

        private String seguronumero;
        public String SeguroNumero
        {
            get { return seguronumero; }
            set { seguronumero = value; }
        }

        private String seguradora;
        public String Seguradora
        {
            get { return seguradora; }
            set { seguradora = value; }
        }

        private Decimal valor;
        public Decimal Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        private String obsnocontrato;
        public String ObsNoContrato
        {
            get { return obsnocontrato; }
            set { obsnocontrato = value; }
        }

        private String validacnpj;
        public String ValidaCnpj
        {
            get { return validacnpj; }
            set { validacnpj = value; }
        }

        private String relatorio;
        public String Relatorio
        {
            get { return relatorio; }
            set { relatorio = value; }
        }


        private String foto;
        public String Foto
        {
            get { return foto; }
            set { foto = value; }
        }

        private String contratocaminho;
        public String ContratoCaminho
        {
            get { return contratocaminho; }
            set { contratocaminho = value; }
        }

        private String casadefsol;
        public String CasaDefSol
        {
            get { return casadefsol; }
            set { casadefsol = value; }
        }

        private String trabalhacep;
        public String TrabalhaCep
        {
            get { return trabalhacep; }
            set { trabalhacep = value; }
        }

        private String lembretechamado;
        public String LembreteChamado
        {
            get { return lembretechamado; }
            set { lembretechamado = value; }
        }

        private String caminhocep;
        public String CaminhoCep
        {
            get { return caminhocep; }
            set { caminhocep = value; }
        }

        private String caminhoimport;
        public String CaminhoImport
        {
            get { return caminhoimport; }
            set { caminhoimport = value; }
        }

        private String produtopcmob;
        public String ProdutoPcMob
        {
            get { return produtopcmob; }
            set { produtopcmob = value; }
        }

        private String pedidoautorizacaogeranf;
        public String PedidoAutorizacaoGeraNf
        {
            get { return pedidoautorizacaogeranf; }
            set { pedidoautorizacaogeranf = value; }
        }

        private String orcamentogeranfe;
        public String OrcamentoGeranFe
        {
            get { return orcamentogeranfe; }
            set { orcamentogeranfe = value; }
        }

        private String geranfunicasubtributaria;
        public String GeraNfUnicaSubTributaria
        {
            get { return geranfunicasubtributaria; }
            set { geranfunicasubtributaria = value; }
        }

        private String infonfcomsubtributaria;
        public String InfoNfComSubTributaria
        {
            get { return infonfcomsubtributaria; }
            set { infonfcomsubtributaria = value; }
        }

        private String infonfsemsubtributaria;
        public String InfoNfSemSubTributaria
        {
            get { return infonfsemsubtributaria; }
            set { infonfsemsubtributaria = value; }
        }

        private String caminhorps;
        public String CaminhoRps
        {
            get { return caminhorps; }
            set { caminhorps = value; }
        }

        private int? fluxocaixacontabancaria;
        public int? FluxoCaixaContaBancaria
        {
            get { return fluxocaixacontabancaria; }
            set { fluxocaixacontabancaria = value; }
        }

        //Indica se trabalha com tipo de faturamento
        private String tipofaturamento;
        public String TipoFaturamento
        {
            get { return tipofaturamento; }
            set { tipofaturamento = value; }
        }

        private int? condicaorecebimento;
        public int? CondicaoRecebimento
        {
            get { return condicaorecebimento; }
            set { condicaorecebimento = value; }
        }

        private String textorecibo;
        public String TextoRecibo
        {
            get { return textorecibo; }
            set { textorecibo = value; }
        }


        private String apoliceseguro;
        public String ApoliceSeguro
        {
            get { return apoliceseguro; }
            set { apoliceseguro = value; }
        }

        private double? aliquotaiss;
        public double? AliquotaIss
        {
            get { return aliquotaiss; }
            set { aliquotaiss = value; }
        }

        private double? aliquotapis;
        public double? AliquotaPis
        {
            get { return aliquotapis; }
            set { aliquotapis = value; }
        }

        private double? aliquotaipi;
        public double? AliquotaIpi
        {
            get { return aliquotaipi; }
            set { aliquotaipi = value; }
        }

        private double? aliquotacofins;
        public double? AliquotaCofins
        {
            get { return aliquotacofins; }
            set { aliquotacofins = value; }
        }

        private double? valorlimitecompra;
        public double? ValorLimiteCompra
        {
            get { return valorlimitecompra; }
            set { valorlimitecompra = value; }
        }

    }

}
