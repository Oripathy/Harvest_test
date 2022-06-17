using Base;
using Factories;
using UnityEngine;

namespace WheatField.Wheat
{
    public class WheatModel : BaseModel
    {
        private WheatCubeFactory _wheatCubeFactory;
        
        public WheatCubeFactory WheatCubeFactory => _wheatCubeFactory;
        public float GrowUpTime { get; private set; }
        public Vector3 InitialScale { get; private set; }
        public Vector3 GrownUpScale { get; private set; }
        public bool IsHarvested { get; set; }

        public WheatModel Init(WheatCubeFactory wheatCubeFactory)
        {
            _wheatCubeFactory = wheatCubeFactory;
            InitialScale = new Vector3(1f, 0.1f, 1f);
            GrownUpScale = new Vector3(1f, 1f, 1f);
            GrowUpTime = 10f;
            return this;
        }
    }
}