using Foundation;
using System;
using UIKit;
using TreeTableView.Models;
using System.Collections.Generic;

namespace TreeTableView
{
    public partial class CityTableViewController : UITableViewController
    {
        public CityTableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			var plistPath = NSBundle.MainBundle.PathForResource("dataInfo", "plist");
			var data = NSArray.FromFile(plistPath);

			var treeNodes = new List<TreeNode>();

			foreach (var item in NSArray.FromArray<NSDictionary>(data))
			{
				var id = item[(NSString)"id"].ToString();
				var pid = item[(NSString)"pid"].ToString();
				var name = item[(NSString)"name"].ToString();
				var description = item[(NSString)"description"].ToString();
				var node = new TreeNode(id, pid, name, description);
				treeNodes.Add(node);
			}

			var filterData = TreeNodeHelper.Instance.FilterVisibleNode(treeNodes);
			
            this.TableView.RegisterNibForCellReuse(TreeTableViewCell.Nib, TreeTableViewCell.Key);			
			this.TableView.Source = new Views.TreeTableViewSource(filterData);

            this.TableView.ReloadData();
        }
    }
}