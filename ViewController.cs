using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using TreeTableView.Models;

namespace TreeTableView
{
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle)
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

            var nodes = TreeNodeHelper.Instance.GetSortedNodes(treeNodes,0);

            var frame = new CoreGraphics.CGRect(0, 0, this.View.Frame.Width, this.View.Frame.Height - 20);
            var cityTableView = new UITableView(frame,UITableViewStyle.Plain);

			cityTableView.RegisterNibForCellReuse(TreeTableViewCell.Nib, TreeTableViewCell.Key);
			cityTableView.Source = new Views.TreeTableViewSource(nodes);
			cityTableView.ReloadData();

            cityTableView.TableFooterView = new UIView();

            this.View.Add(cityTableView);
		}
    }
}