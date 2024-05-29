using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BEAN;
using BLL;
using MODELS;

namespace Aplicacao.Controllers
{
    public class ContasReceberController : Controller
    {
        // GET: ContasReceber
        public ActionResult Index(string pCodigo)
        {
            string vView = UsuarioBLL.Permissao(Session["session_Usuario"], (((UsuarioBEAN)Session["session_Usuario"]).Codigo.ToString()), "3", (((UsuarioBEAN)Session["session_Usuario"]).Nivel));

            if (vView.Length > 0)
                return View(vView);

            ContasReceberViewModel model = new ContasReceberViewModel();

            if (pCodigo != null)
            {
                ContasReceberBLL objBll = new ContasReceberBLL();
                model = objBll.Registro(pCodigo);
            }

            PopulaCombos(model);


            return View("~/Views/Financeiro/ContasReceber/Index.cshtml", model);
        }       

        private void PopulaCombos(ContasReceberViewModel model)
        {
            model.Meses = FuncionalidadeBLL.PopulaCombos<MesViewModel>("MES", "CODMES", "DSCMES", "",string.Empty).ToList();
            model.Ano = FuncionalidadeBLL.PopulaCombos<AnoViewModel>("ANO", "ANO", "ANO", "ANO DESC", string.Empty).ToList();
            model.Pessoa = FuncionalidadeBLL.PopulaCombos<PessoaViewModel>("PESSOA", "CODPES", "NOMPES", "NOMPES",string.Empty).ToList();
            model.ContasBancaria = FuncionalidadeBLL.PopulaCombos<ContaBancariaViewModel>("CONTA_BANCARIA", "CODCONBAN", "NOMCONBAN", "", string.Empty).ToList();
            model.Formas = FuncionalidadeBLL.PopulaCombos<FormaPagamentoRecebimentoViewModel>("FORMA_PAGAMENTO_RECEBIMENTO", "CODFORPAGREC", "DSCFORPAGREC", "", string.Empty).ToList();
            model.Origens = FuncionalidadeBLL.PopulaCombos<OrigemCadastroViewModel>("ORIGEM_CADASTRO", "CODORICAD", "NOMORICAD", "", string.Empty).ToList();
            model.Empresas = FuncionalidadeBLL.PopulaCombos<EmpresaViewModel>("EMPRESA", "CODEMP", "NOMEMP", string.Empty, string.Empty).ToList();
            model.TiposRecebimentoPagamento = FuncionalidadeBLL.PopulaCombos<OrigemCadastroViewModel>("TIPO_RECEBE_PAGA", "CODTIPRECPAG", "DSCTIPRECPAG", string.Empty, string.Empty).ToList();

        }

        [HttpPost]
        public JsonResult Gravar(ContasReceberViewModel pModel)
        {

            ContasReceberViewModel model = new ContasReceberViewModel();
            ContasReceberBLL objBll = new ContasReceberBLL();

            pModel.TelaBotaoQueChamou = 3;
            pModel.CodigoUsuario = ((UsuarioBEAN)Session["session_Usuario"]).Codigo;
            model.Codigo = int.Parse(objBll.Gravar(pModel));

            //PopulaCombos(model);

            //return View("~/Views/Financeiro/ContasPagar/Index.cshtml", model);
            return Json(model);
        }

        [HttpPost]
        public PartialViewResult Buscar(ContasReceberViewModel pModel)
        {
            ContasReceberListaViewModel model = new ContasReceberListaViewModel();
            ContasReceberBLL objBll = new ContasReceberBLL();

            if ((((UsuarioBEAN)Session["session_Usuario"]).Nivel).Equals("N"))
                pModel.BuscaPorUsuarioPermitido = (((UsuarioBEAN)Session["session_Usuario"]).Codigo).ToString();


            model.Contas = objBll.PopulaGrid(pModel);

            return PartialView("~/Views/Financeiro/ContasReceber/Grid.cshtml", model);
        }

        [HttpPost]
        public JsonResult PreencherCampos(string pCodigo)
        {
            ContasReceberBLL objBll = new ContasReceberBLL();
            ContasReceberViewModel model = new ContasReceberViewModel();

            model = objBll.Registro(pCodigo);

            return Json(model);
            //return Json(new { Codigo = model.Codigo, Numeracao = model.Numeracao });
        }

        [HttpPost]
        public ActionResult Excluir(ContasReceberViewModel pessoa)
        {
            ContasReceberBLL objBll = new ContasReceberBLL();

            ContasReceberViewModel model = new ContasReceberViewModel();

            objBll.Excluir(pessoa);

            return View("~/Views/Financeiro/ContasReceber/Index.cshtml");
        }

        [HttpPost]
        public JsonResult BaixaLote(string pCodigo)
        {
            ContasReceberViewModel model = new ContasReceberViewModel();
            ContasReceberBLL objBll = new ContasReceberBLL();

            
            objBll.BaixaLote(pCodigo, (((UsuarioBEAN)Session["session_Usuario"]).Codigo).ToString());
            model.Codigo = 0;            
            
            return Json(model);
        }

        [HttpPost]
        public PartialViewResult ImprimirReceita(ContasReceberViewModel pModel)
        {
            ContasReceberListaViewModel model = new ContasReceberListaViewModel();
            ContasReceberBLL objBll = new ContasReceberBLL();

            model.Contas = objBll.ImprimirReceita(pModel);

            model.FiltrosUtilizados = "<br /><br />Filtros Utilizados";

            if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                model.FiltrosUtilizados += "<br />DT VENCIMENTO: " + string.Format("{0:dd/MM/yyyy}", pModel.BuscaDataVencimentoDe);


            return PartialView("~/Views/Financeiro/ContasReceber/impressaoReceita.cshtml", model);
        }
    }
}