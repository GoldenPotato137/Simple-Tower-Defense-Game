using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject makerMenu;
        
        public void SelectGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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