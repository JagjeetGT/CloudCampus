using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
  public  class Design
    {

      public int Id { get; set; }
      public string Name { get; set; }
      public string Module { get; set; }
      public string Header { get; set; }
      public string Body { get; set; }
      public string Footer { get; set; }

    }
}
