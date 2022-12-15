namespace ApiDocumentationUsingRedocAndSwagger
{
    public static class SampleData
    {
        static SampleData() => Users = new List<UserAccount>();

        public static List<UserAccount> Users { get; set; }
    }
}
