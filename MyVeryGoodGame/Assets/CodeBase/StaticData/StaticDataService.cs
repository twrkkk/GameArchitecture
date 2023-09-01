using Assets.CodeBase.StaticData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;

    public void LoadMonsters()
    {
        _monsters = Resources
            .LoadAll<MonsterStaticData>("StaticData/Monsters")
            .ToDictionary(x => x.MonsterTypeId, x => x);
    }

    public MonsterStaticData ForMonster(MonsterTypeId typeId)
    {
        return _monsters.TryGetValue(typeId, out MonsterStaticData monsterStaticData)
            ? monsterStaticData
            : null;
    }
}
