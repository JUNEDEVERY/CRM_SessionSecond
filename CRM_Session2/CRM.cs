
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace CRM_Session2
{

using System;
    using System.Collections.Generic;
    
public partial class CRM
{

    public int CRMID { get; set; }

    public string NumberCRM { get; set; }

    public System.DateTime DateCreation { get; set; }

    public int SubscriberID { get; set; }

    public int ServicesID { get; set; }

    public int TypeOfServiceID { get; set; }

    public int ServiceTypeID { get; set; }

    public int ServiceStatusID { get; set; }

    public int ProblemTypeID { get; set; }

    public string Description { get; set; }

    public Nullable<System.DateTime> ClosingDate { get; set; }



    public virtual ProblemTypes ProblemTypes { get; set; }

    public virtual Services Services { get; set; }

    public virtual ServiceStatus ServiceStatus { get; set; }

    public virtual ServiceTypes ServiceTypes { get; set; }

    public virtual Subscribers Subscribers { get; set; }

    public virtual TypeOfServices TypeOfServices { get; set; }

}

}
