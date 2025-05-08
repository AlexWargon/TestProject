using Wargon.ezs;
using Wargon.UI;

namespace Wargon.TestGame
{
    public partial class WinSystem : UpdateSystem
    {
        private GameData gameData;
        private GameRuntimeData runtimeData;
        private IUIService uiService;

        public override void Update()
        {
            entities.Each((Car car) =>
            {
                if (gameData.WinDistance <= car.DistanceTraveled)
                {
                    runtimeData.State = GameRuntimeData.GameState.Win;
                    uiService.Show<WinPopup>();
                }
            });
        }
    }
}