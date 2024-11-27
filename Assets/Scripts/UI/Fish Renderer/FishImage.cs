using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ui.fish_renderer
{
    public class FishImage : MonoBehaviour
    {
        public GameObject fishName;
        private TMP_Text _textMesh;

        public void Awake()
        {
            _textMesh = fishName.GetComponent<TMP_Text>();
            _textMesh.text = "Default Text";
        }

        public void UpdateText(string text)
        {
            _textMesh.text = text;
        }
    }
}
