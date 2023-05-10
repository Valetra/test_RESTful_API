using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using vk_test_api.Data.Models.Base;

namespace vk_test_api.Data.Models;

[Index(nameof(Login), IsUnique = true)]
public class User : BaseModel
{
    public string Login { get; set; }

    public DateTime DateCreated { get; set; }

    public string PasswordHash { get; set; }

    public Guid User_Group_Id { get; set; }
    [ForeignKey("User_Group_Id")]
    public virtual UserGroup Group { get; set; }

    public Guid User_State_Id { get; set; }
    [ForeignKey("User_State_Id")]
    public virtual UserState State { get; set; }
}