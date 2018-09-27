namespace QuickHelper.Configuration
{
    public interface IAppConfig
    {
        string SemicolonSeparatedFilePaths { get; set; }

        string[] AllowedExtensions { get; set; }
    }
}