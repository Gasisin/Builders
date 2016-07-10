using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class CellController : MonoBehaviour{
        public Button CellImage;
        public GameObject Selected;
        public GameObject Unavailable;
        public Text Text;


        public void Selectable(){
            Selected.SetActive(true);
            Unavailable.SetActive(false);
        }

        public void Empty() {
            Selected.SetActive(false);
            Unavailable.SetActive(false);
        }

        public void UnavailableNow() {
            Selected.SetActive(false);
            Unavailable.SetActive(true);
        }

        public void BuilderHere(GameObject builder) {
            builder.transform.SetParent(transform);
        }
    }
}
