using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BEAN;
using BLL;
using MODELS;
using System.IO;

namespace Aplicacao.Controllers
{
    public class ContasPagarController : Controller
    {
        // GET: ContasPagar
        public ActionResult Index(string pCodigo)
        {
            if (Session["session_Usuario"] == null)
                return View("~/Views/Usuario/Login.cshtml");

            string vView = UsuarioBLL.Permissao(Session["session_Usuario"], (((UsuarioBEAN)Session["session_Usuario"]).Codigo.ToString()), "2", (((UsuarioBEAN)Session["session_Usuario"]).Nivel) );

            if (vView.Length > 0)
                return View(vView);

            ContasPagarViewModel model = new ContasPagarViewModel();

            if (pCodigo != null)
            {
                ContasPagarBLL objBll = new ContasPagarBLL();
                model = objBll.Registro(pCodigo);
            }

            PopulaCombos(model);
            
            return View("~/Views/Financeiro/ContasPagar/Index.cshtml", model);
        }

        private void PopulaCombos(ContasPagarViewModel model)
        {
            model.Meses = FuncionalidadeBLL.PopulaCombos<MesViewModel>("MES", "CODMES", "DSCMES", string.Empty, string.Empty).ToList();
            model.Ano = FuncionalidadeBLL.PopulaCombos<AnoViewModel>("ANO", "ANO", "ANO", "ANO DESC", string.Empty).ToList();
            model.Pessoa = FuncionalidadeBLL.PopulaCombos<PessoaViewModel>("PESSOA", "CODPES", "NOMPES", "NOMPES", string.Empty).ToList();
            model.ContasBancaria = FuncionalidadeBLL.PopulaCombos<ContaBancariaViewModel>("CONTA_BANCARIA", "CODCONBAN", "NOMCONBAN", "", string.Empty).ToList();
            model.Formas = FuncionalidadeBLL.PopulaCombos<FormaPagamentoRecebimentoViewModel>("FORMA_PAGAMENTO_RECEBIMENTO", "CODFORPAGREC", "DSCFORPAGREC", string.Empty, string.Empty).ToList();
            model.Origens = FuncionalidadeBLL.PopulaCombos<OrigemCadastroViewModel>("ORIGEM_CADASTRO", "CODORICAD", "NOMORICAD", string.Empty, string.Empty).ToList();
            model.Empresas = FuncionalidadeBLL.PopulaCombos<EmpresaViewModel>("EMPRESA", "CODEMP", "NOMEMP", string.Empty, string.Empty).ToList();
            
            model.TipoCobrancaRecebimentos = FuncionalidadeBLL.PopulaCombos<TipoCobrancaRecebimentoViewModel>("TIPO_COBRANCA_RECEBIMENTO", "CODTIPCOB", "NOMTIPCOB", string.Empty, string.Empty).ToList();
            model.ContaContabeis = FuncionalidadeBLL.PopulaCombos<ContaContabilViewModel>("CONTA_CONTABIL", "CODCONCON", "NOMCONCON", string.Empty, string.Empty).ToList();
            model.CentroCustos = FuncionalidadeBLL.PopulaCombos<CentroCustoViewModel>("CENTRO_CUSTO", "CODCENCUS", "NOMCENCUS", string.Empty, string.Empty).ToList();
            model.TiposRecebimentoPagamento = FuncionalidadeBLL.PopulaCombos<OrigemCadastroViewModel>("TIPO_RECEBE_PAGA", "CODTIPRECPAG", "DSCTIPRECPAG", string.Empty, string.Empty).ToList();

        }

        [HttpPost]
        public JsonResult Gravar(ContasPagarViewModel pModel)
        {

            ContasPagarViewModel model = new ContasPagarViewModel();
            ContasPagarBLL objBll = new ContasPagarBLL();

            pModel.TelaBotaoQueChamou = 1;
            pModel.CodigoUsuario = ((UsuarioBEAN)Session["session_Usuario"]).Codigo;


            if (pModel.Pago != null && (pModel.Pago.Equals("S") || pModel.Pago.Equals("s")) )
            {
                if (pModel.ValorPago == null)
                    pModel.ValorPago = pModel.Valor;
            }

            model.Codigo = int.Parse(objBll.Gravar(pModel));           

            //PopulaCombos(model);
                        
            return Json(model);
        }

        [HttpPost]
        //[Aplicacao.Models.Autorizacao(Roles = "Atendimento")]
        public PartialViewResult Buscar(ContasPagarViewModel pModel)
        {
            ContasPagarListaViewModel model = new ContasPagarListaViewModel();
            ContasPagarBLL objBll = new ContasPagarBLL();

            if ((((UsuarioBEAN)Session["session_Usuario"]).Nivel).Equals("N"))
                pModel.BuscaPorUsuarioPermitido = (((UsuarioBEAN)Session["session_Usuario"]).Codigo).ToString();


            model.Contas = objBll.PopulaGrid(pModel);

            return PartialView("~/Views/Financeiro/ContasPagar/Grid.cshtml", model);
        }

        [HttpPost]
        public JsonResult PreencherCampos(string pCodigo)
        {
            ContasPagarBLL objBll = new ContasPagarBLL();
            ContasPagarViewModel model = new ContasPagarViewModel();

            model = objBll.Registro(pCodigo);

            return Json(model);            
        }

        [HttpPost]
        public PartialViewResult Imprimir(ContasPagarViewModel pModel)
        {
            ContasPagarListaViewModel model = new ContasPagarListaViewModel();
            ContasPagarBLL objBll = new ContasPagarBLL();

            model.Contas = objBll.PopulaGrid(pModel);

            model.FiltrosUtilizados = "<br /><br />Filtros Utilizados";

            if (pModel.BuscaFavorecido != null)
                model.FiltrosUtilizados += "<br />FAVORECIDO: " + pModel.BuscaFavorecido;

            if (pModel.BuscaDescricao != null)
                model.FiltrosUtilizados += "<br />DESCRIÇÃO: " + pModel.BuscaDescricao;

            if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                model.FiltrosUtilizados += "<br />DT VENCIMENTO: " + string.Format("{0:dd/MM/yyyy}", pModel.BuscaDataVencimentoDe);

            if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                model.FiltrosUtilizados += "<br />DT VENCIMENTO DE: " + string.Format("{0:dd/MM/yyyy}", pModel.BuscaDataVencimentoDe) + " À " + string.Format("{0:dd/MM/yyyy}", pModel.BuscaDataVencimentoAte);

            if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte == null)
                model.FiltrosUtilizados += "<br />DT PAGAMENTO: " + string.Format("{0:dd/MM/yyyy}", pModel.BuscaDataPagamentoDe);

            if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte != null)
                model.FiltrosUtilizados += "<br />DT PAGAMENTO DE: " + string.Format("{0:dd/MM/yyyy}", pModel.BuscaDataPagamentoDe) + " À " + string.Format("{0:dd/MM/yyyy}", pModel.BuscaDataPagamentoAte);

            if (pModel.BuscaFixa != null & pModel.BuscaFixa == "S")
                model.FiltrosUtilizados += "<br />FIXA: Sim" ;

            if (pModel.BuscaFixa != null & pModel.BuscaFixa == "N")
                model.FiltrosUtilizados += "<br />FIXA: Não";

            return PartialView("~/Views/Financeiro/ContasPagar/Impressao.cshtml", model);

        }   

     
        [HttpPost]
        public void Upload(string pCodigo)
        {
            string fileName = Request.Headers["X-File-Name"];
            string fileType = Request.Headers["X-File-Type"];
            int fileSize = Convert.ToInt32(Request.Headers["X-File-Size"]);        
            string vNomeArq = Request.Headers["vNomeArquivo"];
            //File's content is available in Request.InputStream property
            System.IO.Stream fileContent = Request.InputStream;
            //Creating a FileStream to save file's content

            if (vNomeArq.Length > 0)
            {
                System.IO.FileStream fileStream = null;

                if (fileType.Equals("text/plain"))
                    fileStream = System.IO.File.Create(Server.MapPath("~/Documentos/Cp/") + vNomeArq + ".txt");
                else if (fileType.Equals("application/pdf"))
                    fileStream = System.IO.File.Create(Server.MapPath("~/Documentos/Cp/") + vNomeArq + ".pdf");
                else if (fileType.Equals("image/jpeg"))
                    fileStream = System.IO.File.Create(Server.MapPath("~/Documentos/Cp/") + vNomeArq + ".jpg");
                else if (fileType.Equals("image/png"))
                    fileStream = System.IO.File.Create(Server.MapPath("~/Documentos/Cp/") + vNomeArq + ".png");
                else if (fileType.Equals("image/gif"))
                    fileStream = System.IO.File.Create(Server.MapPath("~/Documentos/Cp/") + vNomeArq + ".gif");

                fileContent.Seek(0, System.IO.SeekOrigin.Begin);
                //Copying file's content to FileStream
                fileContent.CopyTo(fileStream);
                fileStream.Dispose();
                //return Json("File uploaded");
            }
        }

        [HttpPost]
        public ActionResult Excluir(string pCodigo)
        {
            ContasPagarBLL objBll = new ContasPagarBLL();

            ContasPagarViewModel model = new ContasPagarViewModel();        

            objBll.Excluir(pCodigo);

            return View("~/Views/Financeiro/ContasPagar/Index.cshtml");
        }

        [HttpPost]
        public JsonResult BaixaLote(string pCodigo)
        {
            ContasPagarViewModel model = new ContasPagarViewModel();
            ContasPagarBLL objBll = new ContasPagarBLL();


            objBll.BaixaLote(pCodigo, (((UsuarioBEAN)Session["session_Usuario"]).Codigo).ToString());
            model.Codigo = 0;

            return Json(model);
        }

        [HttpPost]
        public PartialViewResult ImprimirDespesas(ContasPagarViewModel pModel)
        {
            ContasPagarListaViewModel model = new ContasPagarListaViewModel();
            ContasPagarBLL objBll = new ContasPagarBLL();

            model.Contas = objBll.ImprimirReceita(pModel);

            model.FiltrosUtilizados = "<br /><br />Filtros Utilizados";

            if (pModel.BuscaFavorecido != null)
                model.FiltrosUtilizados += "<br />FAVORECIDO: " + pModel.BuscaFavorecido;

            if (pModel.BuscaDescricao != null)
                model.FiltrosUtilizados += "<br />DESCRIÇÃO: " + pModel.BuscaDescricao;

            if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                model.FiltrosUtilizados += "<br />DT VENCIMENTO: " + string.Format("{0:dd/MM/yyyy}", pModel.BuscaDataVencimentoDe);

            if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                model.FiltrosUtilizados += "<br />DT VENCIMENTO DE: " + string.Format("{0:dd/MM/yyyy}", pModel.BuscaDataVencimentoDe) + " À " + string.Format("{0:dd/MM/yyyy}", pModel.BuscaDataVencimentoAte);

            if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte == null)
                model.FiltrosUtilizados += "<br />DT PAGAMENTO: " + string.Format("{0:dd/MM/yyyy}", pModel.BuscaDataPagamentoDe);

            if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte != null)
                model.FiltrosUtilizados += "<br />DT PAGAMENTO DE: " + string.Format("{0:dd/MM/yyyy}", pModel.BuscaDataPagamentoDe) + " À " + string.Format("{0:dd/MM/yyyy}", pModel.BuscaDataPagamentoAte);


            return PartialView("~/Views/Financeiro/ContasPagar/ImpressaoDespesas.cshtml", model);

        }


    }
}