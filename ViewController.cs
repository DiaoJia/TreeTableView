using System;

using UIKit;
using Foundation;
using System.Collections.Generic;
using TreeTableView.Models;

namespace TreeTableView
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
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
                var node = new TreeNode(id,pid,name,description);               
                treeNodes.Add(node);
            }

            this.lblTitle.Text = treeNodes.Count.ToString();
        }
    }
}
