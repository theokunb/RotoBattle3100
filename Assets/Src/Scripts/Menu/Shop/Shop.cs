using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ItemsCollectionView _template;
    [SerializeField] private GameObject _container;
    [SerializeField] private ItemsPull _items;
    [SerializeField] private BuyWindow _buyWindow;
    [SerializeField] private BackgroundAudioPlayer _backgroundAudioPlayer;
    [SerializeField] private GameObject _emptyShopTemplate;
    [SerializeField] private SpecialGood _specialGoodTemplate;
    [SerializeField] private RewardItem _rewardTemplate;

    private SpecialGood _special;
    private Type[] _detailTypes = { typeof(Head), typeof(Body), typeof(Leg), typeof(Weapon) };
    private List<ItemsCollectionView> _collections;
    private List<Detail> _details => _items.Details
            .Select(element => element.GetComponent<DetailStatus>())
            .Where(element => element.IsAvailable == false && element.CanBuyInShop == true)
            .Select(element => element.GetComponent<Detail>())
            .ToList();

    private void Awake()
    {
        _collections = new List<ItemsCollectionView>();

        _special = Instantiate(_specialGoodTemplate, _container.transform);
        _special.SetLastDate(_player.Progress.LastGamedDay);
    }

    private void OnEnable()
    {
        Subscribe();
        _special.Opened += SpecialOpened;
    }

    private void OnDisable()
    {
        _special.Opened -= SpecialOpened;
        foreach(var collection in _collections)
        {
            collection.ItemSelected -= OnItemSelected;
        }
    }

    private void Start()
    {
        if(_details.Count == 0)
        {
            Instantiate(_emptyShopTemplate, _container.transform);
            return;
        }

        foreach(var type in _detailTypes)
        {
            var selectedItems = _details
                .TakeByType(type)
                .Select(element => element.GetComponent<DetailShop>())
                .ToList();

            if(selectedItems.Count() > 0)
            {
                var collection = Instantiate(_template, _container.transform);
                collection.Render(selectedItems);
                _collections.Add(collection);
            }
        }

        Subscribe();
    }

    private void Subscribe()
    {
        foreach(var collection in _collections)
        {
            collection.ItemSelected += OnItemSelected;
        }
    }

    private void OnItemSelected(DetailView itemShopView)
    {
        _buyWindow.gameObject.SetActive(true);
        _buyWindow.Render(itemShopView);
        _buyWindow.DialogResult += OnDialogResult;
    }

    private void OnDialogResult(DetailView item,bool result)
    {
        _buyWindow.DialogResult -= OnDialogResult;

        if (result == false)
        {
            return;
        }

        var collection = item.GetComponentInParent<ItemsCollectionView>();
        collection.Remove(item);
        Destroy(item.gameObject);
        ClearEmptyCollections();
    }

    private void ClearEmptyCollections()
    {
        foreach(var collection in _collections)
        {
            if(collection.Count == 0)
            {
                Destroy(collection.gameObject);
            }
        }

        if (_details.Count == 0)
        {
            Instantiate(_emptyShopTemplate, _container.transform);
            return;
        }
    }

    private void SpecialOpened(Detail detail, DateTime dateTime)
    {
        _backgroundAudioPlayer.Pause();

        GameAds.Instance.ShowRewardVideo(() =>
        {
            detail.Unlock();
            _player.AddItem(detail.Id);
            _player.Progress.LastGamedDay = dateTime;
            GameStorage.Storage.Save(new PlayerData(_player));

            var rewardView = Instantiate(_rewardTemplate, transform);
            rewardView.Render(detail);
            rewardView.transform.localScale = Vector3.zero;
            rewardView.Tapped += OnRewardViewTapped;

            rewardView.transform.DOScale(0.7f, 0.5f).SetUpdate(true);
            _special.SetLastDate(dateTime);

            _backgroundAudioPlayer.Resume();
        });
    }

    private void OnRewardViewTapped(RewardView rewardView)
    {
        rewardView.Tapped -= OnRewardViewTapped;

        rewardView.transform.DOScale(0, 0.5f).SetUpdate(true).OnComplete(() =>
        {
            Destroy(rewardView.gameObject);
        });
    }
}

public static class DetailsShopExtension
{
    public static IEnumerable<Detail> TakeByType(this IEnumerable<Detail> items, Type type)
    {
        return items.Where(element => element.GetType() == type);
    }
}
