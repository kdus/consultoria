using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

using BEAN;
using DaoMySql;

using MODELS;

namespace BLL
{
    public class ContasPagarBLL
    {
        #region DML 
        public string Gravar(ContasPagarViewModel pObjBean)
        {

            if (pObjBean.ParcelaX == null || pObjBean.ParcelaY == null)
                throw new Exception ("Por favor informar os campos Parcela De/Até");

            string vUltimoCodigo = "0";
            DateTime vDataVencimento = Convert.ToDateTime(pObjBean.Vencimento);
            int vMes = Convert.ToInt32(pObjBean.MesReferencia);
            int? vAno = Convert.ToInt32(pObjBean.AnoReferencia);
            int? vParcelas = (pObjBean.ParcelaY - pObjBean.ParcelaX) + 1;

            if (pObjBean.Codigo == 0)
            {
                for (int i = 0; i < vParcelas; i++)
                {
                    ContaPagarDaoMySql objDao = new ContaPagarDaoMySql();

                    if (i == 0)
                    {
                        vUltimoCodigo = objDao.Gravar(pObjBean);
                    }
                    else
                    {
                        vDataVencimento = vDataVencimento.AddMonths(1);

                        if (pObjBean.MesReferencia > 0)
                        {
                            if (vMes > 0 && vMes == 12)
                            {
                                vMes = 1;
                                vAno++;
                            }
                            else
                            {
                                vMes++;
                            }

                            pObjBean.MesReferencia = vMes;
                            pObjBean.AnoReferencia = vAno;
                        }

                        pObjBean.Vencimento = vDataVencimento;


                        pObjBean.ParcelaX = pObjBean.ParcelaX + 1;

                        vUltimoCodigo = objDao.Gravar(pObjBean);
                    }

                    
                }

                return vUltimoCodigo;
            }
            else
            {
                ContaPagarDaoMySql objDao = new ContaPagarDaoMySql();
                return objDao.Gravar(pObjBean);
            }
        }
        #endregion

        #region PopulaGrid
        public DataTable PopulaGrid(string pFiltro)
        {
            ContaPagarDaoMySql objDao = new ContaPagarDaoMySql();
            return objDao.PopulaGrid(pFiltro); 
        }
        #endregion

        public List<ContasPagarViewModel> PopulaGrid(ContasPagarViewModel conta)
        {
            ContaPagarDaoMySql objDao = new ContaPagarDaoMySql();
            return objDao.PopulaGrid(conta);
        }

        public ContasPagarViewModel Registro(string pCodigo)
        {
            ContaPagarDaoMySql objDao = new ContaPagarDaoMySql();
            return objDao.Registro(pCodigo);
        }

        public void Excluir(string pCodigo)
        {
            ContaPagarDaoMySql objDao = new ContaPagarDaoMySql();
            objDao.Excluir(pCodigo);
        }

        public void BaixaLote(string pCodigo, string pUsuario)
        {
            ContaPagarDaoMySql objDao = new ContaPagarDaoMySql();
            objDao.BaixaLote(pCodigo, pUsuario);
        }

        public List<ContasPagarViewModel> ImprimirReceita(ContasPagarViewModel conta)
        {
            ContaPagarDaoMySql objDao = new ContaPagarDaoMySql();
            return objDao.ImprimirReceita(conta);
        }

    }
}
