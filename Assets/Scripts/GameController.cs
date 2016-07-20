using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class GameController : MonoBehaviour{
        public GameObject CellPrefab;
        public GameObject BuilderPrefab;
        private MetaController _metaController;
        private Dictionary<Cell, CellController> _cellsGo;
        private Dictionary<Builder, BuilderController> _builderGOs;

        public void CreateCellView(Cell cell){
            GameObject newCell = Instantiate(CellPrefab);
            CellController cellController = newCell.GetComponent<CellController>();
            cellController.Init();
            cellController.Empty();
            cellController.CellImage.onClick.AddListener(() => ClickOnCell(cell));
            _cellsGo.Add(cell, cellController);
            newCell.transform.SetParent(transform);
            newCell.transform.localScale = new Vector3(1,1,1);
        }

        public void AddBuilderToCell(Builder builder, Cell cell){
            GameObject newBuilderGO = Instantiate(BuilderPrefab);
            BuilderController bc = newBuilderGO.GetComponent<BuilderController>();
            _builderGOs.Add(builder, bc);
            bc.Init(_cellsGo.First(x => x.Key.X == cell.X && x.Key.Y == cell.Y).Value.gameObject, builder);
        }

        void ClickOnCell(Cell cell){
            _metaController.ClickOnCell(cell);
        }

        public void MoveBuilderToCell(Builder builder, Cell cell){
            _builderGOs[builder].MoveBuilderToGo(_cellsGo.First(x => x.Key.X == cell.X && x.Key.Y == cell.Y).Value.gameObject);
        }

        public void SetMetaController(MetaController metaController){
            _cellsGo = new Dictionary<Cell, CellController>();
            _builderGOs = new Dictionary<Builder, BuilderController>();
            _metaController = metaController;
        }

        public void SelectBuilder(Builder builder){
            _builderGOs[builder].Select();
        }

        public void DeSelectBuilder(Builder builder){
            _builderGOs[builder].DeSelect();
        }

        public void SelectSell(Cell nCell){
            _cellsGo[nCell].Selectable();
        }

        public void UnAvailable(Cell nCell){
            _cellsGo[nCell].UnavailableNow();
        }

        public void DeselectCells(){
            foreach (var cellController in _cellsGo.Values){
                cellController.Empty();
            }
        }

        public void BuildOnCell(Cell cell){
            _cellsGo[cell].BuildOnCell(cell);
        }
    }
}
