using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace MODELS
{

	public class PessoaUsuarioViewModel : DmlViewModel
	{

		public string Pessoa { get; set; }
		public string Usuario { get; set; }
		public string Email { get; set; }


		public List<PessoaViewModel> Usuarios { get; set; }
	}
}