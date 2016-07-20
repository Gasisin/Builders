using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;
using Random = System.Random;

namespace Assets.Data {
    public class MapData{
        Random random = new Random();
        public List<Cell> MapCells;
        public Dictionary<string, Builder> Builders;
        private List<string> _players;// = new List<string>(); 
        private string _currentPlayer;
        private Dictionary<string, Cell> _buildersPositions;

        private MetaController _metaController;

        public void CreateNewMap(){
            MapCells = new List<Cell>();
            for (int i = 1; i < 6; i++){
                for (int b = 1; b < 6; b++){
                    MapCells.Add(new Cell(i, b));
                    _metaController.CreateCellView(MapCells.Last());
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
            StartNewRound();
        }

        private Cell GetCellByPair(Pair cell){
            return MapCells.FirstOrDefault(x => x.X == cell.x && x.Y == cell.y);
        }
        public void SetMetaController(MetaController controller){
            _metaController = controller;
        }

        public void ClickOnCell(Cell cell){
            if (IsBuilderOnCell(cell)){
                Builder builder = Builders[_buildersPositions.First(x => x.Value.X == cell.X && x.Value.Y == cell.Y).Key];
                if (builder.PlayerId.Equals(_currentPlayer)){
                    SelectBuilder(builder);
                    Debug.Log("Select Builder");
                }
            }
            var selectedBuilder = GetSelectedBuilder();
            if (selectedBuilder == null) return;
            if (!selectedBuilder.IsMoveThisRound && IsCellAreNeighbor(_buildersPositions[selectedBuilder.UniqId], cell)){
                MoveBuilderTo(selectedBuilder, cell);
                Debug.Log("MoveBuilder");
            }
            if (selectedBuilder.IsMoveThisRound && IsCellAreNeighbor(_buildersPositions[selectedBuilder.UniqId], cell)) {
                Debug.Log("Build");
                if (Build(selectedBuilder, cell))
                {
                    StartNewRound();
                }
            }
        }

        private void SelectBuilder(Builder builder){
            if (CanSelect(builder)){
                foreach (var build in Builders){
                    if (build.Value.IsSelected) {
                        _metaController.DeSelectBuilder(build.Value);
                    }
                    build.Value.IsSelected = false;
                    
                }
                builder.IsSelected = true;
                DeselectCells();
                ShowAvailableCellToMove();
                _metaController.SelectBuilder(builder);
            }
        }

        private void DeselectCells(){
            _metaController.DeselectCells();
        }

        private bool CanSelect(Builder builder){
            return builder.PlayerId.Equals(_currentPlayer) && !IsMoveAlready();
        }

        private bool IsMoveAlready(){
            foreach (var build in Builders) {
                if (build.Value.IsMoveThisRound){
                    return true;
                }
            }
            return false;
        }

        private Builder GetSelectedBuilder(){
            foreach (var builder in Builders){
                if (builder.Value.IsSelected){
                    return builder.Value;
                }
            }
            return null;
        }

        private void ShowAvailableCellToBuild(){
            Builder sBuilder = GetSelectedBuilder();
            foreach (var npair in _buildersPositions[sBuilder.UniqId].GetNeighborsCells()) {
                Cell nCell = GetCellByPair(npair);
                if (nCell == null)
                    continue;
                if (CanBuildOnCell(sBuilder, nCell)) {
                    _metaController.SelectCell(nCell);
                } else {
                    _metaController.UnAvailable(nCell);
                }
            }
        }
        private void ShowAvailableCellToMove() {
            Builder sBuilder = GetSelectedBuilder();
            foreach (var npair in _buildersPositions[sBuilder.UniqId].GetNeighborsCells()) {
                Cell nCell = GetCellByPair(npair);
                if (nCell == null)
                    continue;
                if (CanMoveTo(sBuilder, nCell)) {
                    _metaController.SelectCell(nCell);
                } else {
                    _metaController.UnAvailable(nCell);
                }
            }
        }

        private bool IsBuilderOnCell(Cell cell){
            return _buildersPositions.Any(x => x.Value.X == cell.X && x.Value.Y == cell.Y);
        }

        public void StartNewRound(){
            foreach (var builder in Builders){
                builder.Value.StartNewRound();
                _metaController.DeSelectBuilder(builder.Value);
                _metaController.DeselectCells();
            }
            SelectNextPlayer();
        }

        public void MoveBuilderTo(Builder builder, Cell cell){
            if (CanMoveTo(builder, cell)) {
                builder.IsMoveThisRound = true;
                _buildersPositions[builder.UniqId] = cell;
                _metaController.MoveBuilderToCell(builder, cell);
                _metaController.DeselectCells();
                if (cell.GetBuildingType() == BuildingType.ThirdLevel){
                    _metaController.Winner(_currentPlayer);
                    return;
                }
                ShowAvailableCellToBuild();
            }
        }

        private bool CanMoveTo(Builder builder, Cell cell){
            return IsCellAreNeighbor(_buildersPositions[builder.UniqId], cell) &&
                   IsValidCell(cell) &&
                   _buildersPositions[builder.UniqId].IsCellIsNaignborLevel(cell) &&
                   !IsBuilderOnCell(cell) && !cell.IsClosed();
        }
        public bool IsCellAreNeighbor(Cell cell, Cell naiCell) {
            return cell.GetNeighborsCells().Any(x => x.x == naiCell.X && x.y == naiCell.Y);
        }

        private bool CanBuildOnCell(Builder builder, Cell cell){
            return IsCellAreNeighbor(_buildersPositions[builder.UniqId], cell) && IsValidCell(cell) &&
                   !IsBuilderOnCell(cell) && !cell.IsClosed();
        }

        public bool Build(Builder builder, Cell cell){
            if (CanBuildOnCell(builder, cell)) {
                builder.IsBuildThisRound = true;
                cell.BuildNextBuilding();
                _metaController.BuildOnCell(cell);
                return true;
            }
            return false;
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
            _metaController.NowPlay(_currentPlayer);
        }

        private void AddBuilder(Builder builder){
            Cell freeCell = FindFreeCell();
            _buildersPositions.Add(builder.UniqId, freeCell);
            _metaController.AddBuilderToCell(builder, freeCell);
        }

        private Cell FindFreeCell(){
            List<Cell> freeCells = MapCells.FindAll(x => x.IsFree() && !_buildersPositions.ContainsValue(x)).ToList();
            return freeCells[random.Next(freeCells.Count)];
        }
    }
}
