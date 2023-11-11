using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LearnPractice.Models.Database;
using MessagePack;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;

namespace LearnPractice.Areas.Identity.Data;

// Add profile data for application users by adding properties to the LearnPracticeUser class
public class LearnPracticeUser : IdentityUser
{
   
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { set; get; }
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { set; get; }
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string MiddleName { set; get; }
    [PersonalData]
    [Column(TypeName = "nvarchar(15)")]
    public string? Phone { get; set; }
     
    
    public List<IdentityUserRole<string>> Roles { get; set; }

}

