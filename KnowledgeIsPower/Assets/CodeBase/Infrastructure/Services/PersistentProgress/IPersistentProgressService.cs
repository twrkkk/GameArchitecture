using CodeBase.Data;
using Assets.CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}