using UnityEngine;
using UnityEngine.UI;
using Wargon.UI;

namespace Wargon.TestGame
{
    public class StartPopup : Popup
    {
        private GameRuntimeData runtimeData;
        [SerializeField] private Button startButton;
        
        public override void OnCreate()
        {
            startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            UIService.Hide<StartPopup>(() =>
            {
                runtimeData.State = GameRuntimeData.GameState.GameRunning;
            });
        }
    }
}