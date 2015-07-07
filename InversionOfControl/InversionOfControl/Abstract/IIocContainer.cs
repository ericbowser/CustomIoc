using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityInversionOfControl.Models;

namespace InversionOfControl.Abstract
{
  public interface IIocContainer
  {
    void RegisterType<F, T>(LifeCycle lifecycle);
    bool IsRegistered<T>();
    T Resolve<T>();
  }
}
