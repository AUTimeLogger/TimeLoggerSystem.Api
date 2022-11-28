namespace AUTimeManagement.Api.Management.Api.Service
{
    public class StringSaniziter
    {
        public static string Clean(string dirtyString)
        {
            return new string(dirtyString.Where(char.IsLetterOrDigit).ToArray());
        }
    }
}
