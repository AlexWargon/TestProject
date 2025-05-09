using UnityEngine;
using Wargon.ezs;
using Wargon.ezs.Unity;
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
            entities.Without<WinEvent>().Each((Entity e, Car car) =>
            {
                if (gameData.WinDistance <= car.DistanceTraveled)
                {
                    e.Add<WinEvent>();
                    entities.Without<Inactive>().Each((Entity enemyE, EnemyTag enemyTag) =>
                    {
                        enemyE.Add<DeathEvent>();
                    });
                    
                    world.CreateEntity().Add(new ActionOnDelay
                    {
                        Delay = 1f,
                        Action = StopGame
                    });
                }
            });
        }
        private void StopGame()
        {
            runtimeData.State = GameRuntimeData.GameState.Win;
            uiService.Show<WinPopup>();
        }
    }

    public struct WinEvent
    {
        
    }
}