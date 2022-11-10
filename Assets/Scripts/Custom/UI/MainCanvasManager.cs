using System;
using System.Diagnostics;
using core.ui;
using engine;
using UnityEngine;

namespace main.ui
{
    public class MainCanvasManager : GameCanvas, ILevelCompleted, ILevelFailed
    {
        [SerializeField] private GameManager _gameManager;

        [Header("Panels")]
        [SerializeField] private Panel _menuPanel;
        [SerializeField] private Panel _playPanel;
        [SerializeField] private Panel _losePanel;
        [SerializeField] private Panel _winPanel;
        [SerializeField] private Panel _aimPanel;

        [Header("Settings")]
        [SerializeField] private float _timeAndShowWinLosePanel = 2f;
        
        private static MainCanvasManager instance;
        public static MainCanvasManager Instance => instance;

        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        protected void OnEnable()
        {
            LevelStatueCompleted.Subscribe(this);
            LevelStatueFailed.Subscribe(this);

            SwitchPanel(_menuPanel);
        }

        public void StartGame()
        {
            _gameManager.MakeStarted();
            OnStart();
        }

        private void OnStart()
        {
            SwitchPanel(_playPanel);
        }

        public void LevelCompleted()
        {
            StartCoroutine(WaitAndShowWin());
        }

        public void LevelFailed()
        {
            StartCoroutine(WaitAndShowLose());
        }

        #region coroutine
        private System.Collections.IEnumerator WaitAndShowLose()
        {
            yield return new WaitForSeconds(_timeAndShowWinLosePanel);
            SwitchPanel(_losePanel);
        }

        private System.Collections.IEnumerator WaitAndShowWin()
        {
            yield return new WaitForSeconds(_timeAndShowWinLosePanel);
            SwitchPanel(_winPanel);
        }
        #endregion

        public void Next()
        {
            ReloadScene();
        }

        public void ReloadScene()
        {
            GameScenes.ReloadScene();
        }

        public void ShowAim()
        {
            SwitchPanel(_aimPanel);
        }

        public bool IsShowedAimPanel()
        {
            return _aimPanel.gameObject.activeInHierarchy;
        }

        public void ContinueGame()
        {
            SwitchPanel(_playPanel);
        }

#if UNITY_EDITOR
        protected void OnValidate()
        {
            if (_gameManager == null)
                _gameManager = editor.EditorManager.FindScenesComponent<GameManager>();
        }
#endif
    }
}
