using System;
using System.Collections.Generic;

namespace TreeTableView.Models
{
    public class TreeNode
    {
        public static int NODE_TYPE_G = 0;
        public static int NODE_TYPE_N = 1;

        public int? Type { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string Pid { get; set; }
        public string Name { get; set; }
        public int? Level { get; set; }
        public bool IsExpand { get; set; } = false;
        public string Icon { get; set; }
        public List<TreeNode> Children { get; set; } = new List<TreeNode>();
        public TreeNode Parent { get; set; }

        public TreeNode(string id, string pid, string name, string description)
        {
            this.Id = id;
            this.Pid = pid;
            this.Name = name;
            this.Description = description;
        }

        public bool IsRoot()
        {
            return this.Parent == null;
        }

        public bool IsLeaf()
        {
            return Children.Count == 0;
        }

        public bool IsParentExpand()
        {
            return this.Parent == null ? false : this.Parent.IsExpand;
        }

        public int GetLevel()
        {
            return this.Parent == null ? 0 : this.Parent.GetLevel() + 1;
        }

        public void SetExpand(bool isExpand)
        {
            this.IsExpand = isExpand;

            if (!isExpand)
            {
                foreach (var item in this.Children)
                {
                    item.SetExpand(isExpand);
                }
            }
        }
    }
}
