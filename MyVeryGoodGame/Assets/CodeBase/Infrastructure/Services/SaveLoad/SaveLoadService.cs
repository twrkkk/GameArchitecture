
using Assets.CodeBase.Infrastructure.States;
using CodeBase.Data;
using UnityEngine;
using Assets.CodeBase.Data;
using System.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;

namespace Assets.CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public PlayerProgress LoadProgress()
        {
            string json = PlayerPrefs.GetString(ProgressKey);
            //Debug.Log(json);
            PlayerProgress progress = json?.ToDeserialized<PlayerProgress>();
            return progress;
        }

        public void SaveProgress()
        {
            foreach (var progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.PlayerProgress);

            PlayerPrefs.SetString(ProgressKey, _progressService.PlayerProgress.ToJson());
        }
    }
}
