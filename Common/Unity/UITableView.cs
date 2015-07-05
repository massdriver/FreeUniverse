using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace FreeUniverse.Common.Unity
{
    public interface ITableViewDelegate
    {
        void OnTableViewRowSelected(UITableView tableView, UITableViewCell cell);
    }

    public struct UITableViewParameters
    {
        public int rows { get; set; }
        public float cellHeight { get; set; } // autosize if <= 0.0f
        public float cellWidth { get; set; } // autosize if <= 0.0f
        public float initialScroll { get; set; }
    }

    public interface ITableViewDataProvider
    {
        UITableViewParameters OnTableViewGetParameters(UITableView tableView);
        void OnTableViewSetupCell(UITableView tableView, UITableViewCell cell);
    }

    public class UITableViewCell : MonoBehaviour
    {
        public int row { get; set; } // MH: this is set before UITableViewDataProvider.SetupTableViewCell is called
    }

    // MH: attach this script to panel that should be a table view
    // attach UITableViewCell component to cell, it should be a panel too
    // assign GameObject property of table view to cell that will be instantiated
    // at runtime assign data provider then call Reload()
    public class UITableView : MonoBehaviour
    {
        public ITableViewDelegate tableViewDelegate { get; set; }
        public ITableViewDataProvider tableViewDataProvider { get; set; }
        public GameObject cellReference;
        private Mask mask { get; set; }
        private ScrollRect scrollRect { get; set; }
        private List<GameObject> cells { get; set; }
        private GameObject contentHolder { get; set; } // this is where cells are put, its a scroll rect content object

        public UITableView()
        {

        }

        public void Clear()
        {
            if (cells == null)
                return;

            if (cells.Count == 0)
                return;
        }

        public void Reload()
        {
            Clear();

            if (tableViewDataProvider == null)
                return;

            if (cellReference == null)
                return;

            // Add mask
            if( mask == null )
                mask = gameObject.AddComponent<Mask>();

            // Add scroll rect with content game object
            if( scrollRect == null )
            {
                scrollRect = gameObject.AddComponent<ScrollRect>();
                scrollRect.horizontal = false;
                
                contentHolder = new GameObject();
                contentHolder.AddComponent<RectTransform>();
                contentHolder.transform.SetParent(gameObject.transform, false);

                scrollRect.content = contentHolder.GetComponent<RectTransform>();
            }

            if (cells == null)
                cells = new List<GameObject>();

            // get parameters
            UITableViewParameters parameters = tableViewDataProvider.OnTableViewGetParameters(this);

            // create cells
            for (int i = 0; i < parameters.rows; i++)
            {
                GameObject cell = Instantiate(cellReference) as GameObject;

                cell.SetActive(true);
                cell.transform.SetParent(contentHolder.transform, false);

                UITableViewCell cellComponent = cell.GetComponent<UITableViewCell>();

                tableViewDataProvider.OnTableViewSetupCell(this, cellComponent);

                cells.Add(cell);
            }

            // Align cells
            {
                RectTransform tableViewTransform = gameObject.GetComponent<RectTransform>();


            }

            // disable original cell
            cellReference.SetActive(false);
        }
    }
}
