using Assets.CodeBase.Infrastructure.Services;
using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.Input;
using System.Runtime.CompilerServices;
using UnityEngine;
using Assets.CodeBase.Data;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        public CharacterController CharacterController;
        public float MovementSpeed = 4.0f;
        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                //Трансформируем экранные координаты вектора в мировые
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            CharacterController.Move(MovementSpeed * movementVector * Time.deltaTime);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (GetLevelName() != progress.WorldData.PositionOnLevel.LevelName) return;

            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
            if (savedPosition != null)
                Warp(savedPosition);
        }

        private void Warp(Vector3Data savedPosition)
        {
            CharacterController.enabled = false;
            transform.position = savedPosition.AsVector3().AddY(CharacterController.height);
            CharacterController.enabled = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(GetLevelName(), transform.position.AsVector3Data());
        }

        private static string GetLevelName()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}