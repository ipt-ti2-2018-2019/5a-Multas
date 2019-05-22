﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multas.Models {
   public class Viaturas {

      public Viaturas() {
         ListaDasMultas = new HashSet<Multas>();
      }

      public int ID { get; set; }

      public string Matricula { get; set; }

      public string Marca { get; set; }

      public string Modelo { get; set; }

      public string Cor { get; set; }

      public string NomeDono { get; set; }

      public string MoradaDono { get; set; }

      public string CodPostalDono { get; set; }

      // lista das multas a que uma viatura foi associada
      public virtual ICollection<Multas> ListaDasMultas { get; set; }


   }
}