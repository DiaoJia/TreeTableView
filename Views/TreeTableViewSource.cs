using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using TreeTableView.Models;


namespace TreeTableView.Views
{
    public class TreeTableViewSource : UITableViewSource
    {
        private List<TreeNode> _allNodes = new List<TreeNode>();
        private List<TreeNode> _nodes = new List<TreeNode>();

        public TreeTableViewSource(List<TreeNode> allNodes)
        {
            this._allNodes = allNodes;
            this._nodes = TreeNodeHelper.Instance.FilterVisibleNode(allNodes);
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this._nodes.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(TreeTableViewCell.Key) as TreeTableViewCell;
            var node = this._nodes[indexPath.Row];
            cell.SetCell(node.Name, node.Description);

            cell.SetViewBackgroundX(node.GetLevel());

			if (!node.LeafFlag)
            {
                cell.SetIcon(node.Icon);
            }
            else
            {
                cell.RemoveIcon();
            }
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var parentNode = _nodes[indexPath.Row];
            var startPosition = indexPath.Row + 1;
            var endPosition = startPosition;

            if (!parentNode.IsLeaf())
            {
                var countObj = new CountObj();
                countObj.Count = startPosition;
                ExpandOrCollapse(countObj, parentNode);
                endPosition = countObj.Count;

                this._nodes = TreeNodeHelper.Instance.FilterVisibleNode(_allNodes);

                var indexPathArray = new List<NSIndexPath>();

                NSIndexPath tmpIndexPath;

                for (int i = startPosition; i < endPosition; i++)
                {
                    tmpIndexPath = NSIndexPath.FromRowSection(i, 0);
                    indexPathArray.Add(tmpIndexPath);
                }

                if (parentNode.IsExpand)
                {
                    tableView.InsertRows(indexPathArray.ToArray(), UITableViewRowAnimation.None);
                }
                else
                {
                    tableView.DeleteRows(indexPathArray.ToArray(), UITableViewRowAnimation.None);
                }

                tableView.ReloadRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.None);
            }

        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 55;
        }

        private void ExpandOrCollapse(CountObj count, TreeNode node)
        {
            if (node.IsExpand)
            {
                ClosedChildNode(count, node);
            }
            else
            {
                count.Count = count.Count + node.Children.Count;
                node.SetExpand(true);
            }

        }

        private void ClosedChildNode(CountObj count, TreeNode node)
        {
            if (node.IsLeaf())
            {
                return;
            }

            if (node.IsExpand)
            {
                node.IsExpand = false;
                foreach (var item in node.Children)
                {
                    count.Count = count.Count + 1;
                    ClosedChildNode(count, item);
                }
            }
        }

        class CountObj
        {
            public int Count { get; set; }
        }

    }
}
