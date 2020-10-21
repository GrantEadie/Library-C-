using System.Collections.Generic;

namespace Library.Models
{
  public class ManageUsersViewModel
  {
    public ApplicationUser[] Administrators { get; set; }
    public ApplicationUser[] Everyone { get; set; }
  }
}