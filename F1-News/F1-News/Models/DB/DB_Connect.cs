using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models.DB {
    public class DB_Connect {
        private static string IP = "remotemysql.com";
        private static string port = "3306";
        private static string database = "YWmsmiMpWs";
        private static string user = "YWmsmiMpWs";
        private static string password = "MtY9nfZ8uI";

        public static string connectionStr = (port == "") ? $"server={IP};port={port};database={database};uid={user};password={password}"
                                            : $"server={IP};database={database};uid={user};password={password}";
    }
}
