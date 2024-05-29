using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BEAN;
using BLL;
using MODELS;
using System.IO;
using Comum;
using System.Data;

namespace Aplicacao.Controllers
{
    public class RelatorioGerencialController : Controller
    {
        Comum.Funcionalidades Funcionalidade = new Funcionalidades();

        // GET: RelatorioGerencial

        private void PopulaCombos(ResumoViewModel model)
        {
            model.Pessoa = FuncionalidadeBLL.PopulaCombos<PessoaViewModel>("PESSOA", "CODPES", "NOMPES", "NOMPES", " AND EHASSESSORIA = 'S'").ToList();
            model.Meses = FuncionalidadeBLL.PopulaCombos<MesViewModel>("MES", "CODMES", "DSCMESTRES", string.Empty, string.Empty).ToList();
            model.Ano = FuncionalidadeBLL.PopulaCombos<AnoViewModel>("ANO", "ANO", "ANO", "ANO DESC", string.Empty).ToList();
            model.Empresas = FuncionalidadeBLL.PopulaCombos<EmpresaViewModel>("EMPRESA", "CODEMP", "NOMEMP", string.Empty, string.Empty).ToList();
        }

        public ActionResult Index()
        {
            //GerarPdf();
            ResumoViewModel model = new ResumoViewModel();

            PopulaCombos(model);
            return View("~/Views/Financeiro/RelatorioGerencial/Index.cshtml", model);
        }

        public ActionResult Detalhado()
        {
            ResumoViewModel model = new ResumoViewModel();

            model.Empresas = FuncionalidadeBLL.PopulaCombos<EmpresaViewModel>("EMPRESA", "CODEMP", "NOMEMP", string.Empty, string.Empty).ToList();

            return View("~/Views/Financeiro/RelatorioGerencial/Detalhado.cshtml", model);
        }

        [HttpPost]
        public PartialViewResult Buscar(ResumoViewModel pModel)
        {
            ResumoListaViewModel model = new ResumoListaViewModel();
            RelatorioGerencialBLL objBll = new RelatorioGerencialBLL();

            

            if ((((UsuarioBEAN)Session["session_Usuario"]).Nivel).Equals("A"))
                pModel.BuscaPorUsuarioPermitido = (((UsuarioBEAN)Session["session_Usuario"]).Codigo).ToString();

            //if (pModel.BuscaMatrizFilial != null)
            //    model.FiltrosUtilizados += "<br /> Empresa: " + pModel.BuscaMatrizFilial;

            if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                model.FiltrosUtilizados += "<br /> Venctos - Período: " + Funcionalidade.FormatoData(pModel.BuscaDataVencimentoDe.ToString()) + " à " + Funcionalidade.FormatoData(pModel.BuscaDataVencimentoAte.ToString());

            if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte != null)
                model.FiltrosUtilizados += "<br />Pagtos - Período: " + Funcionalidade.FormatoData(pModel.BuscaDataPagamentoDe.ToString()) + " à " + Funcionalidade.FormatoData(pModel.BuscaDataPagamentoAte.ToString());
            
            if (pModel.BuscaAcaoTipo != null)
                model.FiltrosUtilizados += "<br /> Tipo: " + pModel.BuscaAcaoTipo;

            if (pModel.BuscaFavorecido != null)
                model.FiltrosUtilizados += "<br /> Favorecido: " + pModel.BuscaFavorecido;

            if (pModel.BuscaProcesso != null)
                model.FiltrosUtilizados += "<br /> Processo: " + pModel.BuscaProcesso;

            if (pModel.BuscaAssessoria != null)
                model.FiltrosUtilizados += "<br /> Assessoria: " + pModel.BuscaAssessoria;

            
            model.BuscaDataVencimentoDe = pModel.BuscaDataVencimentoDe;

            model.Contas = objBll.PopulaGrid(pModel);

            if (model.Contas.Count > 0)
            {
                if (pModel.BuscaMatrizFilial != null && pModel.BuscaMatrizFilial != "0")
                    model.FiltrosUtilizados += "<br /> Empresa: " + model.Contas[0].EmpresaNome;
            }


            if ((((UsuarioBEAN)Session["session_Usuario"]).Nivel).Equals("S"))
                return PartialView("~/Views/Financeiro/RelatorioGerencial/Grid.cshtml", model);
            else
                return PartialView("~/Views/Financeiro/RelatorioGerencial/GridAssessoria.cshtml", model);
        }

        [HttpPost]
        public PartialViewResult BuscarDetalhado(ResumoViewModel pModel)
        {
            ResumoListaViewModel model = new ResumoListaViewModel();
            RelatorioGerencialBLL objBll = new RelatorioGerencialBLL();


            if ((((UsuarioBEAN)Session["session_Usuario"]).Nivel).Equals("A"))
                pModel.BuscaPorUsuarioPermitido = (((UsuarioBEAN)Session["session_Usuario"]).Codigo).ToString();

            model.Contas = objBll.PopulaGridDetalhado(pModel);

            if (model.Contas.Count > 0)
                if (pModel.BuscaMatrizFilial != null && pModel.BuscaMatrizFilial != "0")
                    model.FiltrosUtilizados += "<br /> Empresa: " + model.Contas[0].EmpresaNome;

            return PartialView("~/Views/Financeiro/RelatorioGerencial/Grid.cshtml", model);
        }

        [HttpPost]
        public PartialViewResult BuscarDetalhadoII(ResumoViewModel pModel)
        {
            ResumoListaViewModel model = new ResumoListaViewModel();
            RelatorioGerencialBLL objBll = new RelatorioGerencialBLL();


            if ((((UsuarioBEAN)Session["session_Usuario"]).Nivel).Equals("A"))
                pModel.BuscaPorUsuarioPermitido = (((UsuarioBEAN)Session["session_Usuario"]).Codigo).ToString();

            model.Contas = objBll.PopulaGridDetalhadoII(pModel);

            return PartialView("~/Views/Financeiro/RelatorioGerencial/GridDetalhadoII.cshtml", model);
        }

        [HttpPost]
        public void GerarPdf()
        {
            string vUrl = System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
            //D:\web\localuser\nome-do-site\www
            DataTable dtDados = new DataTable();

            Aplicacao.Models.Relatorio rltPdf = new Models.Relatorio();
            rltPdf.vOrigem = "Boleto";
            rltPdf.vNomeRlt = "rptGerencialAgrupado";
            rltPdf.vDisplayName = "Relatorio Gerencial - Agrupado";

            if (!vUrl.Contains("localhost"))
                rltPdf.vNomeArquivo = @"D:\web\localuser\systems-on\www\eaj\Teste.pdf";
            else
                rltPdf.vNomeArquivo = "C:\\SisAdvoca\\Teste.pdf";

            rltPdf.dtOrigem = dtDados;
            rltPdf.vContemImagem = false;
            rltPdf.vExportaPdf = true;
            rltPdf.ExportarPdf();
        }

        [HttpPost]
        public JsonResult GravarContasPagar(ResumoViewModel acao)
        {         

            ContasPagarBLL objBll = new ContasPagarBLL();
            ResumoViewModel model = new ResumoViewModel();
            ContasPagarViewModel modelCp = new ContasPagarViewModel();

            modelCp.CodigoOrigem = acao.Codigo;
            modelCp.OrigemCadastro = 3; //RELATORIO GERENCIAL
            modelCp.ParcelaX = 1;
            modelCp.ParcelaY = 1;
            modelCp.Valor = acao.ValorContasPagar;
            modelCp.Vencimento = acao.VencimentoContasPagar;
            modelCp.MesReferencia = acao.MesReferencia;
            modelCp.AnoReferencia = acao.AnoReferencia;

            modelCp.Favorecido = acao.Favorecido;    
            
            modelCp.CodigoUsuario = (((UsuarioBEAN)Session["session_Usuario"]).Codigo);

            model.Codigo = Convert.ToInt32(objBll.Gravar(modelCp));
            //model.Codigo = 999;

            return Json(model);

        }
    }
}