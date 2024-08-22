using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    public enum Screens
    {
        None,
        Loading,
        Login,
        CreateAccount,
        RecoverAccount,
        Ranking
    }
    public Screens currentScreen;

    [Header("Screens")]
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject loginScreen;
    [SerializeField] GameObject createAccountScreen;
    [SerializeField] GameObject messageScreen;
    [SerializeField] GameObject recoverAccountScreen;
    [SerializeField] GameObject rankingScreen;
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

    [Header("Rankings")]
    [SerializeField] string rankingName;
    [SerializeField] TMP_InputField rankingValue;
    [SerializeField] TextMeshProUGUI rankingTxt;
    [SerializeField] TextMeshProUGUI rankingListTxt;

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
        rankingScreen.SetActive(false);

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

            case Screens.Ranking:
                rankingScreen.SetActive(true);
                break;
        }
    }

    #region Login
    public void BtnLogin()
    {
        ShowScreen(Screens.Loading);

        string _usernameOrEmail = inputUsernameOrEmailLogin.text;
        string _password = inputPasswordLogin.text;

        if (string.IsNullOrEmpty(_usernameOrEmail) ||
            string.IsNullOrEmpty(_password))
        {
            ShowMessage("Favor preencher todos os campos!");
            ShowScreen(Screens.Login);
        }
        else if (_usernameOrEmail.Length < 3)
        {
            ShowMessage("Dados de usuário inválidos!");
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

        string _username = inputUsernameCreateAccount.text;
        string _email = inputEmailCreateAccount.text;
        string _password = inputPasswordCreateAccount.text;
        string _confirmPassword = inputConfirmPasswordCreateAccount.text;

        if (string.IsNullOrEmpty(_username) ||
            string.IsNullOrEmpty(_email) ||
            string.IsNullOrEmpty(_password) ||
            string.IsNullOrEmpty(_confirmPassword))
        {
            Debug.Log("Favor preencher todos os campos!");
            ShowMessage("Favor preencher todos os campos!");
            ShowScreen(Screens.CreateAccount);
        }
        else if (_username.Length < 3)
        {
            Debug.Log("Username precisa ter ao menos 3 caracteres");
            ShowMessage("Username precisa ter ao menos 3 caracteres");
            ShowScreen(Screens.CreateAccount);
        }
        else if (_password != _confirmPassword)
        {
            Debug.Log("A senha não confere! Favor verificar a senha digitada!");
            ShowMessage("A senha não confere! Favor verificar a senha digitada!");
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
        PlayfabManager.instance.Recoverpassword(inputRecoverAccount.text);
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

    public void UpdateRanking()
    {
        if (string.IsNullOrEmpty(rankingValue.text))
        {
            ShowMessage("Favor informar o valor a ser atualizado!");
            return;
        }
        ShowScreen(Screens.Loading);
        PlayfabManager.instance.UpdatePlayerScore(rankingName, int.Parse(rankingValue.text));


    }

    public void UpdatePlayerRanking(int _value)
    {
        rankingTxt.text = "Ranking Atual: " + _value.ToString();
    }

    public void UpdateRankingList(string _list)
    {
        rankingListTxt.text = _list;
        ShowScreen(Screens.Ranking);
    }

    public void StartGame()
    {
        ShowScreen(Screens.Loading);
        PlayfabManager.instance.GetLeaderboard(rankingName);
    }
}
