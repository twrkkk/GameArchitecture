using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.Hud
{
    public class HPBar : MonoBehaviour
    {
        public Image ImageCurrent;
        public void SetValue(float curr, float max)
        {
            ImageCurrent.fillAmount = curr / max;
        }
    }
}
