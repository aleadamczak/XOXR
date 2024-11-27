using Animals.Produce;
using UnityEngine;

namespace Animals
{
    public class Feeding : MonoBehaviour
    {
        private hungerController hungerController;
        private GUIManager guiManager;
        public Items eatsWhat;

        void Start()
        {
            hungerController = GetComponent<hungerController>();
            guiManager = GameObject.Find("GUI Manager").GetComponent<GUIManager>();
        }

        void Update()
        {
            TapDetector.DetectTap(Feed, gameObject);
        }

        bool IsFoodSelected()
        {
            switch (guiManager.selectedItem)
            {
                case Items.Wheat:
                case Items.Carrot:
                case Items.Seeds:
                case Items.Water:
                    return true;
                default:
                    return false;
            }
        }

        void Feed()
        {
            if (!IsFoodSelected()) return;
            switch (guiManager.selectedItem)
            {
                case Items.Nothing:
                    break;
                case Items.Wheat:
                case Items.Carrot:
                case Items.Seeds:
                    TryFeed(guiManager.selectedItem);
                    break;
                case Items.Water:
                    GiveWater();
                    break;
            }
        }
        
        void TryFeed(Items itemSelected)
        {
            if (itemSelected == eatsWhat)
            {
                hungerController.Eat();
            }
        }

        void GiveWater()
        {
            hungerController.Drink();
        }
    }
}