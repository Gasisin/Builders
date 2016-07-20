using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    class BuilderController : MonoBehaviour{
        public Image BuilderImage;
        public Image BuilderSelected;
        private Builder builder;

        public void Init(GameObject go, Builder builder){
            this.builder = builder;
            if (builder.PlayerId.Equals("player1")) {
                BuilderImage.color = Color.white;
            } else {
                BuilderImage.color = Color.black;
            }
            transform.SetParent(go.transform);
            transform.localPosition = new Vector3(0,0);
            transform.localScale = new Vector3(1,1);
        }

        public void Select(){
            Debug.Log("Select "+builder.UniqId);
            BuilderSelected.gameObject.SetActive(true);
        }

        public void MoveBuilderToGo(GameObject go){
            transform.SetParent(go.transform);
            transform.localPosition = new Vector3(0, 0);
        }

        public void DeSelect(){
            Debug.Log("Deselect " + builder.UniqId);
            BuilderSelected.gameObject.SetActive(false);
        }
    }
}
