using Agava.YandexGames;
using System;

public class GameAds
{
    private static GameAds _instance = new GameAds();

    private readonly int _adDelay = 4;
    private int _current = 0;

    public static GameAds Instance => _instance;

    public void ShowInterstitial(Action OnCloseCallback)
    {
        _current++;

        if (_current >= _adDelay)
        {
            _current = 0;

            InterstitialAd.Show(onCloseCallback: (status) =>
            {
                OnCloseCallback();
            });
        }
        else
        {
            OnCloseCallback();
        }
    }

    public void ShowRewardVideo(Action rewardCallback)
    {
        bool isRewarded = false;

        VideoAd.Show(onRewardedCallback: () =>
        {
            isRewarded = true;
        },
        onCloseCallback: () =>
        {
            if (isRewarded)
            {
                rewardCallback();
            }
        });
    }
}