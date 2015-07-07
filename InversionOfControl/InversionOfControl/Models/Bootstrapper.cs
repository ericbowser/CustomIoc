using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InversionOfControl.Concrete;
using InversionOfControl.Controllers;
using InversionOfControl.Abstract;
using InversionOfControl.Models;

namespace InversionOfControl.Models
{
  public class Bootstrapper : DefaultControllerFactory
  {
    public static IocContainer Initialise()
    {
      return BuildContainerWithRegistration();
    }

    private static IocContainer BuildContainerWithRegistration()
    {
      var container = new IocContainer();
      container.RegisterType<IUserRepository, UserRepository>();
      container.RegisterType<IInsuranceRepository, InsuranceRepository>();
      container.RegisterType<IEmailRepository, EmailRepository>();
      container.RegisterType<IDeductibleRepository, DeductibleRepository>();
      container.RegisterType<UserController, UserController>();
      container.RegisterType<DeductibleController, DeductibleController>();
      container.RegisterType<InsuranceController, InsuranceController>();
      IocContainer.Container = container;
      return container;
    }
  }
}