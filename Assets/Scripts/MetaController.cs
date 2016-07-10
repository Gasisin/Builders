using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class MetaController : MonoBehaviour{
        public GameController GameController;
        public Button StartGameButton;
        private MapData _mapData;

        void Awake(){
            GameController.SetMetaController(this);
            StartGameButton.onClick.AddListener(() =>{
                StartGameButton.gameObject.SetActive(false);
                _mapData = new MapData();
                _mapData.SetMetaController(this);
                _mapData.CreateNewMap();
            });
        }

        public void CreateCellView(Cell cell){
            GameController.CreateCellView(cell);
        }

        public void ClickOnCell(Cell cell) {
            Debug.Log("Click on cell - " + cell.X + " " + cell.Y);
        }

        public void SelectBuilder(Builder builder){
            throw new NotImplementedException();
        }
    }
}
