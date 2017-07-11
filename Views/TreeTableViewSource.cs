﻿using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using TreeTableView.Models;


namespace TreeTableView.Views
{
    public interface TreeTableViewCellDelegate{
        void CellClick();
    }

    public class TreeTableViewSource:UITableViewSource
    {
        private static string CellIdentifier = "nodeCellIdentifier";
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
            var nib = UINib.FromName("TreeNodeTableViewCell", null);
            tableView.RegisterNibForCellReuse(nib,CellIdentifier);
            var cell = tableView.DequeueReusableCell(CellIdentifier) as TreeNodeTableViewCell;

            var node = this._nodes[indexPath.Row];

            cell.BackgroundView.Frame.X = -20 * (float)node.GetLevel();


            return null;
        }

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow(indexPath, false);
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 55;
		}
    }
}
