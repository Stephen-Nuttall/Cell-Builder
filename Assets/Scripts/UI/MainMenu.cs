using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] TMP_Text titleTextbox;
    [SerializeField] TMP_Text startContinueButtonTextbox;
    [SerializeField] string gameTitleText = "Cell Game";
    [SerializeField] string pauseScreenText = "Paused";
    [SerializeField] string startButtonText = "Start";
    [SerializeField] string continueButtonText = "Continue";
    bool menuOpen = true;
    
    void Start()
    {
        EnableMenu();

        titleTextbox.text = gameTitleText;  // when the game starts, the title textbox should show the title of the game
        startContinueButtonTextbox.text = startButtonText;  // when the game starts, the continue button should be a start button
    }
    
    void Update()
    {
        if (Input.GetKey("escape") && !menuOpen)
        {
            EnableMenu();

            // after the game starts, the title should say paused and the start button should become a continue button
            titleTextbox.text = pauseScreenText;
            startContinueButtonTextbox.text = continueButtonText;
        }
    }
    
    void EnableMenu()
    {
        Time.timeScale = 0;  // pauses game logic
        menuPanel.SetActive(true);
        menuOpen = true;
    }

    public void DisableMenu()
    {
        Time.timeScale = 1;  // unpauses game logic
        menuPanel.SetActive(false);
        menuOpen = false;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("This button works when this is an actual executable");
    }
}
