using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DragNDrop
{
    public class DragAndDropListView : ListView
    {
        #region Private Members

        private ListViewItem m_previousItem;
        private bool m_allowReorder;
        private bool m_allowSelfDrop;
        private Color m_lineColor;

        #endregion

        #region Public Properties

        [Category("Behavior")]
        public bool AllowReorder
        {
            get { return m_allowReorder; }
            set { m_allowReorder = value; }
        }

        public bool AllowSelfDrop
        {
            get { return m_allowSelfDrop; }
            set { m_allowSelfDrop = value; }
        }

        [Category("Appearance")]
        public Color LineColor
        {
            get { return m_lineColor; }
            set { m_lineColor = value; }
        }

        #endregion

        #region Protected and Public Methods

        public DragAndDropListView() : base()
        {
            m_allowReorder = true;
            m_lineColor = Color.Red;
        }

        //定义委托
        public delegate void DragDropHandle(object sender, DragEventArgs e);
        //定义事件
        public event DragDropHandle ListViewDragDrop;
        private void EventListViewDragDrop(object sender, DragEventArgs e)
        {
            if (ListViewDragDrop != null)
                ListViewDragDrop(sender, e);
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            if (!m_allowReorder)
            {
                base.OnDragDrop(drgevent);
                return;
            }
            /*
            // get the currently hovered row that the items will be dragged to
            Point clientPoint = base.PointToClient(new Point(drgevent.X, drgevent.Y));
            ListViewItem hoverItem = base.GetItemAt(clientPoint.X, clientPoint.Y);

            if (!drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()) || 
                ((DragItemData)drgevent.Data.GetData(typeof(DragItemData).ToString())).ListView == null ||
                ((DragItemData)drgevent.Data.GetData(typeof(DragItemData).ToString())).DragItemIndexs.Count == 0)
                return;

            // retrieve the drag item data
            DragItemData data = (DragItemData)drgevent.Data.GetData(typeof(DragItemData).ToString());

            if (hoverItem == null || !m_allowSelfDrop)
            {
                // the user does not wish to re-order the items, just append to the end
                for (int i = 0; i < data.DragItemIndexs.Count; i++)
                {
                    ListViewItem newItem = data.ListView.Items[(int)(data.DragItemIndexs[i])];
                    base.Items.Add(newItem);
                }
            }
            else
            {
                // the user wishes to re-order the items

                // get the index of the hover item
                int hoverIndex = hoverItem.Index;

                // determine if the items to be dropped are from
                // this list view. If they are, perform a hack
                // to increment the hover index so that the items
                // get moved properly.
                if (this == data.ListView)
                {
                    if (hoverIndex > base.SelectedItems[0].Index)
                        hoverIndex++;
                }

                // insert the new items into the list view
                // by inserting the items reversely from the array list
                for (int i = data.DragItemIndexs.Count - 1; i >= 0; i--)
                {
                    ListViewItem newItem = data.ListView.Items[(int)(data.DragItemIndexs[i])];
                    base.Items.Insert(hoverIndex, newItem);
                }
            }

            // remove all the selected items from the previous list view
            // if the list view was found
            if (data.ListView != null)
            {
                List<int> listitemindex = new List<int>();
                foreach (int itemindexToRemove in data.ListView.SelectedIndices)
                {
                    listitemindex.Add(itemindexToRemove);
                }
                listitemindex.Sort();
                listitemindex.Reverse();
                foreach (int itemindexToRemove in listitemindex)
                {
                    data.ListView.Items.RemoveAt(itemindexToRemove);
                }
               
            }
            */

            // set the back color of the previous item, then nullify it
            if (m_previousItem != null)
            {
                m_previousItem = null;
            }

            
            EventListViewDragDrop(this, drgevent);
            this.Invalidate();

            // call the base on drag drop to raise the event
            base.OnDragDrop(drgevent);
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            if (!m_allowReorder)
            {
                base.OnDragOver(drgevent);
                return;
            }

            if (!drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()))
            {
                // the item(s) being dragged do not have any data associated
                drgevent.Effect = DragDropEffects.None;
                return;
            }
            if (!m_allowSelfDrop)
            {
                DragItemData data = (DragItemData)drgevent.Data.GetData(typeof(DragItemData).ToString());
                if (this == data.ListView)
                {
                    // the item(s) being dragged do not have any data associated
                    drgevent.Effect = DragDropEffects.None;
                    return;
                }
            }
            /*
            if (base.Items.Count > 0)
            {
                // get the currently hovered row that the items will be dragged to
                Point clientPoint = base.PointToClient(new Point(drgevent.X, drgevent.Y));
                ListViewItem hoverItem = base.GetItemAt(clientPoint.X, clientPoint.Y);

                Graphics g = this.CreateGraphics();

                if (hoverItem == null)
                {
                    //MessageBox.Show(base.GetChildAtPoint(new Point(clientPoint.X, clientPoint.Y)).GetType().ToString());

                    // no item was found, so no drop should take place
                    drgevent.Effect = DragDropEffects.Move;

                    if (m_previousItem != null)
                    {
                        m_previousItem = null;
                        Invalidate();
                    }

                    hoverItem = base.Items[base.Items.Count - 1];

                    if (this.View == View.Details || this.View == View.List)
                    {
                        g.DrawLine(new Pen(m_lineColor, 2), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(hoverItem.Bounds.X + this.Bounds.Width, hoverItem.Bounds.Y + hoverItem.Bounds.Height));
                        g.FillPolygon(new SolidBrush(m_lineColor), new Point[] { new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + hoverItem.Bounds.Height - 5), new Point(hoverItem.Bounds.X + 5, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + hoverItem.Bounds.Height + 5) });
                        g.FillPolygon(new SolidBrush(m_lineColor), new Point[] { new Point(this.Bounds.Width - 4, hoverItem.Bounds.Y + hoverItem.Bounds.Height - 5), new Point(this.Bounds.Width - 9, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(this.Bounds.Width - 4, hoverItem.Bounds.Y + hoverItem.Bounds.Height + 5) });
                    }
                    else
                    {
                        g.DrawLine(new Pen(m_lineColor, 2), new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width, hoverItem.Bounds.Y + hoverItem.Bounds.Height));
                        g.FillPolygon(new SolidBrush(m_lineColor), new Point[] { new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width - 5, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width + 5, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width, hoverItem.Bounds.Y + 5) });
                        g.FillPolygon(new SolidBrush(m_lineColor), new Point[] { new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width - 5, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width + 5, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(hoverItem.Bounds.X + hoverItem.Bounds.Width, hoverItem.Bounds.Y + hoverItem.Bounds.Height - 5) });
                    }

                    // call the base OnDragOver event
                    base.OnDragOver(drgevent);

                    return;
                }

                // determine if the user is currently hovering over a new
                // item. If so, set the previous item's back color back
                // to the default color.
                if ((m_previousItem != null && m_previousItem != hoverItem) || m_previousItem == null)
                {
                    this.Invalidate();
                }

                // set the background color of the item being hovered
                // and assign the previous item to the item being hovered
                //hoverItem.BackColor = Color.Beige;
                m_previousItem = hoverItem;

                if (this.View == View.Details || this.View == View.List)
                {
                    g.DrawLine(new Pen(m_lineColor, 2), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X + this.Bounds.Width, hoverItem.Bounds.Y));
                    g.FillPolygon(new SolidBrush(m_lineColor), new Point[] { new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y - 5), new Point(hoverItem.Bounds.X + 5, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + 5) });
                    g.FillPolygon(new SolidBrush(m_lineColor), new Point[] { new Point(this.Bounds.Width - 4, hoverItem.Bounds.Y - 5), new Point(this.Bounds.Width - 9, hoverItem.Bounds.Y), new Point(this.Bounds.Width - 4, hoverItem.Bounds.Y + 5) });
                }
                else
                {
                    g.DrawLine(new Pen(m_lineColor, 2), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + hoverItem.Bounds.Height));
                    g.FillPolygon(new SolidBrush(m_lineColor), new Point[] { new Point(hoverItem.Bounds.X - 5, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X + 5, hoverItem.Bounds.Y), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + 5) });
                    g.FillPolygon(new SolidBrush(m_lineColor), new Point[] { new Point(hoverItem.Bounds.X - 5, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(hoverItem.Bounds.X + 5, hoverItem.Bounds.Y + hoverItem.Bounds.Height), new Point(hoverItem.Bounds.X, hoverItem.Bounds.Y + hoverItem.Bounds.Height - 5) });
                }

                // go through each of the selected items, and if any of the
                // selected items have the same index as the item being
                // hovered, disable dropping.
                foreach (int itemToMove in base.SelectedIndices)
                {
                    if (itemToMove == hoverItem.Index)
                    {
                        drgevent.Effect = DragDropEffects.None;
                        hoverItem.EnsureVisible();
                        return;
                    }
                }

                // ensure that the hover item is visible
                hoverItem.EnsureVisible();
            }
            */
            // everything is fine, allow the user to move the items
            drgevent.Effect = DragDropEffects.Move;

            // call the base OnDragOver event
            base.OnDragOver(drgevent);
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            if (!m_allowReorder)
            {
                base.OnDragEnter(drgevent);
                return;
            }

            if (!drgevent.Data.GetDataPresent(typeof(DragItemData).ToString()))
            {
                // the item(s) being dragged do not have any data associated
                drgevent.Effect = DragDropEffects.None;
                return;
            }

            // everything is fine, allow the user to move the items
            drgevent.Effect = DragDropEffects.Move;

            // call the base OnDragEnter event
            base.OnDragEnter(drgevent);
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            if (!m_allowReorder)
            {
                base.OnItemDrag(e);
                return;
            }

            // call the DoDragDrop method
            base.DoDragDrop(GetDataForDragDrop(), DragDropEffects.Move);

            // call the base OnItemDrag event
            base.OnItemDrag(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            // reset the selected items background and remove the previous item
            ResetOutOfRange();

            Invalidate();

            // call the OnLostFocus event
            base.OnLostFocus(e);
        }

        protected override void OnDragLeave(EventArgs e)
        {
            // reset the selected items background and remove the previous item
            ResetOutOfRange();

            Invalidate();

            // call the base OnDragLeave event
            base.OnDragLeave(e);
        }

        #endregion

        #region Private Methods

        private DragItemData GetDataForDragDrop()
        {
            // create a drag item data object that will be used to pass along with the drag and drop
            DragItemData data = new DragItemData(this);

            // go through each of the selected items and 
            // add them to the drag items collection
            // by creating a clone of the list item
            /*
            foreach (ListViewItem item in this.SelectedItems)
            {
                data.DragItems.Add(item.Clone());
            }
            */
            foreach (var itemindex in this.SelectedIndices)
            {
                data.DragItemIndexs.Add(itemindex);
            }
            return data;
        }

        private void ResetOutOfRange()
        {
            // determine if the previous item exists,
            // if it does, reset the background and release 
            // the previous item
            if (m_previousItem != null)
            {
                m_previousItem = null;
            }

        }

        #endregion

        #region DragItemData Class

        public class DragItemData
        {
            #region Private Members

            private DragAndDropListView m_listView;
            private ArrayList m_dragItemindexs;

            #endregion

            #region Public Properties

            public DragAndDropListView ListView
            {
                get { return m_listView; }
            }

            public ArrayList DragItemIndexs
            {
                get { return m_dragItemindexs; }
            }

            #endregion

            #region Public Methods and Implementation

            public DragItemData(DragAndDropListView listView)
            {
                m_listView = listView;
                m_dragItemindexs = new ArrayList();
            }

            #endregion
        }

        #endregion
    }
}
