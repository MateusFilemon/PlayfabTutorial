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
        ShowScreen(Screens.Loading);
        string _usernameOrEmail = usernameEmailLoginInput.text;
        string _password = passwordLoginInput.text;

        if (string.IsNullOrEmpty(_usernameOrEmail) || string.IsNullOrEmpty(_password))
        {
            ShowMessage("Preencha todos os campos");
            ShowScreen(Screens.Login);
        }
        else if (_usernameOrEmail.Length < 4)
        {
            ShowMessage("Dados do usu�rio inv�lidos");
            ShowScreen(Screens.Login);
        }
        else
        {
            PlayfabManager.instance.UserLogin(_usernameOrEmail, _password);
        }
    }

    public void BtnCreateAccount()
    {
        ShowScreen(Screens.Loading);

        string _username = usernameInput.text;
        string _email = emailInput.text;
        string _password = passwordInput.text;
        string _confirmPassword = confirmPasswordInput.text;

        if (string.IsNullOrEmpty(_username) ||
            string.IsNullOrEmpty(_email) ||
            string.IsNullOrEmpty(_password) ||
            string.IsNullOrEmpty(_confirmPassword))
        {

            Debug.Log("Por favor preencher todos os campos");
            ShowMessage("Por favor preencher todos os campos");
            ShowScreen(Screens.CreateAccount);
        }
        else if (_username.Length < 4)
        {
            Debug.Log("Nome de usu�rio precisa ter pelo menos 4 caracteres");
            ShowMessage("Nome de usu�rio precisa ter pelo menos 4 caracteres");
            ShowScreen(Screens.CreateAccount);
        }
        else if (_password.Length < 5)
        {
            ShowMessage("Senha precisa ter pelo menos 5 caracteres")
            ShowScreen(Screens.CreateAccount);
        }
        else if (_password != _confirmPassword)
        {
            Debug.Log("A senha n�o confere!!")
            ShowMessage("A senha n�o confere!!")
            ShowScreen(Screens.CreateAccount);
        }
        else
        {
            PlayfabManager.instance.CreateAccount(_username, _email, _password);
        }
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
