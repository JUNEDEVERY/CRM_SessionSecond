using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRM_Session2
{
    public partial class Employees
    {
      
            public string FullName
            {
                get
                {
                    return "" + Surname + " " + Name + " " + Patronymic;
                }
            }
       
    }
}
