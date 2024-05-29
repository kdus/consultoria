using System;
using System.Collections.Generic;
using System.Text;


namespace MODELS
{
    public class AcaoConfirmadaViewModel : DmlViewModel
    {

        public DateTime? ExpiraEm { get; set; }
        public int? Acao { get; set; }
        public String BuscaNumeracao { get; set; }
        public String Numeracao { get; set; }
        public String BuscaRequerente { get; set; }
        public String RequerenteNome { get; set; }
        public String RequerenteCpf { get; set; }
        public String BuscaAssessoria { get; set; }
        public String BuscaConfirmada { get; set; }
        public String BuscaNaoConfirmada { get; set; }
        public int? Requerente { get; set; }
        public int? ResponsavelFinanceiro { get; set; }
        public String ResponsavelFinanceiroNome { get; set; }
        public int? Assessoria { get; set; }
        public int? AssessoriaParaConfirmar { get; set; }
        public String AssessoriaNome { get; set; }
        public String AssessoriaEmail { get; set; }
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
        public String Encerrada { get; set; }

        public List<PessoaViewModel> Requerentes { get; set; }
        public List<PessoaViewModel> ResponsavelFinanceiros { get; set; }
        public List<PessoaViewModel> Assessorias { get; set; }
        
        public List<PessoaViewModel> Responsavels { get; set; }
        public List<PessoaViewModel> Tipos { get; set; }

    }
}
