using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Assets.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class MetaController : MonoBehaviour{
        public GameController GameController;
        public Button StartGameButton;
        public Text WhoPlayText;
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

        public void ClickOnCell(Cell cell){
            _mapData.ClickOnCell(cell);
            Debug.Log("Click on cell - " + cell.X + " " + cell.Y);
        }

        public void SelectBuilder(Builder builder){
            GameController.SelectBuilder(builder);
        }

        public void DeSelectBuilder(Builder builder) {
            GameController.DeSelectBuilder(builder);
        }

        public void MoveBuilderToCell(Builder builder, Cell cell) {
            GameController.MoveBuilderToCell(builder, cell);
        }
        public void AddBuilderToCell(Builder builder, Cell cell){
            GameController.AddBuilderToCell(builder, cell);
        }

        public void SelectCell(Cell nCell){
            GameController.SelectSell(nCell);
        }

        public void UnAvailable(Cell nCell){
            GameController.UnAvailable(nCell);
        }

        public void DeselectCells(){
            GameController.DeselectCells();
        }

        public void BuildOnCell(Cell cell){
            GameController.BuildOnCell(cell);
        }

        public void NowPlay(string currentPlayer){
            WhoPlayText.text = currentPlayer;
        }

        public void Winner(string currentPlayer){
            WhoPlayText.text = currentPlayer+" Win this game";
        }
    }
}
