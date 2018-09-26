namespace QuickHelper.Configuration
{
    public class JsonAppConfig : IAppConfig
    {
        public static JsonAppConfig EmptyConfig = new JsonAppConfig();

        public string SemicolonSeparatedFilePaths { get; set; }
    }

}