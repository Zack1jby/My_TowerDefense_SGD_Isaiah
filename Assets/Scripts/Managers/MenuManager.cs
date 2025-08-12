using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;

    private void Awake()
    {
        ActivateTitleScreen();
    }

    public void SetTitleScreenActive(bool active)
    {
        titleScreen.SetActive(active);
    }

    public void SetMainMenuActive(bool active)
    {
        mainMenu.SetActive(active);
    }

    public void SetOptionsMenuActive(bool active)
    {
        optionsMenu.SetActive(active);
    }

    public void ActivateTitleScreen()
    {
        DeactiveAllMenus();
        SetTitleScreenActive(true);
    }

    public void ActivateMainMenu()
    {
        DeactiveAllMenus();
        SetMainMenuActive(true);
    }

    public void ActivateOptionsMenu()
    {
        DeactiveAllMenus();
        SetOptionsMenuActive(true);
    }

    public void DeactiveAllMenus()
    {
        SetTitleScreenActive(false);
        SetMainMenuActive(false);
        SetOptionsMenuActive(false);
    }
}
