using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Data {
    public class Cell{
//        private Builder _builder;
        private BuildingType _building;

        private int _x;

        public Cell(int x, int y){
            _x = x;
            _y = y;
//            _builder = null;
            _building = BuildingType.Empty;
        }
        public int X{
            get { return _x; }
        }

        public int Y{
            get { return _y; }
        }

        private int _y;

        public bool IsFree(){
            return _building == BuildingType.Empty;// && _builder == null;
        }

        public List<Pair> GetNeighborsCells(){
            List<Pair> result = new List<Pair>();
            result.Add(new Pair(_x-1, _y-1));
            result.Add(new Pair(_x-1, _y));
            result.Add(new Pair(_x-1, _y+1));
            result.Add(new Pair(_x, _y+1));
            result.Add(new Pair(_x+1, _y+1));
            result.Add(new Pair(_x+1, _y));
            result.Add(new Pair(_x+1, _y-1));
            result.Add(new Pair(_x, _y-1));
        }

//        public void AddBuilder(Builder builder){
//            _builder = builder;
//        }
        public void BuildNextBuilding(){
            int typeInt = (int) _building + 1;
            _building = (BuildingType)typeInt;
        }
    }
}
