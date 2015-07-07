using UnityInversionOfControl.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using InversionOfControl.Models;

namespace InversionOfControl.Models
{
  /// <summary>
  /// Controller factory overriden to use container to inject dependencies
  /// </summary>
  public class ControllerFactory : DefaultControllerFactory
  {
    public ControllerFactory () 
    {
    }

    protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
    {
      return IocContainer.Container.Resolve(controllerType) as Controller;
    }
  }
}