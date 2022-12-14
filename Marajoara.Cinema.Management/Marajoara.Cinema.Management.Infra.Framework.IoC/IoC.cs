using Marajoara.Cinema.Management.Infra.Framework.IoC.Extensions;
using Ninject;

namespace Marajoara.Cinema.Management.Infra.Framework.IoC
{
    public class IoC
    {
        private static readonly object obj = new object();
        private static readonly IKernel _kernel = new StandardKernel();
        private static IoC _instance;

        public IKernel Kernel { get { return _kernel; } }

        private IoC()
        {
            _kernel.BindRepositorySetup();
            _kernel.BindApplicationSetup();
            _kernel.BindMediatRSetup();
            _kernel.BindAutoMapperSetup();
        }

        public static IoC GetInstance()
        {
            lock (obj)
            {
                if (_instance == null)
                    _instance = new IoC();

                return _instance;
            }
        }

        public T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
