using InversionOfControl.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InversionOfControl.Controllers
{
  public class UserController : Controller
  {
    /// <summary>
    /// Insurance Repo Interface
    /// </summary>
    public IUserRepository userRepo;

    /// <summary>
    /// Property controller contructor to use contructor injection based on interface type
    /// </summary>
    public UserController(IUserRepository userRepo)
    {
      this.userRepo = userRepo;
    }

    public ViewResult Index()
    {
      //dynamic expression -> Will be resolved at runtime
      userRepo.UserName = "ericb";
      ViewBag.userName = userRepo.UserName;
      return View();
    }
  }
}
