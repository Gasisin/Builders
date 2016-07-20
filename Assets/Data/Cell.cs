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

        public int GetBuildingLevel(){
            return (int) _building;
        }

        public BuildingType GetBuildingType() {
            return _building;
        }

        public override bool Equals(System.Object obj){
            return ((Cell) obj).X == X && ((Cell) obj).Y == Y;
        }

        private int _y;

        public bool IsFree(){
            return _building == BuildingType.Empty;// && _builder == null;
        }

        public bool IsCellIsNaignborLevel(Cell cell){
            return (GetBuildingLevel() + 1) >= cell.GetBuildingLevel();
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
            return result;
        }

//        public void AddBuilder(Builder builder){
//            _builder = builder;
//        }
        public bool BuildNextBuilding(){
            if ((int)_building>3) return false;
            int typeInt = (int) _building + 1;
            _building = (BuildingType)typeInt;
            return true;
        }

        public bool IsClosed(){
            return _building == BuildingType.Closed;
        }
    }
}
