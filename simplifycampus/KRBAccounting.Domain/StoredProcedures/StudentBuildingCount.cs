using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
  public  class StudentBuildingCount
  {

      public int BuildingId { get; set; }
      public int RoomId { get; set; }
      public string BuildingName { get; set; }
      public string RoomName { get; set; }
      public int ClassId { get; set; }
      public string ClassName { get; set; }
      public int Total { get; set; }



  }
}
