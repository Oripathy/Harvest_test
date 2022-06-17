using Base;
using UnityEngine;

namespace WheatField.Wheat
{
    public class WheatModel : BaseModel
    {
        public float GrowUpTime { get; private set; }
        public Vector3 InitialScale { get; private set; }
        public Vector3 GrownUpScale { get; private set; }
        public bool IsHarvested { get; set; }

        public WheatModel Init()
        {
            // InitialScale = new Vector3(0.1f, 0.2f, 0.1f);
            // GrownUpScale = new Vector3(0.1f, 1f, 0.1f);
            InitialScale = new Vector3(1f, 0.5f, 1f);
            GrownUpScale = new Vector3(1f, 5f, 1f);
            GrowUpTime = 5f;
            return this;
        }
    }
}