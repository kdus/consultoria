using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaoMySql;
using MODELS;

namespace BLL
{
    public class ContaBancariaBLL
    {
       

        public static List<ContaBancariaViewModel> PopulaCombo()
        {
            ContaBancariaDaoMySql objDao = new ContaBancariaDaoMySql();
            return objDao.PopulaCombo();
        }

        public List<ContaBancariaViewModel> PopulaGrid(ContaBancariaViewModel conta)
        {
            ContaBancariaDaoMySql objDao = new ContaBancariaDaoMySql();
            return objDao.PopulaGrid(conta);
        }

        public ContaBancariaViewModel Registro(string pCodigo)
        {
            ContaBancariaDaoMySql objDao = new ContaBancariaDaoMySql();
            return objDao.Registro(pCodigo);
        }

        public List<ContaBancariaSaldoViewModel> PopulaGrid(ContaBancariaSaldoViewModel pSaldo)
        {
            ContaBancariaSaldoDaoMySql objDao = new ContaBancariaSaldoDaoMySql();
            return objDao.PopulaGrid(pSaldo);
        }

        public double Saldo(string pContaBancaria)
        {
            ContaBancariaSaldoDaoMySql objDao = new ContaBancariaSaldoDaoMySql();
            return objDao.Saldo(pContaBancaria);
        }

        public List<ContaBancariaExtratoViewModel> PopulaGridExtrato(string pContaBancaria)
        {
            ContaBancariaDaoMySql objDao = new ContaBancariaDaoMySql();
            return objDao.PopulaGridExtrato(pContaBancaria);
        }
    }
}
