namespace Thundire.MVVM.WPF.Abstractions.Dialogs
{
    public interface IIODialogsService
    {
        bool SaveFile(string title, out string selectedFile, string? defaultFileName = null, string filter = "All files (*.*)|*.*");
        bool OpenFile(string title, out string selectedFile, string filter = "All files (*.*)|*.*");
    }
}