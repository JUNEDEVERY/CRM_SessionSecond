
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
    
public partial class AvailableModules
{

    public int AvailableModuleID { get; set; }

    public int ModuleID { get; set; }

    public int RoleID { get; set; }



    public virtual Modules Modules { get; set; }

    public virtual Roles Roles { get; set; }

}

}
