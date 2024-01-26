using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable() => 
        _button.onClick.AddListener(StartGame);

    private void OnDisable() =>
        _button.onClick.RemoveListener(StartGame);

    private static void StartGame() =>
        SceneManager.LoadScene(1);
}