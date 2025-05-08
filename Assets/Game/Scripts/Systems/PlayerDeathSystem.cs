using Wargon.ezs;
using Wargon.UI;

namespace Wargon.TestGame
{
    public partial class PlayerDeathSystem : UpdateSystem
    {
        private GameRuntimeData runtimeData;
        private IUIService uiService;

        public override void Update()
        {
            entities.Each((Car car, Health health) =>
            {
                if (health.Value <= 0)
                {
                    runtimeData.State = GameRuntimeData.GameState.Lose;
                    uiService.Show<LosePopup>();
                }
            });
        }
    }
}