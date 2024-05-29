using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class HomeViewModel : DmlViewModel
    {
        public double BuscaAno { get; set; }
        public double BuscaMes { get; set; }

        public double Totalfaturamento_peca { get; set; }
        public double Totalticketmedio { get; set; }
        public double Totalfaturamento_maoobra { get; set; }
		public double Totalqtd_veiculo { get; set; }
		public double PorIndicador { get; set; }

		public double Porticketmedio { get; set; }
		public double PorFatServico { get; set; }
		public double PorFatPeca { get; set; }

		public double TOTALFATURAMENTO { get; set; }
		public double PorFat { get; set; }

		public List<EmpresaViewModel> Empresas { get; set; }
        public List<MesViewModel> Meses { get; set; }
        public List<AnoViewModel> Ano { get; set; }
		public List<PessoaViewModel> Clientes { get; set; }
	}
}
