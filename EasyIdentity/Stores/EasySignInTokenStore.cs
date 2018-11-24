//using Microsoft.Extensions.Configuration;
//using System.Data;

//namespace Microsoft.AspNetCore.Identity
//{
//    public static partial class DataStore
//    {
//        private static EasySignInTokenStore tokens { get; set; }
//        public static EasySignInTokenStore Tokens =>
//            tokens ?? (tokens = new EasySignInTokenStore(EasyConfiguration.Get("RavenDbUrl"), EasyConfiguration.Get("DatabaseName")));
//    }

//    public class EasySignInTokenStore : EasyRavenDBStorage<EasySignInToken>
//    {
//        public EasySignInTokenStore(string Url, string Database) : base(Url, Database) { }
//    }
//}
