using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    [SerializeField] private PlayerTutorial _playerTutorial;
    [SerializeField] private Tutorial _tutotailPanel;
    [SerializeField] private GameObject _skipTutorialButton;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        _playerTutorial.IsMenuTutorialCompleted = false;
        _tutotailPanel.gameObject.SetActive(true);
        _skipTutorialButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
