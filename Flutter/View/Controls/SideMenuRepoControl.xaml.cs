namespace Flutter.View.Controls
{
    public partial class SideMenuRepoControl
    {
        public SideMenuRepoControl()
        {
            InitializeComponent();

            localBranchTree.BranchType = EBranchType.Local;
            remoteBranchTree.BranchType = EBranchType.Remote;
        }
    }
}
