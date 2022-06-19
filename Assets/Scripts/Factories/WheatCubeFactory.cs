using Base;
using WheatField.WheatCube;

namespace Factories
{
    public class WheatCubeFactory : Factory<WheatCubeModel, WheatCubeView, IWheatCubeView, WheatCubePresenter>
    {
        public WheatCubeFactory(UpdateHandler updateHandler, WheatCubeView viewPrefab) : base(updateHandler, viewPrefab)
        {
        }
    }
}