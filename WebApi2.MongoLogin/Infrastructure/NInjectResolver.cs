using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Extensions.ChildKernel;

namespace WebApi2.MongoLogin.Infrastructure
{
	public class NInjectResolver : IDependencyResolver
	{
		private IKernel kernel;
		
		public NInjectResolver() : this(new StandardKernel()) { }
		
		public NInjectResolver(IKernel ninjectKernel, bool scope = false) {
			kernel = ninjectKernel;
			if(!scope) {
				AddBindings(kernel);
			}
		}

		public IDependencyScope BeginScope()
		{
			return new NInjectResolver(AddRequestBindings(new ChildKernel(kernel)), true);
		}

		public object GetService(Type serviceType)
		{
			return kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return kernel.GetAll(serviceType);
		}
		
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		private void AddBindings(IKernel kernel) {
			// Singleton and transient bindings go here
			
		}
		
		private IKernel AddRequestBindings(IKernel kernel) {
			// Request bindings go here
			return kernel;
		}
	}
}
