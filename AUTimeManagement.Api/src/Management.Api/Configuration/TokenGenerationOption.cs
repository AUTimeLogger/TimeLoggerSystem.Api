namespace AUTimeManagement.Api.Management.Api.Configuration
{
    public class TokenGenerationOption
    {
        public const string SectionName = "TokenGeneration";

        public string SigningSecret { get; set; } = "";
        public string ValidIssuer { get; set; } = "";
        public string ValidAudience { get; set; } = "";
        public int ExpireDuration { get; set; }
    }
}
