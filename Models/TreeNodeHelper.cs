using System;
using System.Collections.Generic;

namespace TreeTableView.Models
{
    public class TreeNodeHelper
    {
        public readonly static TreeNodeHelper Instance = new TreeNodeHelper();

        public List<TreeNode> GetSortedNodes(List<TreeNode> sourceNodes, int defaultLevel)
        {
            var result = new List<TreeNode>();
            var nodes = ConvertDataToNode(sourceNodes);
            var rootNodes = GetRootNodes(nodes);
            foreach (var item in rootNodes)
            {
                AddNode(result, item, defaultLevel, 1);
            }
            return result;
        }

        public List<TreeNode> FilterVisibleNode(List<TreeNode> nodes)
        {
            var result = new List<TreeNode>();
            foreach (var item in nodes)
            {
                if (item.IsRoot() || item.IsParentExpand())
                {
                    SetNodeIcon(item);
                    result.Add(item);
                }
            }
            return result;
        }

        public List<TreeNode> ConvertDataToNode(List<TreeNode> nodes)
        {
            TreeNode n, m;
            for (int i = 0; i < nodes.Count; i++)
            {
                n = nodes[i];
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    m = nodes[j];
                    if (m.Pid == n.Id)
                    {
                        n.Children.Add(m);
                        m.Parent = n;
                    }
                    else if(n.Pid == m.Id)
                    {
                        m.Children.Add(n);
                        n.Parent = m;
                    }
                }
            }

            foreach (var item in nodes)
            {
                SetNodeIcon(item);
            }

            return nodes;

        }

        public void AddNode(List<TreeNode> nodes, TreeNode node, int defaultExpandLevel, int currentLevel)
        {
            nodes.Add(node);

            if (defaultExpandLevel >= currentLevel)
            {
                node.SetExpand(true);
            }

            if (node.IsLeaf())
            {
                return;
            }

            foreach (var item in node.Children)
            {
                AddNode(nodes, item, defaultExpandLevel, currentLevel + 1);
            }
        }


        public List<TreeNode> GetRootNodes(List<TreeNode> nodes)
        {
            var roots = new List<TreeNode>();
            foreach (var item in nodes)
            {
                if (item.IsRoot())
                {
                    roots.Add(item);
                }
            }

            return roots;
        }

        public void SetNodeIcon(TreeNode node)
        {
            if (node.Children.Count > 0)
            {
                node.Type = TreeNode.NODE_TYPE_G;
                node.Icon = node.IsExpand ? "tree_ex.png" : "tree_ec.png";
            }
            else
            {
                node.Type = TreeNode.NODE_TYPE_N;
            }
        }

    }
}
