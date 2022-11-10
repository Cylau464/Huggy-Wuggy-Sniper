namespace apps
{
    [System.Serializable]
    public class ADSInfo
    {
        [UnityEngine.Header("Banner")]
        public bool useBanner = true;
        public IronSourceBannerPosition bannerPosition = IronSourceBannerPosition.BOTTOM;


        [UnityEngine.Header("interstitial")]
        public bool useInterstitial = true;
        public float showInterstitialEvery = 45;

        [UnityEngine.Header("rewardedVideo")]
        public bool useRewardedVideo = true;
    }
}