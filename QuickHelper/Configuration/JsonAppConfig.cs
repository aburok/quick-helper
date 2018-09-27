namespace QuickHelper.Configuration
{
    public class JsonAppConfig : IAppConfig
    {
        public static JsonAppConfig EmptyConfig = new JsonAppConfig()
        {
            SemicolonSeparatedFilePaths = @"C:\Dropbox\Learning",
            AllowedExtensions = new string[] { ".json", ".vim", ".txt" }
        };

        public string SemicolonSeparatedFilePaths { get; set; }
        public string[] AllowedExtensions { get; set; }
    }

}