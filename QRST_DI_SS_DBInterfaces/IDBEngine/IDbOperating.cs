﻿using QRST_DI_SS_Basis;
using QRST_DI_SS_Basis.MetaData;

namespace QRST_DI_SS_DBInterfaces.IDBEngine
{
    public interface IDbOperating
    {

        /// <summary>
        /// 获取相应数据库的SQLiteBaseUtilities操作对象
        /// </summary>
        /// <param name="dbtype"></param>
        /// <returns></returns>
        IDbBaseUtilities GetSubDbUtilities(EnumDBType dbtype);

        IDbBaseUtilities GetsqlBaseObj(string dbName);

        /// <summary>
        /// 获取数据库服务地址IP
        /// </summary>
        /// <returns></returns>
        string GetDbServerIp();
        #region 判断表或编码是否存在
        /// <summary>
        /// 判断该表是否存在
        /// </summary>
        /// <param name="TableName">表名称</param>
        bool JudgeTableExist(string TableName, EnumDBType dbtype);

        /// <summary>
        /// 判断编码是否为表的编码
        /// </summary>
        /// <param name="TableCode">表编码</param>
        bool JudgeTableCodeExist(string TableCode, EnumDBType dbtype);

        #endregion

        #region 固定的表名与表编码转换

        /// <summary>
        ///依据表名获取表的编码 
        /// </summary>
        /// <param name="TableName">表名称</param>
        /// <returns>表编码</returns>
        string GetTableCodeFromTableName(string TableName, EnumDBType dbtype);

        /// <summary>
        /// 依据编码获取表的名称
        /// </summary>
        /// <param name="TableCode">表编码</param>
        /// <returns>表名</returns>
        //依据表编码获取表名称
        string GetTableNameFromTableCode(string TableCode, EnumDBType dbtype);

        /// <summary>
        /// 依据记录获取所有表名称
        /// </summary>
        /// <param name="TableCode">记录编码</param>
        /// <returns>所有表名</returns>
        string GetTableNameFromColumnCode(string TableCode, EnumDBType dbtype);

        /// <summary>
        /// 依据记录获取当前表名称
        /// </summary>
        /// <returns></returns>
        string GetCurrentTableNameFromColumnCode(string TableCode, EnumDBType dbtype);
        #endregion 固定的表名与表编码转换

        #region 根据表名或者编码获取新的表记录编码
        /// <summary>
        /// 依据表名插入表的新纪录编码
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns>新纪录编码</returns>
        string GetNewQrstCodeFromTableName(string TableName, EnumDBType dbtype);
        ////////////////////////////////////////////////////////////////   lxl 15:01

        /// <summary>
        /// 依据表名插入表的新纪录编码的前缀
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns>新纪录编码的前缀</returns>
        string GetPreNewColumnCodeFromTableName(string TableName, EnumDBType dbtype);
        ////////////////////////////////////////////////////////////////   lxl 15:01
        /// <summary>
        /// 依据表名插入表的新纪录编码的前缀
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns>新纪录编码的前缀</returns>
        string GetPreNewColumnCodeFromTableName(string TableName, EnumDBType dbtype, out int newID);


        int GetNewID(string TableName, EnumDBType dbtype);

        /// <summary>
        /// 依据编码插入表的新纪录编码
        /// </summary>
        /// <param name="TableCode">表编码</param>
        /// <returns>表的新纪录编码</returns>
        string GetNewColumnCodeFromTableCode(string TableCode, EnumDBType dbtype);
        #endregion 根据表名或者编码获取新的表记录编码

        #region 生成数据表或者辅助表的编码

        /// <summary>
        /// 生成数据表的code
        /// </summary>
        /// <param name="DataType">数据表类型</param>
        /// <param name="tableName">表名</param>
        /// <returns>表编码</returns>
        string GetNewTableCode(EnumDataTypes DataType, string tableName, EnumDBType dbtype);

        /// <summary>
        /// 生成辅助表的code
        /// </summary>
        /// <param name="hypTableName">辅助表表名</param>
        /// <returns>辅助表表编码</returns>
        string GetNewHypTableCode(string hypTableName, EnumDBType dbtype);
        #endregion 生成数据表或者辅助表的编码

        #region 表时间同步到本地

        void SynDBTimeToLocal();

        #endregion

        #region 表锁

        bool LockTable(string tablename, EnumDBType dbtype);

        void UnlockAnyTable(EnumDBType dbtype);

        bool UnlockTable(string tablename, EnumDBType dbtype);

        #endregion


        /// <summary>
        /// 数据库连接地址相对转绝对
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string GetAbsoluteDbCon(string path);

        /// <summary>
        /// 获取数据根路径.目前单机版根路径获取，后续可以将多机版合并至此
        /// </summary>
        /// <returns></returns>
        string GetDbRootDataPath();
    }
}
