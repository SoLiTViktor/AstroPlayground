using AsteroidProject;
using System;
using UnityEngine;
using Zenject;

public class Interstitial : IInitializable, IDisposable
{
    private SignalBus _signalBus;

    private string _adUnitId = "d9a62d3d22828eee";

    private int _retryAttempt = 0;

    public event Action<int> Retry;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        SubscribeToSignalBusEvents();
        SubscribeToMaxSdkCallbacks();
    }

    public void Dispose()
    {
        UnsubscribeToSignalBusEvents();
        UnsubscribeToMaxSdkCallbacks();
    }

    public void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(_adUnitId);
    }

    private void SubscribeToSignalBusEvents()
    {
        _signalBus.Subscribe<PlayerCrushedSignal>(LoadInterstitial);
    }
    private void UnsubscribeToSignalBusEvents()
    {
        _signalBus.Unsubscribe<PlayerCrushedSignal>(LoadInterstitial);
    }

    private void SubscribeToMaxSdkCallbacks()
    {
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;
    }

    private void UnsubscribeToMaxSdkCallbacks()
    {
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent -= OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent -= OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent -= OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent -= OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent -= OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent -= OnInterstitialAdFailedToDisplayEvent;
    }


    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        MaxSdk.ShowInterstitial(adUnitId);
        _retryAttempt = 0;
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        int delayRatioBase = 2;
        int delayRationMaxGrade = 6;

        _retryAttempt++;
        double retryDelay = Math.Pow(delayRatioBase, Math.Min(delayRationMaxGrade, _retryAttempt));

        Retry?.Invoke(_retryAttempt);

        Debug.Log("Interstitial load failed");
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    {
        Debug.Log("Interstitial displayed");
    }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        LoadInterstitial();
        Debug.Log("Interstitial ad failed to display");
    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    {
        Debug.Log("Interstitial clicked");
    }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Debug.Log("Interstitial hidden");

    }
}
