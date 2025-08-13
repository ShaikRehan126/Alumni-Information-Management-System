using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyMVCMappingDEMO.Areas.Identity.Data;

// Add profile data for application users by adding properties to the MyMVCMappingDEMOUser class
public class MyMVCMappingDEMOUser : IdentityUser
{
    public string FUllName{ get; set; }
}

