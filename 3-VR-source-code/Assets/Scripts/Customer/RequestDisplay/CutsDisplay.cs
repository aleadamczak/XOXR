using UnityEngine;

namespace RequestDisplay
{
    public class CutsDisplay : MonoBehaviour
    {
        private GameObject vertical;
        private GameObject horizontal;
        private GameObject diagonal1;
        private GameObject diagonal2;
        
        private void Start()
        {
            vertical = gameObject.transform.FindChildByName("Vertical").gameObject;
            horizontal = gameObject.transform.FindChildByName("Horizontal").gameObject;
            diagonal1 = gameObject.transform.FindChildByName("Diagonal1").gameObject;
            diagonal2 = gameObject.transform.FindChildByName("Diagonal2").gameObject;
        }

        public void SetActive(bool vertical, bool horizontal, bool diagonal1, bool diagonal2)
        {
            this.vertical.SetActive(vertical);
            this.horizontal.SetActive(horizontal);
            this.diagonal1.SetActive(diagonal1);
            this.diagonal2.SetActive(diagonal2);
        }
    }
}