using ServerlessBlog.Application.Models;

namespace ServerlessBlog.Application
{
    internal static class Constants
    {
        public static class Posts
        {
            public static readonly SemanticVersion CurrentDocumentVersion = new SemanticVersion
                {Major = 1, Minor = 0, Patch = 0};
        }
    }
}
