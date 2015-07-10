using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
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

    // MH: cell is a custom button
    public class UITableViewCell : UIButtonImage
    {
        public int row { get; set; } // MH: this is set before UITableViewDataProvider.SetupTableViewCell is called

        public T GetElement<T>(string nickname) where T : Component
        {
            Transform childTransform = gameObject.transform.Find(nickname);

            if (childTransform == null)
                return null;

            return childTransform.GetComponent<T>();
        }
    }

    // MH: attach this script to panel that should be a table view
    // attach UITableViewCell component to cell, it should be a panel too
    // assign GameObject property of table view to cell that will be instantiated
    // at runtime assign data provider then call Reload()
    // optionally you can attach scrollbar name "Scrollbar" to table view panel, it will be attached to scroll rect if found
    public sealed class UITableView : MonoBehaviour, UIButtonImageDelegate
    {
        public ITableViewDelegate tableViewDelegate { get; set; }
        public ITableViewDataProvider tableViewDataProvider { get; set; }
        public GameObject cellReference;
        private Mask mask { get; set; }
        private ScrollRect scrollRect { get; set; }
        private List<UITableViewCell> cells { get; set; }
        private GameObject contentHolder { get; set; } // this is where cells are put, its a scroll rect content object
        private Scrollbar tableVerticalScrollBar { get; set; }

        public UITableView()
        {

        }

        public void Clear()
        {
            if (cells == null)
                return;

            if (cells.Count == 0)
                return;

            // Destroy content cells
            foreach (UITableViewCell c in cells)
            {
                UnityEngine.Object.DestroyImmediate(c.gameObject);
            }

            cells.Clear();
            cells = null;
        }

        public void Reload()
        {
            Clear();

            if (tableViewDataProvider == null)
                return;

            if (cellReference == null)
                return;

            // get parameters
            UITableViewParameters parameters = tableViewDataProvider.OnTableViewGetParameters(this);

            // Add mask
            if (mask == null)
                mask = gameObject.AddComponent<Mask>();

            float contentHeight = parameters.rows * cellReference.GetComponent<RectTransform>().rect.height;

            // Add scroll rect with content game object
            if (scrollRect == null)
            {
                scrollRect = gameObject.AddComponent<ScrollRect>();
                scrollRect.horizontal = false;

                contentHolder = new GameObject();
                contentHolder.AddComponent<RectTransform>();

                contentHolder.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentHeight);
                contentHolder.transform.SetParent(gameObject.transform, false);

                scrollRect.content = contentHolder.GetComponent<RectTransform>();

                tableVerticalScrollBar = Assist.FindComponent<Scrollbar>(gameObject, "Scrollbar");

                if (tableVerticalScrollBar)
                {
                    scrollRect.verticalScrollbar = tableVerticalScrollBar;
                }
            }

            if (cells == null)
                cells = new List<UITableViewCell>();

            // create cells
            for (int i = 0; i < parameters.rows; i++)
            {
                GameObject cell = Instantiate(cellReference) as GameObject;

                cell.SetActive(true);
                cell.transform.SetParent(contentHolder.transform, false);

                UITableViewCell cellComponent = cell.GetComponent<UITableViewCell>();

                cellComponent.row = i;
                cellComponent.buttonDelegate = this;

                tableViewDataProvider.OnTableViewSetupCell(this, cellComponent);

                cells.Add(cellComponent);
            }

            // Align cells, in screen cords
            {
                RectTransform tableViewTransform = gameObject.GetComponent<RectTransform>();

                Vector3 pos = tableViewTransform.position + new Vector3(0.0f, contentHeight * 0.5f, 0.0f);

                int i = 0;

                float tableWidth = tableViewTransform.rect.width;

                float scrollbarOffset = tableVerticalScrollBar != null ? tableVerticalScrollBar.GetComponent<RectTransform>().rect.width : 0.0f;

                foreach (UITableViewCell c in cells)
                {
                    RectTransform rc = c.gameObject.GetComponent<RectTransform>();

                    if (i == 0)
                        pos -= new Vector3(0.0f, rc.rect.height * 0.5f, 0.0f);
                    else
                        pos -= new Vector3(0.0f, rc.rect.height, 0.0f);

                    i++;

                    rc.position = pos - new Vector3(scrollbarOffset * 0.5f, 0.0f, 0.0f);
                    rc.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tableWidth - scrollbarOffset);
                }

            }

            // disable original cell
            cellReference.SetActive(false);

            // Vertical scroll bar should be first
            if (tableVerticalScrollBar)
            {
                tableVerticalScrollBar.transform.SetAsLastSibling();
            }
        }

        public void OnUIButtonImageClick(UIButtonImage button)
        {
            if (tableViewDelegate != null)
            {
                tableViewDelegate.OnTableViewRowSelected(this, (UITableViewCell)button);
            }
        }
    }
}
