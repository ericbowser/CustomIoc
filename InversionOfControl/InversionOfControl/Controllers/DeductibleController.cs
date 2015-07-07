using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InversionOfControl.Abstract;

namespace InversionOfControl.Controllers
{
  public class DeductibleController : Controller
  {
    IDeductibleRepository deductibleRepo;
    IInsuranceRepository insRepo;

    /// <summary>
    /// Property controller contructor to use contructor injection based on interface type
    /// </summary>
    public DeductibleController(IDeductibleRepository deductibleRepo, IInsuranceRepository insRepo)
    {
      this.deductibleRepo = deductibleRepo;
      this.insRepo = insRepo;
    }

    public ActionResult Index()
    {
      insRepo.Insurance = "Insurance";
      ViewBag.deductible = deductibleRepo.GetDeductible();
      return View();
    }
  }
}
