using UnityEngine;

namespace apps
{
    public enum ShowADSType { Simulator, Debug }

    [CreateAssetMenu(fileName = "AppsSettings", menuName = "AppsSettings", order = 1)]
    public class AppsSettings : ScriptableObject
    {
        public readonly static string globalDirectoryPath = "Assets/_SDK/Resources/";

        #region facebook
        public bool integrateFacebook = true;

        public string appLabels = "";

        public string appFacebookID = "Entry Facebook ID...";

        public string clientTokens = "";
        #endregion

        #region IronSource
        public bool integrateIronSource = false;
        public ShowADSType showADSType = ShowADSType.Simulator;

        public ADSInfo adsInfo;

        public string androidKey = "Entry Android Key";
        public string iosKey = "Entry IOS Key";
        #endregion

        public bool integrateGameAnalytics = true;
        public bool debugMode = true;
    }
}
