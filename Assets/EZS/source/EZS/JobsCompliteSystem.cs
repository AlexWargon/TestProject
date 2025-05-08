namespace Wargon.ezs
{
    [SystemColor(DColor.cyan)]
    public partial class JobsCompliteSystem : UpdateSystem {
        public override void Update() {
            DependenciesAll.Complete();
        }
    }
}