using Assets.CodeBase.Infrastructure.Services;
using CodeBase.Data;

namespace Assets.CodeBase.Infrastructure.States
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}