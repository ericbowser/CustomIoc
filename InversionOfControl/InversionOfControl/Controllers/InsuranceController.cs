using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InversionOfControl;
using InversionOfControl.Abstract;

namespace InversionOfControl.Controllers
{
  public class InsuranceController : Controller
  {
    IInsuranceRepository insRepo;
    IUserRepository userRepo;
    IEmailRepository emailRepo;

    /// <summary>
    /// Insurance controller contructor to use contructor injection based
    /// on interface type.
    /// </summary>
    /// <param name="insuranceRepo"></param>
    public InsuranceController(IInsuranceRepository insRepo, IUserRepository userRepo, IEmailRepository emailRepo)
    {
      this.insRepo = insRepo;
      this.userRepo = userRepo;
      this.emailRepo = emailRepo;
    }

    /// <summary>
    /// return the appropriate view for the controller request
    /// </summary>
    /// <returns></returns>
    public ActionResult Index()
    {
      insRepo.Insurance = "Insurance";
      emailRepo.Email = "Email";
      userRepo.UserName = "User";
      ViewBag.user = userRepo.UserName;
      ViewBag.email = emailRepo.Email;
      ViewBag.insurance = insRepo.Insurance;
      return View();
    }

  }
}
