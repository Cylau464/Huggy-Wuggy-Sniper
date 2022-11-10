using core;
using input;
using engine;
using UnityEngine;
using main.level;

public class GameManager : CoreManager
{
    #region statues
    public static bool isStarted { get; private set; }
    public static bool isCompleted { get; private set; }
    public static bool isFailed { get; private set; }

    public static bool isFinished { get { return isFailed || isCompleted; } }
    public static bool isPlaying { get { return !isFinished && isStarted; } }
    #endregion

    [Header("Levels data")]
    [SerializeField] private LevelsData _levelsData;

    public LevelsData levelsData => _levelsData;

    private IGameStatue _startStatue = new LevelStatueStarted();
    private IGameStatue _failedStatue = new LevelStatueFailed();
    private IGameStatue _completedStatue = new LevelStatueCompleted();

    protected override void OnInitialize()
    {

        isStarted = false;
        isCompleted = false;
        isFailed = false;

#if Support_SDK
        apps.ADSManager.DisplayBanner();
#endif
    }

    #region desitions
    public void MakeStarted()
    {
        isStarted = true;

#if Support_SDK
        apps.ProgressEvents.OnLevelStarted(_levelsData.playerLevel);
#endif

        SwitchToStatue(_startStatue);
    }

    public void MakeFailed()
    {
        if (isFinished)
            return;

        isFailed = true;

        ControllerInputs.s_EnableInputs = false;

        _levelsData.OnLost();

#if Support_SDK
        apps.ProgressEvents.OnLevelFieled(_levelsData.playerLevel);
#endif

        SwitchToStatue(_failedStatue);
    }

    public void MakeCompleted()
    {
        if (isFinished)
            return;

        isCompleted = true;

        ControllerInputs.s_EnableInputs = false;

        int playerLevel = _levelsData.playerLevel;
        _levelsData.OnWin();

#if Support_SDK
        apps.ProgressEvents.OnLevelCompleted(playerLevel);
#endif

        SwitchToStatue(_completedStatue);
    }
    #endregion
}