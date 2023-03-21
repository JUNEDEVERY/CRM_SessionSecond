using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Session2
{
    public partial class Subscribers
    {

        public string Uslugi
        {
            get
            {
                List<ConnectedServices> services = db.tbe.ConnectedServices.Where(x => x.SubscribersID == SubscriberID).ToList();
                string strServices = "";

                for (int i = 0; i < services.Count; i++)
                {
                    if (i == services.Count - 1)
                    {
                        strServices += services[i].Services.Services1;
                    }
                    else
                    {
                        strServices += services[i].Services.Services1 + ", ";
                    }
                }
                return strServices;
            }
            set
            {

            }


        }
        public string fullNameFirstly
        {
            get
            {
                return Surname + " " + Name[0] + "." + Patronymic[0] + ".";
            }
       
        }

        public string FullName
        {
            get
            {
                return Surname + " " + Name + " " + Patronymic + " ";
            }
            set
            {
                Name = value;
                Surname = value;
                Patronymic = value;
            }
            
        }

    }
}
