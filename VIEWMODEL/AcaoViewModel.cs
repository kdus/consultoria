using System;
using System.Collections.Generic;
using System.Text;


namespace MODELS
{
    public class AcaoViewModel : DmlViewModel
    {
        public String BuscaCodigo { get; set; }
        public String BuscaNumeracao { get; set; }
        public String BuscaAnoNumeracao { get; set; }
        public String Numeracao { get; set; }
        public String BuscaRequerente { get; set; }
        public String RequerenteNome { get; set; }
        public String RequerenteCpf { get; set; }
        public String BuscaAssessoria { get; set; }
        public String DescricaoContaReceber { get; set; }
        public int? Requerente { get; set; }
        public int? ResponsavelFinanceiro { get; set; }
        public String ResponsavelFinanceiroNome { get; set; }
        public int? Assessoria { get; set; }
        public int? AssessoriaParaConfirmar { get; set; }
        public String AssessoriaNome { get; set; }
        public String BuscaSemAssessoria { get; set; }
        public string BuscaMatrizFilial { get; set; }
        public int? Favorecido { get; set; }
        public int? Cliente { get; set; }
        public String AssessoriaFinanceiroNome { get; set; }
        public int? Responsavel { get; set; }
        public String ResponsavelNome { get; set; }
        public double Valor { get; set; }
        public double ValorReal { get; set; }
        public int? ParcelaDeContasPagar { get; set; }
        public int? ParcelaAteContasPagar { get; set; }
        public double? ValorContasPagar { get; set; }
        public DateTime? VencimentoContasPagar { get; set; }
        public int? ParcelaDeContasReceber { get; set; }
        public int? ParcelaAteContasReceber { get; set; }
        public int? TipoRecebimentoPagamento { get; set; }
        public int? TipoRecebimentoPagamentoRec{ get; set; }
        public double ValorContasReceber { get; set; }
        public double ValorRecebidoContasReceber { get; set; }
        public DateTime? VencimentoContasReceber { get; set; }
        public String Sucumbencia { get; set; }

        public DateTime? BuscaDataDe { get; set; }
        public DateTime? BuscaDataAte { get; set; }

        public String TipoDescricao { get; set; }

        public String BuscaTipoAcao { get; set; }

        public string BuscaPorUsuarioPermitido { get; set; }

        public String Principal { get; set; }

        public string  BuscaEhRecebido { get; set; }

        public string  BuscaNaoRecebido { get; set; }

        public int? ContaBancaria { get; set; }

        public List<PessoaViewModel> Requerentes { get; set; }
        public List<PessoaViewModel> ResponsavelFinanceiros { get; set; }
        public List<PessoaViewModel> Assessorias { get; set; }
        
        public List<PessoaViewModel> Responsavels { get; set; }
        public List<PessoaViewModel> Tipos { get; set; }
        public List<EmpresaViewModel> Empresas { get; set; }
        public List<OrigemCadastroViewModel> TiposRecebimentoPagamento { get; set; }//usei a model origem por te o campo codigo e nome
        public List<ContaBancariaViewModel> ContasBancaria { get; set; }

    }
}
