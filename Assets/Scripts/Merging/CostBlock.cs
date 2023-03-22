using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Merging
{
    public class CostBlock : MonoBehaviour
    {
        public TextMeshProUGUI costText;
        public Image image;

        public void Show(int cost)
        {
            costText.text = $"{cost}";
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}