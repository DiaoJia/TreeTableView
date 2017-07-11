using System;

using Foundation;
using UIKit;

namespace TreeTableView.Views
{
    public partial class TreeNodeTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("TreeNodeTableViewCell");
        public static readonly UINib Nib;

        static TreeNodeTableViewCell()
        {
            Nib = UINib.FromName("TreeNodeTableViewCell", NSBundle.MainBundle);
        }

        protected TreeNodeTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
