using Enums;
using Helper;
using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject makerMenu;
        
        public void SelectGame()
        {
            EventBus.Trigger(Events.LevelStart);
        }

        public void ShowMakerMenu()
        {
            makerMenu.SetActive(true);
        }

        public void HideMakerMenu()
        {
            makerMenu.SetActive(false);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                HideMakerMenu();
        }
    }
}