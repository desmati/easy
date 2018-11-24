//using Microsoft.Extensions.Configuration;
//using System.Data;

//namespace Microsoft.AspNetCore.Identity
//{
//    public static partial class DataStore
//    {
//        private static ApplicationUserStore users { get; set; }
//        public static ApplicationUserStore Users =>
//            users ?? (users = new ApplicationUserStore(EasyConfiguration.Get("RavenDbUrl"), EasyConfiguration.Get("DatabaseName")));
//    }

//    public class ApplicationUserStore : EasyRavenDBStorage<ApplicationUser>
//    {
//        public ApplicationUserStore(string Url, string Database) : base(Url, Database) { }
//    }
//}
