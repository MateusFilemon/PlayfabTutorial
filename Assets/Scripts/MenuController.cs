using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    public enum Screens
    {
        None,
        Loading,
        Login,
        CreateAccount

    }
    public Screens currentScreen;

    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject loginScreen;
    [SerializeField] GameObject createAccountScreen;

    [Header("Login Information")]
    [SerializeField] TMP_InputField inputUsernameOrEmailLogin;
    [SerializeField] TMP_InputField inputPasswordLogin;

    [Header("Create Account Information")]
    [SerializeField] TMP_InputField inputUsernameCreateAccount;
    [SerializeField] TMP_InputField inputEmailCreateAccount;
    [SerializeField] TMP_InputField inputPasswordCreateAccount;
    [SerializeField] TMP_InputField inputConfirmPasswordCreateAccount;


    private void Awake()
    {
        instance = this;
        ShowScreen(Screens.Login);
    }

    public void ShowScreen(Screens _screen)
    {
        currentScreen = _screen;
        loadingScreen.SetActive(false);
        loginScreen.SetActive(false);
        createAccountScreen.SetActive(false);

        switch (currentScreen)
        {
            case Screens.None:
                break;

            case Screens.Loading:
                loadingScreen.SetActive(true);
                break;

            case Screens.Login:
                loginScreen.SetActive(true);
                break;

            case Screens.CreateAccount:
                createAccountScreen.SetActive(true);
                break;
        }
    }

    public void BtnLogin()
    {

    }
    public void BtnCreateAccount()
    {

    }

    public void BtnBackToLogin()
    {
        ShowScreen(Screens.Login);
    }

    public void BtnGoToCreateAccount()
    {
        ShowScreen(Screens.CreateAccount);
    }
}
