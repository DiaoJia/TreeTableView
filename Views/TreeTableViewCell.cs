using System;
using CoreGraphics;

using Foundation;
using UIKit;

namespace TreeTableView
{
    public partial class TreeTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("TreeTableViewCell");
        public static readonly UINib Nib;

        public void SetCell(string name, string description)
        {
            lblName.Text = name;
            lblDescription.Text = description;
        }
        public void SetIcon(string iconName)
        {
            imgNodeIcon.ContentMode = UIViewContentMode.Center;
            imgNodeIcon.Image = UIImage.FromFile(iconName);
        }

        public void RemoveIcon()
        {
            imgNodeIcon.Image = null;
        }

        public void SetViewBackgroundX(int level)
        {
            this.imgViewX.Constant = level * 8f + 8f;
        }

        static TreeTableViewCell()
        {
            Nib = UINib.FromName("TreeTableViewCell", NSBundle.MainBundle);
        }

        protected TreeTableViewCell(IntPtr handle) : base(handle)
        {

        }
    }
}
