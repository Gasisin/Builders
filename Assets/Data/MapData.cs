using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Data {
    public class MapData{
        public List<Cell> MapCells;
        public Dictionary<string, Builder> Builders;
        private List<string> _players;// = new List<string>(); 
        private string _currentPlayer;
        private Dictionary<string, Cell> _buildersPositions; 

        public void CreateNewMap(){
            MapCells = new List<Cell>();
            for (int i = 1; i < 6; i++){
                for (int b = 1; b < 6; b++){
                    MapCells.Add(new Cell(i, b));
                }
            }
            _players = new List<string>();
            _players.Add("player1");
            _players.Add("player2");
            Builders = new Dictionary<string, Builder>();
            _buildersPositions = new Dictionary<string, Cell>();
            Builders.Add("1", new Builder(_players[0], "1"));
            Builders.Add("2", new Builder(_players[0], "2"));
            Builders.Add("3", new Builder(_players[1], "3"));
            Builders.Add("4", new Builder(_players[1], "4"));
            foreach (var builder in Builders){
                AddBuilder(builder.Value);
            }
        }

        public void StartNewRound(){
            SelectNextPlayer();
        }

        public void MoveBuilderTo(Builder builder, Cell cell){
            if (IsCellAreNeighbor(_buildersPositions[builder.UniqId], cell) && IsValidCell(cell)) {
                builder.IsMoveThisRound = true;
                _buildersPositions[builder.UniqId] = cell;
            }
        }

        public bool IsCellAreNeighbor(Cell cell, Cell naiCell) {
            return cell.GetNeighborsCells().Any(x => x.x == naiCell.X && x.y == naiCell.Y);
        }

        public void Build(Builder builder, Cell cell){
            if (IsCellAreNeighbor(_buildersPositions[builder.UniqId], cell) && IsValidCell(cell)) {
                builder.IsBuildThisRound = true;
                cell.BuildNextBuilding();
            }
        }

        public bool IsValidCell(Cell cell){
            return MapCells.Any(x => x.X == cell.X && x.Y == cell.Y);
        }

        public void ResetBuildersCounters(){
            foreach (var builder in Builders){
                builder.Value.IsMoveThisRound = false;
                builder.Value.IsBuildThisRound = false;
            }
        }

        private void SelectNextPlayer(){
            _currentPlayer = _players.First(x => !x.Equals(_currentPlayer));
        }

        private void AddBuilder(Builder builder){
            Cell freeCell = FindFreeCell();
            _buildersPositions.Add(builder.UniqId, freeCell);
        }

        private Cell FindFreeCell(){
            Random random = new Random();
            List<Cell> freeCells = MapCells.FindAll(x => x.IsFree() && !_buildersPositions.ContainsValue(x)).ToList();
            return freeCells[random.Next(freeCells.Count)];
        }
    }
}
