using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using vk_test_api.Data.Models.Base;

namespace vk_test_api.Data.Models;

[Index(nameof(Login), IsUnique = true)]
public class User : BaseModel
{
    public string Login { get; set; }

    public DateTime DateCreated { get; set; }

    public string Password { get; set; }

    public Guid UserGroupId { get; set; }
    [ForeignKey("UserGroupId")]
    public virtual UserGroup Group { get; set; }

    public Guid UserStateId { get; set; }
    [ForeignKey("UserStateId")]
    public virtual UserState State { get; set; }
}