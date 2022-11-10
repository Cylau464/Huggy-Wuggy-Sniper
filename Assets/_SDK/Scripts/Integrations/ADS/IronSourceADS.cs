using apps.exception;
using System;

namespace apps
{
    public class IronSourceADS : IADS
    {
        private bool _useBanner;
        public bool useBanner
        {
            get { return _useBanner; }
            set
            {
                if (value == true)
                    IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, bannerPosition);

                _useBanner = value;
            }
        }

        private bool _useInterstitial;
        public bool useInterstitial
        {
            get { return _useInterstitial; }
            set
            {
                if (value == true)
                    IronSource.Agent.loadInterstitial();

                _useInterstitial = value;
            }
        }

        private bool _useRewardsVideo;
        public bool useRewardedVideo
        {
            get { return _useRewardsVideo; }
            set { _useRewardsVideo = value; }
        }

        public IronSourceBannerPosition bannerPosition { get; private set; }

        protected Action _onCompletedRewardsVideo = null;
        protected Action _onClosedInterstitial = null;

        public IronSourceADS(string key, ADSInfo info)
        {
            if (key == null || key.CompareTo("") == 0)
                throw new ArgumentEmptryOrNullException();

            bannerPosition = info.bannerPosition;

            useBanner = info.useBanner;
            useInterstitial = info.useInterstitial;
            useRewardedVideo = info.useRewardedVideo;

            Intialize(key);
        }

        private void Intialize(string key)
        {
            IronSource.Agent.validateIntegration();
            IronSource.Agent.init(key);

            SubscribeEvents();

            LoadBanner();
            LoadInterstitial();
        }

        private void SubscribeEvents()
        {
            IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
            IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
            IronSourceEvents.onRewardedVideoAdRewardedEvent += OnRewardedVideoAdRewardedEvent;
        }

        public bool DisplayBanner()
        {
            if (!useBanner)
                return false;

            IronSource.Agent.displayBanner();
            return true;
        }

        public void HideBanner()
        {
            IronSource.Agent.hideBanner();
        }

        public void LoadBanner()
        {
            if (!useBanner)
                return;

            IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, bannerPosition);
        }

        public void LoadInterstitial()
        {
            if (!useInterstitial)
                return;

            IronSource.Agent.loadInterstitial();
        }

        public bool IsInterstitialAvailable()
        {
            return useInterstitial && IronSource.Agent.isInterstitialReady();
        }

        public bool ShowInterstitial(string placementName = null, Action onClose = null)
        {
            if (!IsInterstitialAvailable())
                return false;

            _onClosedInterstitial = onClose;
            IronSource.Agent.showInterstitial();
            return true;
        }

        public void LoadRewardedVideo() { }

        public bool IsRewardedideoAvailable()
        {
            return useRewardedVideo && IronSource.Agent.isRewardedVideoAvailable();
        }

        public bool ShowRewardedVideo(string placementName = null, Action onCompleted = null, Action onClose = null)
        {
            if (!IsRewardedideoAvailable())
                return false;

            _onCompletedRewardsVideo = onCompleted;
            IronSource.Agent.showRewardedVideo();
            return true;
        }

        #region call back
        private void InterstitialAdShowFailedEvent(IronSourceError error)
        {
            LoadInterstitial();
        }

        private void InterstitialAdClosedEvent()
        {
            _onClosedInterstitial?.Invoke();
            LoadInterstitial();
        }

        private void OnRewardedVideoAdRewardedEvent(IronSourcePlacement placement)
        {
            _onCompletedRewardsVideo?.Invoke();
        }
        #endregion
    }
}
