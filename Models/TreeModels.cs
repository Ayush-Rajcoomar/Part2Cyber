using System.Collections.Generic;

namespace WPFModernVerticalMenu.Models
{
    // Represents a reported issue with location, category, description, and media path
    public class ReportedIssue
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string MediaPath { get; set; }

        public ReportedIssue(string location, string category, string description, string mediaPath)
        {
            Location = location;
            Category = category;
            Description = description;
            MediaPath = mediaPath;
        }
    }

    // Represents a node in the tree
    public class TreeNode
    {
        public string Name { get; set; }
        public List<TreeNode> Children { get; set; } = new List<TreeNode>();
        public ReportedIssue IssueDetails { get; set; }

        public TreeNode(string name)
        {
            Name = name;
        }

        // Method to add a child node
        public void AddChild(TreeNode child)
        {
            Children.Add(child);
        }

        // Method to find a child node by name
        public TreeNode FindChild(string name)
        {
            return Children.Find(node => node.Name == name);
        }
    }
}
