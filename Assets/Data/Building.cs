using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Data {
    public class Building{
        private BuildingType _type;

        public BuildingType Type{
            get { return _type; }
        }

        public Building(BuildingType type){
            _type = type;
        }

        public Building(int type){
            _type = (BuildingType) type;
        }

    }

    public enum BuildingType{
        Empty = 0,
        FirstLevel = 1,
        SecondLevel = 2,
        ThirdLevel = 3,
        Closed = 4,
    }
}
