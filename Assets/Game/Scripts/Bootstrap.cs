using UnityEngine;
using Wargon.DI;
using Wargon.ezs.Unity;
using Wargon.UI;

namespace Wargon.TestGame
{

    [DefaultExecutionOrder(ExecutionOrder.BOOTSTRAP)]
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameWorld gameWorld;
        [SerializeField] private GameData gameData;
        [SerializeField] private int targetFrameRate = 60;
        private void Awake() {
            Initialize();
        }

        private void Initialize()
        {
            Application.targetFrameRate = targetFrameRate;
            var di = Injector.GetOrCreate();
            Injector.AddAsSingle(di);
            Injector.AddAsSingle(new GameRuntimeData());
            Injector.AddAsSingle(gameData);
            var ui = IUIService.CreateInstance<UIService>();
            Injector.AddAsSingle(ui);
            Injector.AddAsSingle(new TimeService());
            Injector.Resolve(gameWorld);
            gameWorld.InitializeWorld(out var world);
            Injector.AddAsSingle(new EntityViewMap(world));
            Injector.AddAsSingle(new ObjectPool());
            gameWorld.InitializeSystems();
            ui.Show<StartPopup>();
        }
        private void OnDestroy()
        {
            Injector.DisposeAll();
        }
    }
}

