using DG.Tweening;
using IJunior.TypedScenes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class FinishMenu : Menu
{
    [SerializeField] private Game _game;
    [SerializeField] private RewardItem _rewardItemTemplate;
    [SerializeField] private RewardCurrencyView _rewardCurrencyTemplate;
    [SerializeField] private MenuBackground _menuBackground;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _replayButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private LoadingPanel _loadingPanel;

    private Queue<RewardView> _rewardItems= new Queue<RewardView>();
    private Bag _bag;

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(OnHomeClicked);
        _replayButton.onClick.AddListener(OnReplayClicked);
        _nextButton.onClick.AddListener(OnNextClicked);
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveListener(OnHomeClicked);
        _replayButton.onClick.RemoveListener(OnReplayClicked);
        _nextButton.onClick.RemoveListener(OnNextClicked);
    }

    private void Start()
    {
        _nextButton.gameObject.SetActive(_game.HaveNextLevel());
    }

    public void SetRewards(Bag playerBag)
    {
        _bag = playerBag;
    }

    private void DisplayItems(IEnumerable<DetailDropped> _items)
    {
        foreach(var item in _items)
        {
            var createdDisplayItem = Instantiate(_rewardItemTemplate, transform);
            createdDisplayItem.Render(item.GetDetail());
            createdDisplayItem.transform.localScale = Vector3.zero;
            createdDisplayItem.gameObject.SetActive(false);
            createdDisplayItem.Tapped += OnTapped;

            _rewardItems.Enqueue(createdDisplayItem);
        }

        CreateRewardCurrency(_game.CurrentLevel.Reward, _bag.GetCurrencies());

        if (_rewardItems.Count > 0 )
        {
            var firstreward = _rewardItems.Dequeue();
            firstreward.gameObject.SetActive(true);
            firstreward.transform.DOScale(1, 1f).SetUpdate(true);
        }
    }

    private void CreateRewardCurrency(IEnumerable<Currency> levelReward, IEnumerable<Currency> collectedReward)
    {
        var rewardWindow = Instantiate(_rewardCurrencyTemplate, transform);
        rewardWindow.RenderLevelReward(levelReward);
        rewardWindow.RenderCollectedReward(collectedReward);

        rewardWindow.transform.localScale = Vector3.zero;
        rewardWindow.gameObject.SetActive(false);
        rewardWindow.Tapped += OnTapped;

        _rewardItems.Enqueue(rewardWindow);
    }

    private void OnTapped(RewardView rewardItem)
    {
        rewardItem.transform.DOScale(0, 0.2f).SetUpdate(true).OnComplete(() =>
        {
            if (_rewardItems.Count > 0)
            {
                var reward = _rewardItems.Dequeue();
                reward.gameObject.SetActive(true);
                reward.transform.DOScale(1, 1f).SetUpdate(true);
                Destroy(rewardItem.gameObject);
            }
            else
            {
                ChangeInteractionButtons(true, _homeButton, _replayButton, _nextButton);
            }
        });
    }

    private void OnHomeClicked()
    {
        _menuBackground.CloseMenu(this);

        _loadingPanel.gameObject.SetActive(true);
        _loadingPanel.Open(MenuScene.LoadAsync());
    }

    private void OnReplayClicked()
    {
        _menuBackground.CloseMenu(this);

        _loadingPanel.gameObject.SetActive(true);
        _loadingPanel.Open(GameScene.LoadAsync(_game.CurrentLevel.Id));
    }

    private void OnNextClicked()
    {
        _menuBackground.CloseMenu(this);

        _loadingPanel.gameObject.SetActive(true);
        _loadingPanel.Open(GameScene.LoadAsync(_game.CurrentLevel.Id + 1));
    }

    public override void Activated()
    {
        ChangeInteractionButtons(false, _homeButton, _replayButton, _nextButton);
        DisplayItems(_bag.GetDetails());
    }

    private void ChangeInteractionButtons(bool status,params Button[] buttons)
    {
        foreach (var button in buttons)
        {
            button.interactable = status;
        }
    }
}
