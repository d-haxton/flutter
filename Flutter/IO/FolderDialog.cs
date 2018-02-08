using Flutter.Library.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Flutter.IO
{
    public class FolderDialog : IFolderDialog
    {
        private readonly CommonOpenFileDialog fileDialog;

        public FolderDialog()
        {
            fileDialog = new CommonOpenFileDialog();
        }

        public string SelectFolder(string description)
        {
            fileDialog.IsFolderPicker = true;
            return fileDialog.ShowDialog() == CommonFileDialogResult.Ok
                ? fileDialog.FileName
                : "";
        }
    }
}
