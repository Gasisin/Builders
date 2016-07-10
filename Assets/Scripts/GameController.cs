using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Data;
using UnityEngine;

namespace Assets.Scripts {
    public class GameController : MonoBehaviour{
        public GameObject CellPrefab;
        public GameObject BuilderPrefab;
        private MetaController _metaController;
        private Dictionary<Cell, CellController> _cellsGo;

        public void CreateCellView(Cell cell){
            GameObject newCell = Instantiate(CellPrefab);
            CellController cellController = newCell.GetComponent<CellController>();
            cellController.Empty();
            cellController.CellImage.onClick.AddListener(() => ClickOnCell(cell));
            _cellsGo.Add(cell, cellController);
            newCell.transform.SetParent(transform);
            newCell.transform.localScale = new Vector3(1,1,1);
        }

        void ClickOnCell(Cell cell){
            _metaController.ClickOnCell(cell);
        }

        public void SetMetaController(MetaController metaController){
            _cellsGo = new Dictionary<Cell, CellController>(); 
            _metaController = metaController;
        }
    }
}
