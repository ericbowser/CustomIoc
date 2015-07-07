using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InversionOfControl.Abstract;

namespace InversionOfControl.Concrete
{
  public class UserRepository : IUserRepository
  {
    public string UserName { get; set; }
  }
}