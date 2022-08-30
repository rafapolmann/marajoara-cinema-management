using AutoMapper;
using Marajoara.Cinema.Management.Application;
using Ninject;

namespace Marajoara.Cinema.Management.Infra.Framework.IoC.Extensions
{
    public static class AutoMapperExtensions
    {
        public static void BindAutoMapperSetup(this IKernel kernel)
        {
            // Add all profiles in current assembly          
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(AppModule).Assembly); });
            kernel.Bind<IMapper>().ToConstructor(c => new Mapper(mapperConfiguration)).InSingletonScope();
        }
    }
}
