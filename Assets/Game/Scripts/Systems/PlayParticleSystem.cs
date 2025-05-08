using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class PlayParticleSystem : UpdateSystem
    {
        public override void Update()
        {
            entities.Each((Particle particle, PooledEvent evnt) => { particle.Value.Play(); });
        }
    }

    public partial class CheckWinSystem : UpdateSystem
    {
        private float distanceTraveled;
        private GameData gameData;

        public override void Update()
        {
        }
    }
}