using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Text currencyUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen); 
    }

    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
    }

}
