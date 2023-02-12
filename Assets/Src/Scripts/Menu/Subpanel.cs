using UnityEngine;
using UnityEngine.UI;

public class Subpanel : MonoBehaviour
{
    [SerializeField] private Button _backButton;

    private Subpanel _subpanel;
    private Subpanel _parentPanel;

    private void OnEnable()
    {
        _backButton?.onClick.AddListener(BackClick);
    }

    private void OnDisable()
    {
        _backButton?.onClick.RemoveListener(BackClick);
    }

    private void Start()
    {
        _subpanel = GetComponent<Subpanel>();
    }

    public void Push(Subpanel subpanel, Subpanel parent)
    {
        _parentPanel = parent;
        _subpanel = subpanel;

        _parentPanel.gameObject.SetActive(false);
        _subpanel.gameObject.SetActive(true);
    }

    private void BackClick()
    {
        _subpanel.gameObject.SetActive(false);
        _parentPanel.gameObject.SetActive(true);
    }
}
