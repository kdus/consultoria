using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace Aplicacao.Models
{
    public class Relatorio
    {
        public string vOrigem = string.Empty;
        public string vNomeRlt = string.Empty;
        public DataTable dtOrigem = new DataTable();
        public string vDisplayName = string.Empty;
        public string vNomeColunaArquivo = string.Empty;
        public string vNomeArquivo = string.Empty;
        public bool vContemImagem = false;


        public bool vExportaPdf = false;

        public string vNomeParametro1 = string.Empty;
        public string vNomeParametro2 = string.Empty;
        public string vNomeParametro3 = string.Empty;
        public string vNomeParametro4 = string.Empty;

        public string vValorParametro1 = string.Empty;
        public string vValorParametro2 = string.Empty;
        public string vValorParametro3 = string.Empty;
        public string vValorParametro4 = string.Empty;

        public void ExportarPdf()
        {
            //for (int i = 0; i < dtOrigem.Rows.Count; i++)
            //{
                try
                {
                    Microsoft.Reporting.WinForms.ReportViewer rptVisualizar = new Microsoft.Reporting.WinForms.ReportViewer();

                    ReportDataSource RDS = new ReportDataSource("dsRelatorio_Dinamico", dtOrigem);
                    //rptVisualizar.Visible = true;
                    rptVisualizar.LocalReport.DataSources.Clear();
                    rptVisualizar.Reset();
                    rptVisualizar.LocalReport.ReportEmbeddedResource = "Aplicacao.Relatorios." + vNomeRlt + ".rdlc";
                    rptVisualizar.LocalReport.DisplayName = vDisplayName;
                    rptVisualizar.LocalReport.DataSources.Add(RDS);

                    rptVisualizar.SetDisplayMode(DisplayMode.PrintLayout);

                    if (vNomeParametro1.Length > 0)
                        rptVisualizar.LocalReport.SetParameters(new ReportParameter(vNomeParametro1, vValorParametro1, true));

                    if (vNomeParametro2.Length > 0)
                        rptVisualizar.LocalReport.SetParameters(new ReportParameter(vNomeParametro2, vValorParametro2, true));


                    rptVisualizar.LocalReport.Refresh();
                    rptVisualizar.LocalReport.EnableExternalImages = vContemImagem;
                    rptVisualizar.RefreshReport();



                    if (vExportaPdf)
                    {
                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string extension;

                        byte[] bytes = rptVisualizar.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                        //if (vNomeArquivo.Length == 0)
                        //{
                        //    FileStream fs = new FileStream(@"c:\SisEleva\Rps\Pdf\Nf_" + dtOrigem.Rows[i][vNomeColunaArquivo].ToString().PadLeft(6, '0') + ".pdf", FileMode.Create);
                        //    fs.Write(bytes, 0, bytes.Length);
                        //    fs.Close();
                        //    fs.Dispose();
                        //}
                        //else
                        //{
                            FileStream fsArquivo = new FileStream(vNomeArquivo, FileMode.Create);
                            fsArquivo.Write(bytes, 0, bytes.Length);
                            fsArquivo.Close();
                            fsArquivo.Dispose();
                        //}
                    }

                    rptVisualizar.Dispose();

                }
                catch (Exception EXC)
                {
                    //MessageBox.Show(EXC.Message);
                    //MessageBox.Show(EXC.StackTrace);
                }
                finally
                {

                }

            //}
        }
    }
}