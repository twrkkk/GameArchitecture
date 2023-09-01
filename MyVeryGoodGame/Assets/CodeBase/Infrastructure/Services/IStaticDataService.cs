using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.StaticData;

public interface IStaticDataService : IService
{
    MonsterStaticData ForMonster(MonsterTypeId typeId);
    void LoadMonsters();
}