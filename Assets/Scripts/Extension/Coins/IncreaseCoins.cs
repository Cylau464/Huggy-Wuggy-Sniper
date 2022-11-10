using System.Collections;
using System.Collections.Generic;
using engine;
using engine.coin;
using UnityEngine;

namespace Extension
{
    public class IncreaseCoins : MonoBehaviour, ILevelCompleted, ILevelFailed
    {
        [SerializeField] private CoinsData coinsData;
        public int coinsTests;

        void OnEnable()
        {
            LevelStatueCompleted.Subscribe(this);
            LevelStatueFailed.Subscribe(this);
        }

        //[NaughtyAttributes.Button("On Level Failed")]
        public void LevelFailed()
        {
            coinsData.RemoveCoins(500);
            coinsTests -= 500;
        }

        //[NaughtyAttributes.Button("On Level Completed")]
        public void LevelCompleted()
        {
            coinsData.AddCoins(1000);
            coinsTests += 1000;
        }
    }
}