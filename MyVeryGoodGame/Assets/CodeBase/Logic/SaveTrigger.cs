using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.States;
using UnityEngine;

namespace Assets.CodeBase.Logic
{
    public class SaveTrigger:MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;
        public BoxCollider Collider;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player") return;
            _saveLoadService.SaveProgress();
            Debug.Log("Save Progress");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!Collider) return;

            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
        }
    }
}
