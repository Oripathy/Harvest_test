using System.Collections.Generic;
using Base;
using WheatField.Wheat;

namespace WheatField
{
    public class WheatFieldModel : BaseModel
    {
        private readonly float[] _fieldSize = { 10f, 10f };
        private List<List<WheatModel>> _wheat;

        public void SetWheat(List<List<WheatModel>> wheat)
        {
            _wheat = wheat;
        }
    }
}