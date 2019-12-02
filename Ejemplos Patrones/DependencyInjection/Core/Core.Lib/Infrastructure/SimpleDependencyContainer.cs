using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Lib.Infrastructure
{
    public class SimpleDependencyContainer : IDependencyContainer
    {
        public void Register<Tin, Tout>()
        {
            throw new NotImplementedException();
        }

        public Tin Resolve<Tin>()
        {
            throw new NotImplementedException();
        }
    }
}
