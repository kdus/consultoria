using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BEAN;
using BLL;
using MODELS;

using Comum;

namespace Aplicacao.Controllers
{
    public class ProcessoController : Controller
    {
        Funcionalidades Funcionalidade = new Funcionalidades();
        // GET: Processo
        public ActionResult Index()
        {
            string vView = UsuarioBLL.Permissao(Session["session_Usuario"], (((UsuarioBEAN)Session["session_Usuario"]).Codigo.ToString()), "4", (((UsuarioBEAN)Session["session_Usuario"]).Nivel));

            if (vView.Length > 0)
                return View(vView);

            AcaoViewModel model = new AcaoViewModel();

            PopulaCombos(model);

            return View("~/Views/Cadastros/Processo/Index.cshtml", model);
        }

        private void PopulaCombos(AcaoViewModel model)
        {
            model.Requerentes = FuncionalidadeBLL.PopulaCombos<PessoaViewModel>("PESSOA", "CODPES", "NOMPES", "NOMPES", " AND EHREQUERENTE = 'S'").ToList();
            model.ResponsavelFinanceiros = FuncionalidadeBLL.PopulaCombos<PessoaViewModel>("PESSOA", "CODPES", "NOMPES", "NOMPES", string.Empty).ToList();
            model.Assessorias = FuncionalidadeBLL.PopulaCombos<PessoaViewModel>("PESSOA", "CODPES", "NOMPES", "NOMPES", " AND EHASSESSORIA = 'S'").ToList();
            model.Responsavels = FuncionalidadeBLL.PopulaCombos<PessoaViewModel>("PESSOA", "CODPES", "NOMPES", "NOMPES", string.Empty).ToList();
            model.Tipos = FuncionalidadeBLL.PopulaCombos<PessoaViewModel>("ACAO_TIPO", "CODACATIP", "DSCACATIP", "DSCACATIP", string.Empty).ToList();
            model.Empresas = FuncionalidadeBLL.PopulaCombos<EmpresaViewModel>("EMPRESA", "CODEMP", "NOMEMP", string.Empty, string.Empty).ToList();
            model.TiposRecebimentoPagamento = FuncionalidadeBLL.PopulaCombos<OrigemCadastroViewModel>("TIPO_RECEBE_PAGA", "CODTIPRECPAG", "DSCTIPRECPAG", string.Empty, string.Empty).ToList();
            model.ContasBancaria = FuncionalidadeBLL.PopulaCombos<ContaBancariaViewModel>("CONTA_BANCARIA", "CODCONBAN", "NOMCONBAN", "", string.Empty).ToList();

        }

        [HttpPost]
        public JsonResult Gravar(AcaoViewModel acao)
        {
            AcaoBLL objBll = new AcaoBLL();
            AcaoViewModel model = new AcaoViewModel();
           
            //acao.MatrizFilial = (((UsuarioBEAN)Session["session_Usuario"]).MatrizFilial);
                        
            model.Codigo = int.Parse(objBll.Gravar(acao));

            //PopulaCombos(model);

            //JsonResult jsonResult = Json(model,JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;

            //return View("~/Views/Cadastros/Processo/Index.cshtml", model);
            return Json(model);
           
        }

        [HttpPost]
        public ActionResult AlterarAssessoria(string pAcao, string pAssessoria)
        {
            AcaoBLL objBll = new AcaoBLL();
            AcaoViewModel model = new AcaoViewModel();
            
            objBll.AlterarAssessoria(pAcao, pAssessoria);
            
            return View("~/Views/Cadastros/Processo/Index.cshtml", model);
        }

        [HttpPost]
        public PartialViewResult Buscar(AcaoViewModel acao, string pPagina, string pOrdem)
        {
            AcaoListaViewModel model = new AcaoListaViewModel();
            AcaoBLL objBll = new AcaoBLL();

            model.AssessoriasParaConfirmar = FuncionalidadeBLL.PopulaCombos<PessoaViewModel>("PESSOA", "CODPES", "NOMPES", "NOMPES", " AND EHASSESSORIA = 'S'").ToList();

            if ((((UsuarioBEAN)Session["session_Usuario"]).Nivel).Equals("A"))// N DE NORMAL
                acao.BuscaPorUsuarioPermitido = (((UsuarioBEAN)Session["session_Usuario"]).Codigo).ToString();

            acao.BuscaEhRecebido = acao.BuscaEhRecebido == "on" ? "S" : String.Empty;
            acao.BuscaNaoRecebido = acao.BuscaNaoRecebido == "on" ? "S" : String.Empty;
            acao.BuscaSemAssessoria = acao.BuscaSemAssessoria == "on" ? "S" : String.Empty;

            if (pOrdem.Equals("DATA"))
                pOrdem = "A.DTACA";
            else if (pOrdem.Equals("REQUERENTE"))
                pOrdem = "R.NOMPES, A.NUMACA";

            model.Acao = objBll.PopulaGrid(acao, pOrdem);

            return PartialView("~/Views/Cadastros/Processo/" + pPagina + ".cshtml", model);
        }

        [HttpPost]
        public JsonResult PreencherCampos(string pCodigo)
        {
            AcaoBLL objBll = new AcaoBLL();
            AcaoViewModel model = new AcaoViewModel();
            
            model = objBll.Registro(pCodigo);

            if ((((UsuarioBEAN)Session["session_Usuario"]).Nivel).Equals("A"))
                model.Valor = 0;

            return Json(model);
            //return Json(new { Codigo = model.Codigo, Numeracao = model.Numeracao });
        }


        [HttpPost]
        public ActionResult GravarContasPagar(AcaoViewModel acao)
        {

            if (acao.ValorContasPagar == null || acao.VencimentoContasPagar == null)
                throw new Exception("Preencha pelo menos os campos Valor e Vencimento.");

            ContasPagarBLL objBll = new ContasPagarBLL();
            AcaoViewModel model = new AcaoViewModel();
            ContasPagarViewModel modelCp = new ContasPagarViewModel();

            modelCp.CodigoOrigem = acao.Codigo;            
            modelCp.OrigemCadastro = 1; //acao processo
            modelCp.ParcelaX   = acao.ParcelaDeContasPagar;
            modelCp.ParcelaY   = acao.ParcelaAteContasPagar;
            modelCp.Valor      = acao.ValorContasPagar;
            modelCp.Vencimento = acao.VencimentoContasPagar;

            if (acao.ResponsavelFinanceiro != null && acao.ResponsavelFinanceiro > 0)
                modelCp.Favorecido = acao.ResponsavelFinanceiro;
            else
                modelCp.Favorecido = acao.Favorecido;

            modelCp.Sucumbencia = acao.Sucumbencia;
            modelCp.TipoRecebimentoPagamento = acao.TipoRecebimentoPagamento;

            modelCp.MatrizFilial = acao.MatrizFilial;
            modelCp.TelaBotaoQueChamou = 2;
            model.CodigoUsuario = ((UsuarioBEAN)Session["session_Usuario"]).Codigo;
            objBll.Gravar(modelCp);

            PopulaCombos(model);

            return View("~/Views/Cadastros/Processo/Index.cshtml", model);
        }

        [HttpPost]
        public ActionResult GravarContasReceber(AcaoViewModel acao)
        {
            ContasReceberBLL objBll = new ContasReceberBLL();
            AcaoViewModel model = new AcaoViewModel();
            ContasReceberViewModel modelCr = new ContasReceberViewModel();

            modelCr.CodigoOrigem = acao.Codigo;            
            modelCr.OrigemCadastro = 1; //acao processo
            modelCr.ParcelaX   = acao.ParcelaDeContasReceber;
            modelCr.ParcelaY   = acao.ParcelaAteContasReceber;
            modelCr.Valor      = acao.ValorContasReceber;
            modelCr.ValorPago  = acao.ValorRecebidoContasReceber;
            modelCr.Vencimento = acao.VencimentoContasReceber;
            modelCr.Principal  = acao.Principal;
            modelCr.Descricao  = acao.DescricaoContaReceber;
            modelCr.ContaBancaria = acao.ContaBancaria;            

            if (acao.ResponsavelFinanceiro != null && acao.ResponsavelFinanceiro > 0)
                modelCr.Cliente = acao.ResponsavelFinanceiro;
            else
                modelCr.Cliente = acao.Cliente;

            modelCr.TipoRecebimentoPagamentoRec = acao.TipoRecebimentoPagamentoRec;
            modelCr.MatrizFilial = acao.MatrizFilial;
            modelCr.TelaBotaoQueChamou = 4;
            model.CodigoUsuario = ((UsuarioBEAN)Session["session_Usuario"]).Codigo;
            objBll.Gravar(modelCr);

            PopulaCombos(model);

            return View("~/Views/Cadastros/Processo/Index.cshtml", model);
        }


        [HttpPost]
        public PartialViewResult BuscarContasPagar(AcaoViewModel acao)
        {           

            ContasPagarViewModel pModel = new ContasPagarViewModel();
            pModel.BuscaProcessoCodigo = acao.Codigo.ToString();

            ContasPagarListaViewModel model2 = new ContasPagarListaViewModel();
            ContasPagarBLL objBll = new ContasPagarBLL();

            model2.Contas = objBll.PopulaGrid(pModel);


            return PartialView("~/Views/Cadastros/Processo/GridContasPagar.cshtml", model2);
        }

        [HttpPost]
        public PartialViewResult BuscarContasReceber(AcaoViewModel acao)
        {            

            ContasReceberViewModel pModel = new ContasReceberViewModel();
            pModel.BuscaProcessoCodigo = acao.Codigo.ToString();

            ContasReceberListaViewModel model = new ContasReceberListaViewModel();
            ContasReceberBLL objBll = new ContasReceberBLL();

            model.Contas = objBll.PopulaGrid(pModel);

            return PartialView("~/Views/Cadastros/Processo/GridContasReceber.cshtml", model);
        }

        [HttpPost]
        public ActionResult Excluir(AcaoViewModel pModel)
        {
            AcaoBLL objBll = new AcaoBLL();

            objBll.Excluir(pModel);
            
            return View("~/Views/Cadastros/Processo/Index.cshtml");
        }

        [HttpPost]
        public JsonResult GravarAcaoConfirmada(string pAssessoria)
        {
            AcaoViewModel model = new AcaoViewModel();
            AcaoBLL objBll = new AcaoBLL();
            
            model.Codigo = int.Parse(objBll.GravarAcaoConfirmada((((UsuarioBEAN)Session["session_Usuario"]).Codigo).ToString(), pAssessoria));
            return Json(model);
        }

        [HttpPost]
        public JsonResult GravarAcaoConfirmadaDetalhe(string pAcao, string pAssessoria, string pConfirmacao)
        {
            AcaoViewModel model = new AcaoViewModel();
            AcaoConfirmadaDetalheBLL objBll = new AcaoConfirmadaDetalheBLL();

            objBll.Gravar(pAcao, (((UsuarioBEAN)Session["session_Usuario"]).Codigo).ToString(), pAssessoria, pConfirmacao);
            model.Codigo = 0;

            return Json(model);
        }

        [HttpPost]
        public JsonResult Confirmar(string pCodigo)
        {
            AcaoViewModel model = new AcaoViewModel();
            AcaoConfirmadaDetalheBLL objBll = new AcaoConfirmadaDetalheBLL();

            objBll.Confirmar(pCodigo);
            model.Codigo = 0;

            return Json(model);
        }

    }
}
