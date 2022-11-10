using System.Collections.Generic;
using UnityEngine;

namespace apps
{
    public class AppsIntegration : MonoBehaviour
    {
        public readonly static string appSettingsName = "AppsSettings";
        protected List<IApplication> m_Applications = new List<IApplication>();
        protected bool m_IsInited { get; private set; }

        protected void Awake()
        {
            if (OnLoaded())
            {
                m_IsInited = Initialize();
            }
        }

        private bool OnLoaded()
        {
            if (m_IsInited == false)
            {
                DontDestroyOnLoad(gameObject);
                return true;
            }
            else
            {
                Destroy(gameObject);
                return false;
            }
        }

        protected bool Initialize()
        {
            AppsSettings settings = Resources.Load<AppsSettings>(appSettingsName);
            if (settings == null)
                throw new System.NullReferenceException("AppsSettings not found in resource folder!");

#if UNITY_IOS
            RequestAuthorizations.RequestAuthorizationsIOS();
#endif

            if (settings.integrateFacebook)
            {
                m_Applications.Add(new FacebookApp());
            }

            if (settings.integrateIronSource)
            {
                #region choice key
#if UNITY_ANDROID
            string appKey = settings.androidKey;
#elif UNITY_IPHONE
                string appKey = settings.iosKey;
#else
            string appKey = "unexpected_platform";
#endif
                #endregion

                #region create ad maker
                IADS adsMaker = null;
#if UNITY_EDITOR
                if (settings.showADSType == ShowADSType.Simulator)
                    adsMaker =
                        new ADSSimulator(
                        appKey,
                        settings.adsInfo.useBanner,
                        settings.adsInfo.useInterstitial,
                        settings.adsInfo.useRewardedVideo
                        );
                else
                    adsMaker = new ADSDebugger(appKey);

#elif UNITY_ANDROID || UNITY_IPHONE
                adsMaker = new IronSourceADS(appKey, settings.adsInfo);
#endif
                #endregion

                ADSManager.Initialize(adsMaker, true, settings.adsInfo.showInterstitialEvery);
                m_Applications.Add(adsMaker);
            }

            if (settings.integrateGameAnalytics)
            {
                GameAnalyticsEvents GA = new GameAnalyticsEvents();
                EventsLogger.AddEvent(GA);

                m_Applications.Add(GA);
            }

            if (settings.debugMode)
            {
                EventsDebug debug = new EventsDebug();
                EventsLogger.AddEvent(debug);
                m_Applications.Add(debug);
            }

            return true;
        }
    }
}
