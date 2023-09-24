namespace Infrastructure.Services
{
    public interface IService
    {
        
    }

    public interface IUpdateableService : IService
    {
        void Update();
    }
}