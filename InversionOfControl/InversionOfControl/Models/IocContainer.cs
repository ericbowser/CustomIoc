using UnityInversionOfControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InversionOfControl.Abstract;

namespace InversionOfControl.Models
{
  /// <summary>
  /// Some referenced code from: https://timross.wordpress.com/2010/01/21/creating-a-simple-ioc-container/
  /// </summary>
  public class IocContainer : IIocContainer
  {
    readonly List<Register> registrations;

    public IocContainer()
    {
      registrations = new List<Register>();
    }

    /// <summary>
    /// The container 
    /// </summary>
    public static IocContainer Container { get; set; }

    /// <summary>
    /// Add registration by creating a new object to reference parts
    /// </summary>
    /// <param name="from">Type from</param>
    /// <param name="to">Type to</param>
    /// <param name="lifeCycle">LifeCycle lifeCycle</param>
    public void Register(Type from, Type to, LifeCycle lifeCycle = LifeCycle.Transient)
    {
      registrations.Add(new Register(to, from, lifeCycle));
    }

    /// <summary>
    /// Register type with generic types to then call Register 
    /// </summary>
    /// <typeparam name="F">From</typeparam>
    /// <typeparam name="T">To</typeparam>
    /// <param name="lifeCycle">LifeCycle lifeCycle</param>
    public void RegisterType<F, T>(LifeCycle lifeCycle = LifeCycle.Transient)
    {
      try
      {
        Register(typeof(F), typeof(T), lifeCycle);
      }
      catch 
      {
      }
    }

    /// <summary>
    /// Determine if the type is registered using generic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public bool IsRegistered<T>()
    {
      return IsRegistered(typeof(T));
    }

    /// <summary>
    /// Check the container for specific type registration
    /// </summary>
    /// <param name="type">Type type</param>
    /// <returns>bool</returns>
    public bool IsRegistered(Type type)
    {
      if (type == null)
        throw new ArgumentNullException(type.Name);

      object[] isTrue = IocContainer.Container.registrations.Where(x => x.From == type).ToArray();

      return isTrue.Count() == 0 ? false : true;
    }

    /// <summary>
    /// Resolve by passing a type and returning an instance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Resolve<T>()
    {
      object instance = null;
      instance = Resolve(typeof(T));
      return (T)instance;
    }

    /// <summary>
    /// Get the first instance where types match and return an instance of it if not null
    /// </summary>
    /// <param name="type">Type type</param>
    /// <param name="instanceName">string instanceName</param>
    /// <returns>object</returns>
    public object Resolve(Type type, string instanceName = null)
    {
      var instance = registrations.Where(x => x.From == type).FirstOrDefault();
      if (instance != null)
      {
        return GetInstance(instance);
      }
      else 
      {
        throw new InvalidOperationException("The type was not found. Do you need to add a mapping?");
      }
    }

    /// <summary>
    /// Get an instance based on life cycle or state
    /// </summary>
    /// <param name="regObj">Register regObj</param>
    /// <returns>object</returns>
    private object GetInstance(Register regObj)
    {
      if (regObj.Instance == null || regObj.LifeCycle == LifeCycle.Transient)
      {
        List<object> ctor = ResolveCtorDependencies(regObj);
        regObj.Instance = Activator.CreateInstance(regObj.To, ctor.ToArray());
      }

      return regObj.Instance;
    }

    /// <summary>
    /// Resolve contructor dependencies to get an instance
    /// </summary>
    /// <param name="regObj"></param>
    /// <param name="list"></param>
    private List<object> ResolveCtorDependencies(Register regObj)
    {
      List<object> list = new List<object>();
      var constructorInfo = regObj.To.GetConstructors().First();
      foreach (var parameter in constructorInfo.GetParameters())
      {
        object obj = new object();
        obj = Resolve(parameter.ParameterType);
        list.Add(obj);
      }

      return list;
    }
  }
}