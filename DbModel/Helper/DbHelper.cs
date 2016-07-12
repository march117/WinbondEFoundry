using System;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Collections.Generic;
using DbModel.Util;
using System.Data.Entity.Validation;

namespace DbModel.Helper
{
    public class DbHelper
    {
        #region 新增

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <typeparam name="T">Entity物件</typeparam>
        /// <param name="model">新增的資料</param>
        public static void Insert<T>(T model) where T : class
        {
            using (DbContext db = new EFoundryContext())
            {
                DbSet dbSet = GetDbSet<T>(db);
                Type modelType = typeof(T);

                #region 將指定欄位進行加密

                List<string> memberName = modelType.GetMembers().Select(o=>o.Name).ToList();
                List<string> encryptCols = EncryptColumns();
                if (encryptCols.Any(o=>memberName.Contains(o)))
                {
                    foreach (string col in encryptCols.Where(o=>memberName.Contains(o)))
                    {
                        MethodInfo getPwd = modelType.GetMethod("get_"+col);
                        string pwd = (string)getPwd.Invoke(model, null);
                        if (!string.IsNullOrEmpty(pwd))
                        {
                            string encryptPwd = SecureUtil.Encrypt(pwd);
                            MethodInfo setPwd = modelType.GetMethod("set_" + col);
                            setPwd.Invoke(model, new object[] { encryptPwd });
                        }
                    }
                }

                #endregion

                dbSet.Add(model);
                db.SaveChanges();
                
            }
        }

        #endregion

        #region 更新
        
        /// <summary>
        /// 更新資料，需在Metadata定義KeyAttribute
        /// </summary>
        /// <typeparam name="T">Entity物件</typeparam>
        /// <param name="model">更新資料</param>
        public static void Update<T>(T model)where T : class
        {
            using (DbContext db = new EFoundryContext())
            {
                Type modelType = typeof(T);
                DbSet<T> dbSet = GetDbSet<T>(db);
                object keyVal = GetPKColumnValue<T>(model);                
                T old = dbSet.Find(keyVal);
                //避免Create Date被覆寫
                MethodInfo getCreateDt = typeof(T).GetMethod("get_CreateDate");
                DateTime oldDt = (DateTime)getCreateDt.Invoke(old, null);
                if (oldDt != null)
                {
                    MethodInfo setCreateDt = typeof(T).GetMethod("set_CreateDate");
                    setCreateDt.Invoke(model, new object[]{oldDt});
                }

                db.Entry(old).CurrentValues.SetValues(model);                

                db.SaveChanges();
            }
        }

        #endregion

        #region 刪除

        /// <summary>
        /// 刪除資料,需在Metadata定義KeyAttribute
        /// </summary>
        /// <typeparam name="T">Entity物件</typeparam>
        /// <param name="id">Primary Key</param>
        public static void Delete<T>(int id) where T : class
        {
            Delete<T>(new string[] { id.ToString() });
        }

        /// <summary>
        /// 刪除資料,需在Metadata定義KeyAttribute
        /// </summary>
        /// <typeparam name="T">Entity物件</typeparam>
        /// <param name="id">Primary Key</param>
        public static void Delete<T>(string id) where T : class
        {
            Delete<T>(new string[] { id});
        }

        /// <summary>
        /// 刪除資料,需在Metadata定義KeyAttribute
        /// </summary>
        /// <typeparam name="T">Entity物件</typeparam>
        /// <param name="id">Primary Key</param>
        public static void Delete<T>(int[] id) where T : class
        {
            Delete<T>(id.Select(o => o.ToString()).ToArray());
        }

        /// <summary>
        /// 刪除資料,需在Metadata定義KeyAttribute
        /// </summary>
        /// <typeparam name="T">Entity物件</typeparam>
        /// <param name="id">Primary Key</param>
        public static void Delete<T>(string[] id)where T : class
        {
            using (DbContext db = new EFoundryContext())
            {
                MethodInfo method = typeof(DbEntities).GetMethod("get_"+typeof(T).Name);
                DbSet<T> dbSet = GetDbSet<T>(db);
                if (dbSet != null)
                {
                    Func<T,bool> pre = delegate(T model)
                    {
                        string pkCol = GetPKColumnName(typeof(T));
                        object pkVal = GetPKColumnValue<T>(model);
                        return id.Contains(pkVal.ToString());
                    };
                    dbSet.RemoveRange(dbSet.Where(pre).ToList());
                    db.SaveChanges();
                }                
            }
        }

        #endregion

        #region 取得單一物件

        /// <summary>
        /// 取得單一物件
        /// </summary>
        /// <typeparam name="T">Entity物件</typeparam>
        /// <param name="id">Primary Key</param>
        /// <returns>物件</returns>
        public static T GetItem<T>(string id) where T:class
        {
            T item = null;
            using (DbContext db = new EFoundryContext())
            {
                Type modelType = typeof(T);
                DbSet<T> dbSet = GetDbSet<T>(db);
                if (dbSet != null)
                {
                    string pkCol = GetPKColumnName(typeof(T));
                    Func<T, bool> pre = delegate(T model)
                    {
                        string pkVal = GetPKColumnValue<T>(model).ToString();
                        return id == pkVal;
                    };

                    item = dbSet.Where(pre).FirstOrDefault();
                }                
            }
            return item;
        }

        #endregion

        #region 取得列表

        /// <summary>
        /// 取得不分頁列表
        /// </summary>
        /// <typeparam name="T">Entity物件</typeparam>
        /// <returns>列表</returns>
        public static List<T> GetList<T>() where T : class
        {
            return GetList<T>(0,0);
        }

        /// <summary>
        /// 取得分頁列表
        /// </summary>
        /// <typeparam name="T">Entity物件</typeparam>
        /// <param name="pageIdx">頁次</param>
        /// <param name="pageSize">每頁比數</param>
        /// <returns>分頁列表</returns>
        public static List<T> GetList<T>(int pageIdx = 0,int pageSize = 0) where T: class{
            List<T> list = null;

            using (DbContext db = new EFoundryContext())
            {
                DbSet<T> dbSet = GetDbSet<T>(db);
                IQueryable<T> qry = dbSet.AsQueryable();
                if (pageIdx != 0 && pageSize != 0)
                {
                    qry = qry
                            .Skip((pageIdx - 1) * pageSize)
                            .Take(pageSize);
                }

                list = qry.ToList<T>();
            }

            return list;
        }

        #endregion

        #region Private Function

        static string GetPKColumnName(Type modelType)
        {            
            Type metadataType = Type.GetType("DbModel." + modelType.Name + "MD");
            PropertyInfo[] props = metadataType.GetProperties();
            string keyCol = string.Empty;
            foreach (PropertyInfo prop in props)
            {
                if (prop.IsDefined(typeof(KeyAttribute), false))
                {
                    keyCol = prop.Name;             
                }
            }

            return keyCol;
        }

        static object GetPKColumnValue<T>(T model) where T : class
        {
            Type modelType = typeof(T);
            string pkCol =  GetPKColumnName(modelType);
            object keyVal = modelType.GetProperty(pkCol).GetValue(model, null);
            return keyVal;
        }

        static DbSet<T> GetDbSet<T>(DbContext db) where T : class{
            MethodInfo method = typeof(DbEntities).GetMethod("get_"+typeof(T).Name);
            if(method != null){
                return (DbSet<T>)method.Invoke(db,null);
            }
            return null;
            
        }


        static List<string> EncryptColumns()
        {
            List<string> cols = new List<string>();
            cols.Add("UserPwd");
            cols.Add("CheckPw");
            return cols;
        }

        #endregion
    }
}
