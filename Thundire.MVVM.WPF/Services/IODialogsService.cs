using Microsoft.Win32;
using Thundire.MVVM.WPF.Services.Interfaces;

namespace Thundire.MVVM.WPF.Services
{
    public class IODialogsService : IIODialogsService
    {
        public bool SaveFile(string title, out string selectedFile, string defaultFileName = null, string filter = "All files (*.*)|*.*")
        {
            var dlg = new SaveFileDialog
            {
                Title = title,
                Filter = filter
            };

            if (!string.IsNullOrWhiteSpace(defaultFileName)) dlg.FileName = defaultFileName;

            if (dlg.ShowDialog() is not true)
            {
                selectedFile = null;
                return false;
            }

            selectedFile = dlg.FileName;
            return true;
        }

        public bool OpenFile(string title, out string selectedFile, string filter = "All files (*.*)|*.*")
        {
            var dlg = new OpenFileDialog
            {
                Title = title,
                Filter = filter
            };

            if (dlg.ShowDialog() is not true)
            {
                selectedFile = null;
                return false;
            }

            selectedFile = dlg.FileName;
            return true;
        }
    }
}