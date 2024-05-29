using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEAN
{
    public class ConexoesBEAN
    {
        protected static string owner;
        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        protected static string senha;
        public string Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        protected static string host;
        public string Host
        {
            get { return host; }
            set { host = value; }
        }

        protected static string localcep;
        public string LocalCep
        {
            get { return localcep; }
            set { localcep = value; }
        }

        protected static string localcontrato;
        public string LocalContrato
        {
            get { return localcontrato; }
            set { localcontrato = value; }
        }
        

    }
}
