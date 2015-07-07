using UnityInversionOfControl.Models;
using Xunit;
using InversionOfControl.Abstract;
using System;
using InversionOfControl.Controllers;
using System.Web.Mvc;
using InversionOfControl;
using InversionOfControl.Concrete;
using InversionOfControl.Models;

namespace InversionOfControl.IocControllerTests
{
  public class IocControllerTests
  {
    private IUserRepository userRepo;
    private IocContainer container;

    public IocControllerTests()
    {
      container = new IocContainer();
      Bootstrapper.Initialise();
    }

    /// <summary>
    /// Test the registrations using an extention
    /// </summary>
    [Fact]
    public void IocTestRegistrations()
    {
      bool actual = false;

      actual = container.IsRegistered<IInsuranceRepository>();
      Assert.Equal(true, actual);

      actual = container.IsRegistered<IEmailRepository>();
      Assert.Equal(true, actual);

      actual = container.IsRegistered<IUserRepository>();
      Assert.Equal(true, actual);

      actual = container.IsRegistered<IDeductibleRepository>();
      Assert.Equal(true, actual);

      actual = container.IsRegistered<Register>();
      Assert.Equal(false, actual);
    }

    /// <summary>
    /// Test custom IOC container registration
    /// </summary>
    [Fact]
    public void IocTestRegistrationAndResolve()
    {
      container.RegisterType<IUserRepository, UserRepository>();
      userRepo = container.Resolve<IUserRepository>();
      userRepo.UserName = "ericb";
      Assert.Equal("ericb", userRepo.UserName);
    }

    /// <summary>
    /// Test transient life cycle
    /// </summary>
    [Fact]
    public void IocTestTransient()
    {
      container.RegisterType<IUserRepository, UserRepository>(LifeCycle.Transient);
      userRepo = container.Resolve<IUserRepository>();
      userRepo.UserName = "Test1";
      userRepo = container.Resolve<IUserRepository>();
      Assert.Equal(null, userRepo.UserName);
    }

    /// <summary>
    /// Test singleton life cycle
    /// </summary>
    [Fact]
    public void IocTestSingleton()
    {
      container.RegisterType<IUserRepository, UserRepository>(LifeCycle.Singleton);
      userRepo = container.Resolve<IUserRepository>();
      userRepo.UserName = "Test1";
      userRepo = container.Resolve<IUserRepository>();
      Assert.Equal("Test1", userRepo.UserName);
    }

    /// <summary>
    /// Test to inject the User controller
    /// </summary>
    [Fact]
    public void IocTestInjectUserController()
    {
      container.RegisterType<IUserRepository, UserRepository>();
      container.RegisterType<UserController, UserController>();
      var controller = container.Resolve<UserController>();
      var result = controller.Index() as ViewResult;
      Assert.NotNull(result);
    }

    /// <summary>
    /// Test to inject the Insurance controller
    /// </summary>
    [Fact]
    public void IocTestInjectInsuranceController()
    {
      container.RegisterType<IInsuranceRepository, InsuranceRepository>();
      container.RegisterType<IUserRepository, UserRepository>();
      container.RegisterType<IEmailRepository, EmailRepository>();
      container.RegisterType<InsuranceController, InsuranceController>();
      var controller = container.Resolve<InsuranceController>();
      var result = controller.Index() as ViewResult;
      Assert.NotNull(result);
      Assert.Contains("Insurance", result.ViewBag.insurance);
    }

    /// <summary>
    /// Test to inject the Property controller
    /// </summary>
    [Fact]
    public void IocTestInjectDeductibleController()
    {
      container.RegisterType<DeductibleController, DeductibleController>();
      container.RegisterType<IDeductibleRepository, DeductibleRepository>();
      container.RegisterType<IInsuranceRepository, InsuranceRepository>();
      var controller = container.Resolve<DeductibleController>();
      var result = controller.Index() as ViewResult;
      Assert.NotNull(result);
      Assert.Contains("SomeDeductible", result.ViewBag.deductible);
    }

    /// <summary>
    /// Test invalid type exception
    /// </summary>
    [Fact]
    public void IocTestException()
    {
      try
      {
        container.Resolve<Register>();
      }
      catch (Exception ex)
      {
        Assert.Contains("The type was not found. Do you need to add a mapping?", ex.Message.ToString());
      }
    }
  }
}