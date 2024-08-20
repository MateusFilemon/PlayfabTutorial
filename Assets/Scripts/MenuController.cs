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
        CreateAccount,
        RecoverAccount
    }
    public Screens currentScreen;

    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject loginScreen;
    [SerializeField] GameObject createAccountScreen;
    [SerializeField] GameObject messageScreen;
    [SerializeField] GameObject recoverAccountScreen;
    [SerializeField] TextMeshProUGUI messageTXT;

    [Header("Login Information")]
    [SerializeField] TMP_InputField inputUsernameOrEmailLogin;
    [SerializeField] TMP_InputField inputPasswordLogin;

    [Header("Create Account Information")]
    [SerializeField] TMP_InputField inputUsernameCreateAccount;
    [SerializeField] TMP_InputField inputEmailCreateAccount;
    [SerializeField] TMP_InputField inputPasswordCreateAccount;
    [SerializeField] TMP_InputField inputConfirmPasswordCreateAccount;

    [Header("Recover Account")]
    [SerializeField] TMP_InputField inputRecoverAccount;


    private void Awake()
    {
        instance = this;
        ShowScreen(Screens.Login);
        messageScreen.SetActive(false);
    }

    public void ShowScreen(Screens _screen)
    {
        currentScreen = _screen;
        loadingScreen.SetActive(false);
        loginScreen.SetActive(false);
        createAccountScreen.SetActive(false);
        recoverAccountScreen.SetActive(false);

        switch (currentScreen)
        {
            case Screens.None:
                break;

            case Screens.Loading:
                loadingScreen.SetActive(true);
                break;

            case Screens.Login:
                inputUsernameOrEmailLogin.text = "";
                inputPasswordLogin.text = "";
                inputUsernameCreateAccount.text = "";
                inputEmailCreateAccount.text = "";
                inputPasswordCreateAccount.text = "";
                inputConfirmPasswordCreateAccount.text = "";
                loginScreen.SetActive(true);
                break;

            case Screens.CreateAccount:
                createAccountScreen.SetActive(true);
                break;

            case Screens.RecoverAccount:
                recoverAccountScreen.SetActive(true);
                break;
        }
    }

    #region Login
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
            ShowMessage("Dados do usuário inválidos");
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
            Debug.Log("Nome de usuário precisa ter pelo menos 4 caracteres");
            ShowMessage("Nome de usuário precisa ter pelo menos 4 caracteres");
            ShowScreen(Screens.CreateAccount);
        }
        else if (_password.Length < 5)
        {
            ShowMessage("Senha precisa ter pelo menos 5 caracteres")
            ShowScreen(Screens.CreateAccount);
        }
        else if (_password != _confirmPassword)
        {
            Debug.Log("A senha não confere!!")
            ShowMessage("A senha não confere!!")
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

    public void BtnRecoverAccount()
    {
        PlayfabManager.instance.RecoverPassword(inputRecoverAccount.text);
    }

    public void BtnShowRecoverAccountScreen()
    {
        ShowScreen(Screens.RecoverAccount);
    }

    #endregion

    #region Others
    public void ShowMessage(string _message)
    {
        messageTXT.text = _message;
        messageScreen.SetActive(true);
    }

    public void BtnDeleteAccount()
    {
        PlayfabManager.instance.DeleteAccount();
    }

    #endregion
}
