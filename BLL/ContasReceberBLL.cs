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
    public class ContasReceberBLL
    {
        #region DML 
        public string Gravar(ContasReceberViewModel pObjBean)
        {
            string vUltimoCodigo = "0";
            DateTime vDataVencimento = Convert.ToDateTime(pObjBean.Vencimento);
            int vMes = Convert.ToInt32(pObjBean.MesReferencia);
            int? vAno = Convert.ToInt32(pObjBean.AnoReferencia);
            int? vParcelas = (pObjBean.ParcelaY - pObjBean.ParcelaX) + 1;

            if (pObjBean.Codigo == 0)
            {
                for (int i = 0; i < vParcelas; i++)
                {
                    ContasReceberDaoMySql objDao = new ContasReceberDaoMySql();

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
                ContasReceberDaoMySql objDao = new ContasReceberDaoMySql();
                return objDao.Gravar(pObjBean);
            }
        }
        #endregion

        #region PopulaGrid
        public DataTable PopulaGrid(string pFiltro)
        {
            ContaPagarDaoMySql objContasPagarDaoMy = new ContaPagarDaoMySql();
            return objContasPagarDaoMy.PopulaGrid(pFiltro); 
        }
        #endregion

        public List<ContasReceberViewModel> PopulaGrid(ContasReceberViewModel conta)
        {
            ContasReceberDaoMySql objDao = new ContasReceberDaoMySql();
            return objDao.PopulaGrid(conta);
        }

        public ContasReceberViewModel Registro(string pCodigo)
        {
            ContasReceberDaoMySql objDao = new ContasReceberDaoMySql();
            return objDao.Registro(pCodigo);
        }

        public void Excluir(ContasReceberViewModel pObjBean)
        {
            ContasReceberDaoMySql objDao = new ContasReceberDaoMySql();
            objDao.Excluir(pObjBean);
        }

        public void BaixaLote(string pCodigo, string pUsuario)
        {
            ContasReceberDaoMySql objDao = new ContasReceberDaoMySql();
            objDao.BaixaLote(pCodigo, pUsuario);
        }

        public List<ContasReceberViewModel> ImprimirReceita(ContasReceberViewModel conta)
        {
            ContasReceberDaoMySql objDao = new ContasReceberDaoMySql();
            return objDao.ImprimirReceita(conta);
        }
    }
}
