using System.ComponentModel.DataAnnotations.Schema;
using vk_test_api.Data.Models.Base;

namespace vk_test_api.Data.Models;


public class User : BaseModel
{
    public string Login { get; set; }

    public DateTime DateCreated { get; set; }

    public string PasswordHash { get; set; }

    public Guid UserGroupId { get; set; }
    [ForeignKey("UserGroupId")]
    public virtual UserGroup Group { get; set; }

    public Guid UserStateId { get; set; }
    [ForeignKey("UserStateId")]
    public UserState State { get; set; }
}