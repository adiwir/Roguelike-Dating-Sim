using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace finished2
{
    public class OverlayTile : MonoBehaviour
    {
        public int G;
        public int H;
        public int F { get { return G + H; } }

        public bool isBlocked = false;

        public OverlayTile Previous;
        public Vector3Int gridLocation;


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HideTile();
            }
        }

        public void HideTile()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }

        public void ShowTile()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
        }

        public void ShowHurtTile()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }

    }
}
