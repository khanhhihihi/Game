using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirebaseLoginManager : MonoBehaviour
{
    [Header("Register")]
    public InputField ipRegisterEmail;
    public InputField ipRegisterPassword;

    public Button buttonRegister;
    [Header("Sign In")]
    public InputField ipLoginEmail;
    public InputField ipLoginPassword;

    public Button buttonLogin;

    private FirebaseAuth auth;

    [Header("Switch Form")]
    public Button buttonMoveToLogin;
    public Button buttonMoveToRegister;

    public GameObject LoginForm;
    public GameObject RegisterForm;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        buttonRegister.onClick.AddListener(RegisterAccountWithFirebase);
        buttonLogin.onClick.AddListener(SignInAccountWithFirebase);

        buttonMoveToLogin.onClick.AddListener(SwitchForm);
        buttonMoveToRegister.onClick.AddListener(SwitchForm);
    }
    public void RegisterAccountWithFirebase()
    {
        string email = ipRegisterEmail.text;
        string password = ipRegisterPassword.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("dang ky bi huy");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("dang ky that bai");
                return;
            }
            if(task.IsCompleted)
            {
                Debug.Log("dang ky thanh cong");
                return;
            }
        });
    }

    public void SignInAccountWithFirebase()
    {
        string email = ipLoginEmail.text;
        string password = ipLoginPassword.text;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("dang nhap bi huy");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("dang nhap that bai");
                return;
            }
            if (task.IsCompleted)
            {
                Debug.Log("dang nhap thanh cong");
                FirebaseUser user = task.Result.User;

                SceneManager.LoadScene("Menu");
            }
        });
    }
    public void SwitchForm()
    {
        LoginForm.SetActive(!LoginForm.activeSelf);
        RegisterForm.SetActive(!RegisterForm.activeSelf);

    }
}
