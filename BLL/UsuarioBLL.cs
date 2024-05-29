using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

using BEAN;
using DaoMySql;

namespace BLL
{
    public class UsuarioBLL
    {
        #region Incluir Usuario
       
        #endregion


        #region Metodo que retorna a Permissao de Visualizar, Gravar e Leitura do usuario
        public void PermissaoTela(int codigo, string tela, out bool visivel, out bool escreve, out bool leitura)
        {           
            UsuarioDaoMySql objUsuarioDaoMySql = new UsuarioDaoMySql();
            objUsuarioDaoMySql.PermissaoTela(codigo, tela, out visivel, out escreve, out leitura);    
            
        }
        #endregion

        #region Alterar
       
        #endregion

        #region Excluir Usuario
       
        #endregion

        #region Popula Grid
        public DataTable PopulaGrid(string pFiltro)
        {           
            UsuarioDaoMySql objUsuarioDaoMySql = new UsuarioDaoMySql();
            return objUsuarioDaoMySql.PopulaGrid(pFiltro);            
        }
        #endregion

        public UsuarioBEAN ListarPorId(int pCodigo)
        {
            UsuarioDaoMySql objUsuario = new UsuarioDaoMySql();
            return objUsuario.ListarPorId(pCodigo);
        }

        public UsuarioBEAN ListarPorNomeSenha(string pNome, string pSenha)
        {
            UsuarioDaoMySql objUsuario = new UsuarioDaoMySql();
            return objUsuario.ListarPorNome(pNome, pSenha);
        }


        #region Metodo que retorna se existe usuario criado por codigo de funcioario

        #endregion

        #region Metodo que retorna o nivel de acesso, paramentro codigo
        public string Nivel(int codigo)
        {
           
                UsuarioDaoMySql objUsuarioDaoMySql = new UsuarioDaoMySql();
                return objUsuarioDaoMySql.Nivel(codigo);    
 
            
        }
        #endregion

        #region Alterar Senha de usuario primeiro acesso
      
        #endregion

        #region Metodo que retorna a quantidade de acesso, paramentro codigo
        public int QtdeAcesso(int codigo)
        {
          
                UsuarioDaoMySql objUsuarioDaoMySql = new UsuarioDaoMySql();
                return objUsuarioDaoMySql.QtdeAcesso(codigo);                 
            
        }
        #endregion

        #region GetUsuarioFuncionario
        public DataTable GetUsuarioFuncionario(string pFiltro)
        {

            UsuarioDaoMySql objUsuarioDaoMySql = new UsuarioDaoMySql();
            return objUsuarioDaoMySql.GetUsuarioFuncionario(pFiltro);

        }
        #endregion


        public static string Permissao(object pSessao, string pUsuario, string pTela, string pNivel)
        {            

            if (pNivel.Equals("S"))
                return string.Empty;

            if (pSessao == null)
                return "~/Views/Usuario/Login.cshtml";
            else
            {
                UsuarioPermissaoBLL objUsuarioBll = new UsuarioPermissaoBLL();
                var vUsuario = objUsuarioBll.Registro(pUsuario, pTela);

                if (vUsuario == null)
                    return "~/Views/Usuario/AcessoNaoPermitido.cshtml";
                else
                    return string.Empty;

            }
        }
    }
}
