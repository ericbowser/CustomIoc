using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnityInversionOfControl.Models
{
  public class Register
  {
    /// <summary>
    /// Need to build objects and set types
    /// </summary>
    public Register(Type to, Type from, LifeCycle lifeCycle)
    {
      To = to;
      From = from;
      LifeCycle = lifeCycle;
    }

    public LifeCycle LifeCycle { get; set; }
    public Type To { get; set; }
    public Type From { get; set; }
    public object Instance { get; set; }
  }
}