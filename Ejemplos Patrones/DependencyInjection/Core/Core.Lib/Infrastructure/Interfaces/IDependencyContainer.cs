namespace Common.Lib.Infrastructure
{
    public interface IDependencyContainer
    {
        void Register<Tin, Tout>();

        Tin Resolve<Tin>();
    }
}
