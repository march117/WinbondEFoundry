using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbModel.Util
{
    public class SecureUtil
    {
        /// <summary>
        /// 編碼字串
        /// </summary>
        /// <param name="str">未編碼字串</param>
        /// <param name="key">key</param>
        /// <returns>編碼後字串</returns>
        public static string Encrypt(string str , string key = "Thesys@123"){
            string encrypt = string.Empty;
            
            using(DbEntities db = new DbEntities()){
                encrypt = db.Database.SqlQuery<string>("select encryptPWD(@key,@str)", key, str).FirstOrDefault();
            }

            return encrypt;
        }

        /// <summary>
        /// 解碼字串
        /// </summary>
        /// <param name="str">已編碼字串</param>
        /// <param name="key">key</param>
        /// <returns>解碼後字串</returns>
        public static string Decrypt(string str, string key = "Thesys@123")
        {
            string decrypt = string.Empty;

            using (DbEntities db = new DbEntities())
            {
                decrypt = db.Database.SqlQuery<string>("select decryptPWD(@key,@str)", key, str).FirstOrDefault();
            }

            return decrypt;
        }
    }
}
