using UnityEngine;
using Wargon.ezs;

namespace Wargon.TestGame
{
    public partial class PlayerInputWriteSystem : UpdateSystem
    {
        private Camera camera;

        public override void Update()
        {
            entities.Each((PlayerInput input) =>
            {
                input.Axis.x = Input.GetAxis("Horizontal");
                input.Axis.y = Input.GetAxis("Vertical");
                input.MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                input.Fire = Input.GetButton("Fire1");
            });
        }
    }
}