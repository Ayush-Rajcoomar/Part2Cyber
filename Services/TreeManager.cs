using System.Collections.Generic;
using WPFModernVerticalMenu.Models;

namespace WPFModernVerticalMenu.Services
{
    public class TreeManager
    {
        public TreeNode Root { get; private set; } = new TreeNode("Root");

        public void AddIssue(ReportedIssue issue)
        {
            // Find or create the location node
            TreeNode locationNode = Root.FindChild(issue.Location);
            if (locationNode == null)
            {
                locationNode = new TreeNode(issue.Location);
                Root.AddChild(locationNode);
            }

            // Find or create the category node
            TreeNode categoryNode = locationNode.FindChild(issue.Category);
            if (categoryNode == null)
            {
                categoryNode = new TreeNode(issue.Category);
                locationNode.AddChild(categoryNode);
            }

            // Add the issue as a leaf node
            TreeNode issueNode = new TreeNode(issue.Description) { IssueDetails = issue };
            categoryNode.AddChild(issueNode);
        }
    }
}
