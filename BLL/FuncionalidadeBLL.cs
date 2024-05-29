﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net.Mail;

using DaoMySql;
using MODELS;

namespace BLL
{
    public class FuncionalidadeBLL
    {
        public static IEnumerable<T> PopulaCombos<T>(string pTabela, string pCampoCodigo, string pCampoDescricao, string pOrderBy, string pWhere)
        {
            FuncionalidadeDaoMySql objDao = new FuncionalidadeDaoMySql();
            return objDao.PopulaCombos<T>(pTabela,  pCampoCodigo, pCampoDescricao, pOrderBy, pWhere);
        }

        public DataTable PopulaCombo(string pTabela, string pCampos, string pWhere)
        {
            FuncionalidadeDaoMySql objDaoMy = new FuncionalidadeDaoMySql();
            return objDaoMy.PopulaCombo(pTabela, pCampos, pWhere);

        }

        #region Limpa Cnpj
        public string LimpaCnpjCpf(string texto)
        {
            string textor = "";

            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i].ToString() == ".") textor += "";
                else if (texto[i].ToString() == "-") textor += "";
                else if (texto[i].ToString() == "/") textor += "";
                else if (texto[i].ToString() == " ") textor += "";
                else if (texto[i].ToString() == ",") textor += "";


                else textor += texto[i];
            }
            return textor.ToUpper();
        }
        #endregion

        public string Enviar(string pPara, string pAssunto, string pCorpo, List<string> pAnexos, bool pIncorporaImagem)
        {

            ParametroBLL objParametroBll = new ParametroBLL();
            ParametroViewModel objViewModel = new ParametroViewModel();
            objViewModel = objParametroBll.Parametro("SMTP");

            string[] config = (objViewModel.Conteudo).Split(';');

            string pSmtp    = config[0].ToString();
            int    pPorta   = Convert.ToInt16(config[1]);
            string pConta   = config[2].ToString();
            string pSenha   = config[3].ToString();
            string pDe      = config[2].ToString();
            string pDisplay = config[4].ToString();

            try
            {
                MailMessage objMensagem = new MailMessage();

                StringBuilder vHtmlCompleto = new StringBuilder();
                string vCorpo = pCorpo;
                int cont = 0;
                string vImagem = "Imagem";

                objMensagem.From = new MailAddress(pDe, pDisplay);
                objMensagem.To.Add(pPara);

                if (pAnexos != null)
                {
                    foreach (string anexos in pAnexos)
                    {
                        Attachment att = new Attachment(anexos);
                        objMensagem.Attachments.Add(att);

                        if (pIncorporaImagem)
                        {
                            att.ContentId = vImagem + cont.ToString();
                            vCorpo = vCorpo.Replace(anexos, "<img src=\"cid:" + vImagem + cont.ToString() + "\"><br>");
                        }

                        cont += 1;
                    }
                }

                objMensagem.Subject = pAssunto;

                
                #region 
                vHtmlCompleto.AppendLine("<html>");
                vHtmlCompleto.AppendLine("<head>");

                vHtmlCompleto.AppendLine("<link rel=\"stylesheet\" href=\"http://www.eaj.systems-on.com.br/admin-lte/bootstrap/dist/css/bootstrap.min.css\"> ");
                vHtmlCompleto.AppendLine("<link rel=\"stylesheet\" href=\"http://www.eaj.systems-on.com.br/admin-lte/font-awesome/css/font-awesome.min.css\">");
                vHtmlCompleto.AppendLine("<link rel=\"stylesheet\" href=\"http://www.eaj.systems-on.com.br/admin-lte/Ionicons/css/ionicons.min.css\">        ");
                vHtmlCompleto.AppendLine("<link rel=\"stylesheet\" href=\"http://www.eaj.systems-on.com.br/admin-lte/dist/css/AdminLTE.min.css\">            ");
                vHtmlCompleto.AppendLine("<link rel=\"stylesheet\" href=\"http://www.eaj.systems-on.com.br/admin-lte/dist/css/skins/_all-skins.min.css\">    ");
                vHtmlCompleto.AppendLine("<link rel=\"stylesheet\" href=\"http://www.eaj.systems-on.com.br/admin-lte/morris.js/morris.css\">                 ");
                vHtmlCompleto.AppendLine("<link rel=\"stylesheet\" href=\"http://www.eaj.systems-on.com.br/admin-lte/jvectormap/jquery-jvectormap.css\">     ");
                vHtmlCompleto.AppendLine("<link rel=\"stylesheet\" href=\"http://www.eaj.systems-on.com.br/admin-lte/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css\"> ");
                vHtmlCompleto.AppendLine("<link rel=\"stylesheet\" href=\"http://www.eaj.systems-on.com.br/admin-lte/bootstrap-daterangepicker/daterangepicker.css\">              ");
                vHtmlCompleto.AppendLine("<link rel=\"stylesheet\" href=\"http://www.eaj.systems-on.com.br/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css\">   ");

                vHtmlCompleto.AppendLine("</head>");
                vHtmlCompleto.AppendLine("<body>");
                vHtmlCompleto.AppendLine(vCorpo);
                vHtmlCompleto.AppendLine("</body>");
                vHtmlCompleto.AppendLine("</html>");
                #endregion


                objMensagem.Body = vHtmlCompleto.ToString();


                objMensagem.IsBodyHtml = true;

                SmtpClient objSmtp = new SmtpClient();
                objSmtp.Host = pSmtp;
                objSmtp.Port = pPorta;
                objSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtp.EnableSsl = false;

                objSmtp.Credentials = new System.Net.NetworkCredential(pConta, pSenha);
                objSmtp.Send(objMensagem);

                return "Enviado com sucesso.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public enum TipoValorExtenso
        {
            Monetario,
            Porcentagem,
            Decimal
        }

        public static string toExtenso(double Valor, TipoValorExtenso tipoValorExtenso)
        {
            decimal valorEscrever = new decimal(Valor);

            return toExtenso(valorEscrever, tipoValorExtenso);
        }

        // O método toExtenso recebe um valor do tipo decimal
        public static string toExtenso(decimal valor, TipoValorExtenso tipoValorExtenso)
        {
            if (valor == 0)
                return "Zero";

            if (valor <= 0 | valor >= 1000000000000000)
                throw new ArgumentOutOfRangeException("Valor não suportado pelo sistema. Valor: " + valor);

            string strValor = String.Empty;
            strValor = valor.ToString("000000000000000.00#");
            //strValor = valor.ToString("{0:0.00#}");
            string valor_por_extenso = string.Empty;
            int qtdCasasDecimais =
                strValor.Substring(strValor.IndexOf(',') + 1, strValor.Length - (strValor.IndexOf(',') + 1)).Length;
            bool existemValoresAposDecimal = Convert.ToInt32(strValor.Substring(16, qtdCasasDecimais)) > 0;

            for (int i = 0; i <= 15; i += 3)
            {
                var parte = strValor.Substring(i, 3);
                // se parte contém vírgula, pega a substring com base na quantidade de casas decimais.
                if (parte.Contains(','))
                {
                    parte = strValor.Substring(i + 1, qtdCasasDecimais);
                }
                valor_por_extenso += escreva_parte(Convert.ToDecimal(parte));
                if (i == 0 & valor_por_extenso != string.Empty)
                {
                    if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                        valor_por_extenso += " TRILHÃO" +
                                             ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0)
                                                  ? " E "
                                                  : string.Empty);
                    else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                        valor_por_extenso += " TRILHÕES" +
                                             ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0)
                                                  ? " E "
                                                  : string.Empty);
                }
                else if (i == 3 & valor_por_extenso != string.Empty)
                {
                    if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                        valor_por_extenso += " BILHÃO" +
                                             ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0)
                                                  ? " E "
                                                  : string.Empty);
                    else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                        valor_por_extenso += " BILHÕES" +
                                             ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0)
                                                  ? " E "
                                                  : string.Empty);
                }
                else if (i == 6 & valor_por_extenso != string.Empty)
                {
                    if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                        valor_por_extenso += " MILHÃO" +
                                             ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0)
                                                  ? " E "
                                                  : string.Empty);
                    else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                        valor_por_extenso += " MILHÕES" +
                                             ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0)
                                                  ? " E "
                                                  : string.Empty);
                }
                else if (i == 9 & valor_por_extenso != string.Empty)
                    if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                        valor_por_extenso += " MIL" +
                                             ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0)
                                                  ? " E "
                                                  : string.Empty);

                if (i == 12)
                {
                    if (valor_por_extenso.Length > 8)
                        if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "BILHÃO" |
                            valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "MILHÃO")
                            valor_por_extenso += " DE";
                        else if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" |
                                 valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES" |
                                 valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "TRILHÕES")
                            valor_por_extenso += " DE";
                        else if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "TRILHÕES")
                            valor_por_extenso += " DE";

                    if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                    {
                        switch (tipoValorExtenso)
                        {
                            case TipoValorExtenso.Monetario:
                                valor_por_extenso += " REAL";
                                break;
                            case TipoValorExtenso.Porcentagem:
                                if (existemValoresAposDecimal == false)
                                    valor_por_extenso += " PORCENTO";
                                break;
                            case TipoValorExtenso.Decimal:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("tipoValorExtenso");
                        }
                    }

                    else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                    {
                        switch (tipoValorExtenso)
                        {
                            case TipoValorExtenso.Monetario:
                                valor_por_extenso += " REAIS";
                                break;
                            case TipoValorExtenso.Porcentagem:
                                if (existemValoresAposDecimal == false)
                                    valor_por_extenso += " PORCENTO";
                                break;
                            case TipoValorExtenso.Decimal:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("tipoValorExtenso");
                        }
                    }

                    if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                    {
                        switch (tipoValorExtenso)
                        {
                            case TipoValorExtenso.Monetario:
                                valor_por_extenso += " E ";
                                break;
                            case TipoValorExtenso.Porcentagem:
                                valor_por_extenso += " VÍRGULA ";
                                break;
                            case TipoValorExtenso.Decimal:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("tipoValorExtenso");
                        }
                    }
                }

                if (i == 15)
                    if (Convert.ToInt32(strValor.Substring(16, qtdCasasDecimais)) == 1)
                    {
                        switch (tipoValorExtenso)
                        {
                            case TipoValorExtenso.Monetario:
                                valor_por_extenso += " CENTAVO";
                                break;
                            case TipoValorExtenso.Porcentagem:
                                valor_por_extenso += " CENTAVO";
                                break;
                            case TipoValorExtenso.Decimal:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("tipoValorExtenso");
                        }
                    }

                    else if (Convert.ToInt32(strValor.Substring(16, qtdCasasDecimais)) > 1)
                    {
                        switch (tipoValorExtenso)
                        {
                            case TipoValorExtenso.Monetario:
                                valor_por_extenso += " CENTAVOS";
                                break;
                            case TipoValorExtenso.Porcentagem:
                                valor_por_extenso += " PORCENTO";
                                break;
                            case TipoValorExtenso.Decimal:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("tipoValorExtenso");
                        }
                    }
            }
            return valor_por_extenso;
        }

        private static string escreva_parte(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                if (a == 1) montagem += (b + c == 0) ? "CEM" : "CENTO";
                else if (a == 2) montagem += "DUZENTOS";
                else if (a == 3) montagem += "TREZENTOS";
                else if (a == 4) montagem += "QUATROCENTOS";
                else if (a == 5) montagem += "QUINHENTOS";
                else if (a == 6) montagem += "SEISCENTOS";
                else if (a == 7) montagem += "SETECENTOS";
                else if (a == 8) montagem += "OITOCENTOS";
                else if (a == 9) montagem += "NOVECENTOS";

                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " E " : string.Empty) + "DEZ";
                    else if (c == 1) montagem += ((a > 0) ? " E " : string.Empty) + "ONZE";
                    else if (c == 2) montagem += ((a > 0) ? " E " : string.Empty) + "DOZE";
                    else if (c == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TREZE";
                    else if (c == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUATORZE";
                    else if (c == 5) montagem += ((a > 0) ? " E " : string.Empty) + "QUINZE";
                    else if (c == 6) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSEIS";
                    else if (c == 7) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSETE";
                    else if (c == 8) montagem += ((a > 0) ? " E " : string.Empty) + "DEZOITO";
                    else if (c == 9) montagem += ((a > 0) ? " E " : string.Empty) + "DEZENOVE";
                }
                else if (b == 2) montagem += ((a > 0) ? " E " : string.Empty) + "VINTE";
                else if (b == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TRINTA";
                else if (b == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUARENTA";
                else if (b == 5) montagem += ((a > 0) ? " E " : string.Empty) + "CINQUENTA";
                else if (b == 6) montagem += ((a > 0) ? " E " : string.Empty) + "SESSENTA";
                else if (b == 7) montagem += ((a > 0) ? " E " : string.Empty) + "SETENTA";
                else if (b == 8) montagem += ((a > 0) ? " E " : string.Empty) + "OITENTA";
                else if (b == 9) montagem += ((a > 0) ? " E " : string.Empty) + "NOVENTA";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " E ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "UM";
                    else if (c == 2) montagem += "DOIS";
                    else if (c == 3) montagem += "TRÊS";
                    else if (c == 4) montagem += "QUATRO";
                    else if (c == 5) montagem += "CINCO";
                    else if (c == 6) montagem += "SEIS";
                    else if (c == 7) montagem += "SETE";
                    else if (c == 8) montagem += "OITO";
                    else if (c == 9) montagem += "NOVE";

                return montagem;
            }
        }

    }
}
