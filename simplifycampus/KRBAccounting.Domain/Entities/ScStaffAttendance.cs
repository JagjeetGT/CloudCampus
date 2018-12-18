using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class ScStaffAttendance{        [Key]        public int Id {get;set;}        public int StaffId {get;set;}        public DateTime Date {get;set;}        public DateTime CreatedDate {get;set;}        public string Status {get;set;}        public int AcademicYearId {get;set;}    }}