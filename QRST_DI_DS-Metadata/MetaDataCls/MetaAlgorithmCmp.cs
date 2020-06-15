﻿using System;
using System.Text;
using System.Xml;
using QRST_DI_DS_Metadata.MetaDataDefiner.Dal;
using System.Data;
using System.Data.Common;
using QRST_DI_Resources;
using QRST_DI_SS_Basis;
using QRST_DI_SS_Basis.MetaData;
using QRST_DI_SS_DBInterfaces.IDBEngine;

namespace QRST_DI_DS_Metadata.MetaDataCls
{
    public class MetaAlgorithmCmp:MetaData
    {

        public string[] algorithmCmpAttributes = new string[]
        {
            "DataTransModel","Artificial","AlgorithmName","DllName","ChsName","AlgorithmDesc","Version"
        };

        public string[] algorithmCmpAttributesValues;     //基本属性
        public MetaAlogrithmpara[] paraList;                    //组件参数信息
        public MetaDataParaUtility[] utilityList;                 //使用信息

        public MetaDataPublicInfo publicInfo = null;                  //用户信息

		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set;
			get;
		}
		/// <summary>
		/// 
		/// </summary>
		public string DataTransModel
		{
			set{ algorithmCmpAttributesValues[0]=value;}
			get{return algorithmCmpAttributesValues[0];}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Artificial
		{
			set{ algorithmCmpAttributesValues[1]=value;}
			get{return algorithmCmpAttributesValues[1];}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AlgorithmName
		{
			set{ algorithmCmpAttributesValues[2]=value;}
			get{return algorithmCmpAttributesValues[2];}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DllName
		{
			set{algorithmCmpAttributesValues[3]=value;}
			get{return algorithmCmpAttributesValues[3];}
		}
		/// <summary>
		/// 
		/// </summary>
		public string  ChsName
		{
			set{ algorithmCmpAttributesValues[4]=value.ToString();}
			get
            {
                return algorithmCmpAttributesValues[4];
            }
		}
		/// <summary>
		/// 
		/// </summary>
		public string AlgorithmDesc
		{
			set{algorithmCmpAttributesValues[5]=value;}
			get{return algorithmCmpAttributesValues[5];}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Version
		{
			set{algorithmCmpAttributesValues[6]=value;}
			get{return algorithmCmpAttributesValues[6];}
		}
		/// <summary>
		/// 
		/// </summary>
	
    

        
        #region Method
        public MetaAlgorithmCmp()
        {
            algorithmCmpAttributesValues = new string[algorithmCmpAttributes.Length];
        }
    
        /// <summary>
        /// 读取算法组件中的xml信息
        /// </summary>
        /// <param name="fileName">XML文件名</param>
        /// <param name="otherParameters">其它属性值,如xml中没有的“文件路径”等</param>
        /// <param name="output_attribute_string">输出属性值</param>
        public override void ReadAttributes(string fileName)
        {
             XmlDocument root = new XmlDocument();
             try
             {
                 root.Load(fileName);
             }
             catch (System.Exception ex)
             {
                 throw new Exception("xml文件损坏，请检查！");
             }
             XmlNode node = null;

             try
             {
                 for (int i = 0; i < algorithmCmpAttributes.Length; i++)
                 {
                     node = root.GetElementsByTagName(algorithmCmpAttributes[i]).Item(0);
                     if (node == null)
                     {
                         algorithmCmpAttributesValues[i] = "";
                     }
                     else
                     {
                         algorithmCmpAttributesValues[i] = node.InnerText;
                     }
                 }
                 //获取参数信息
                 XmlNodeList paranodeLst = root.GetElementsByTagName("Parameter");
                 paraList = new MetaAlogrithmpara[paranodeLst.Count];
                 for (int i = 0; i < paranodeLst.Count; i++)
                 {
                     paraList[i] = new MetaAlogrithmpara();
                     paraList[i].ReadAttribute(paranodeLst[i]);
                 }
                 //获取utility信息
                 XmlNodeList utilityArr = root.GetElementsByTagName("Utility");
                 utilityList = new MetaDataParaUtility[utilityArr.Count];
                 for (int j = 0; j < utilityArr.Count; j++)
                 {
                     utilityList[j] = new MetaDataParaUtility();
                     utilityList[j].ReadAttribute(utilityArr[j]);
                 }

                 //如果有用户上传信息，则读取用户上传信息
                 XmlNodeList publicInfoArr = root.GetElementsByTagName("PublicInfo");
                 if (publicInfoArr.Count > 0)
                 {
                     publicInfo = new MetaDataPublicInfo();
                     publicInfo.ReadAttribute(publicInfoArr[0]);
                 }
             }
            catch(Exception ex)
             {
                 throw new Exception("读取元数据信息出错"+ex.ToString());
            }
        }

        /// <summary>
        /// 将模型算法元数据信息导入数据库,
        /// </summary>
        /// <param name="sqlBase"></param>
        /// <returns></returns>
        public override void ImportData(IDbBaseUtilities sqlBase)
        {
            try
            {
                tablecode_Dal tablecode = new tablecode_Dal(sqlBase);
                //TableLocker dblock = new TableLocker(sqlBase);
                //dblock.LockTable("isdb_useralgorithm");
                Constant.IdbOperating.LockTable("isdb_useralgorithm", EnumDBType.MIDB);
                ID = sqlBase.GetMaxID("ID", "isdb_useralgorithm");
                QRST_CODE = tablecode.GetDataQRSTCode("isdb_useralgorithm", ID);
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into isdb_useralgorithm(");
                strSql.Append("ID,DataTransModel,Artificial,AlgorithmName,DllName,ChsName,AlgorithmDesc,Version,QRST_CODE)");
                strSql.Append(" values (");
                strSql.Append(String.Format("{0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", ID, DataTransModel, Artificial,
                    AlgorithmName, DllName, ChsName, AlgorithmDesc, Version, QRST_CODE));
                //strSql.Append(String.Format("@ID,@DataTransModel,@Artificial,@AlgorithmName,@DllName,@ChsName,@AlgorithmDesc,@Version,@QRST_CODE)");
                //DbParameter[] parameters = {
                //    new MySqlParameter("@ID", MySqlDbType.Int32,7),
                //new MySqlParameter("@DataTransModel", MySqlDbType.Text),
                //new MySqlParameter("@Artificial", MySqlDbType.Text),
                //new MySqlParameter("@AlgorithmName", MySqlDbType.Text),
                //new MySqlParameter("@DllName", MySqlDbType.Text),
                //new MySqlParameter("@ChsName", MySqlDbType.Text),
                //new MySqlParameter("@AlgorithmDesc", MySqlDbType.Text),
                //new MySqlParameter("@Version", MySqlDbType.VarChar,200),
                //new MySqlParameter("@QRST_CODE", MySqlDbType.VarChar,50)};
                //           parameters[0].Value = ID;
                //           parameters[1].Value = DataTransModel;
                //           parameters[2].Value = Artificial;
                //           parameters[3].Value = AlgorithmName;
                //           parameters[4].Value = DllName;
                //           parameters[5].Value = ChsName;
                //           parameters[6].Value = AlgorithmDesc;
                //           parameters[7].Value = Version;
                //           parameters[8].Value = QRST_CODE;
                //sqlBase.ExecuteSql(strSql.ToString(), parameters);
                sqlBase.ExecuteSql(strSql.ToString());
                Constant.IdbOperating.UnlockTable("isdb_useralgorithm",EnumDBType.MIDB);

                //将参数信息添加到表
                for (int i = 0; i < paraList.Length; i++)
                {
                    Constant.IdbOperating.LockTable("isdb_alogrithmpara", EnumDBType.MIDB);
                    paraList[i].ParaID = QRST_CODE;
                    paraList[i].ID = sqlBase.GetMaxID("ID", " isdb_alogrithmpara");
                    StringBuilder strSql1 = new StringBuilder();
                    strSql1.Append("insert into isdb_alogrithmpara(");
                    strSql1.Append("ID,ParaName,ParaChsName,Description,ParaType,ParaValue,ParaSource,ParaID)");
                    strSql1.Append(" values (");
                    strSql1.Append(string.Format("{0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}')", paraList[i].ID,
                        paraList[i].ParaName, paraList[i].ParaChsName, paraList[i].Description, paraList[i].ParaType,
                        paraList[i].ParaValue, paraList[i].ParaSource, paraList[i].ParaID));
                    //               strSql1.Append("@ID,@ParaName,@ParaChsName,@Description,@ParaType,@ParaValue,@ParaSource,@ParaID)");
                    //               MySqlParameter[] parameters1 = {
                    //new MySqlParameter("@ID", MySqlDbType.Int32,7),
                    //new MySqlParameter("@ParaName", MySqlDbType.VarChar,200),
                    //new MySqlParameter("@ParaChsName", MySqlDbType.Text),
                    //new MySqlParameter("@Description", MySqlDbType.Text),
                    //new MySqlParameter("@ParaType", MySqlDbType.VarChar,200),
                    //new MySqlParameter("@ParaValue", MySqlDbType.Text),
                    //new MySqlParameter("@ParaSource", MySqlDbType.Text),
                    //new MySqlParameter("@ParaID", MySqlDbType.VarChar,50)};
                    //               parameters1[0].Value = paraList[i].ID;
                    //               parameters1[1].Value = paraList[i].ParaName;
                    //               parameters1[2].Value = paraList[i].ParaChsName;
                    //               parameters1[3].Value = paraList[i].Description;
                    //               parameters1[4].Value = paraList[i].ParaType;
                    //               parameters1[5].Value = paraList[i].ParaValue;
                    //               parameters1[6].Value = paraList[i].ParaSource;
                    //               parameters1[7].Value = paraList[i].ParaID;
                    //               sqlBase.ExecuteSql(strSql1.ToString(), parameters1);
                    sqlBase.ExecuteSql(strSql1.ToString());
                    Constant.IdbOperating.UnlockTable("isdb_alogrithmpara",EnumDBType.MIDB);
                }
                //将utility信息添加到表
                for (int j = 0; j < utilityList.Length; j++)
                {
                    if (utilityList[j] == null)
                        continue;
                    Constant.IdbOperating.LockTable("isdb_parautility",EnumDBType.MIDB);
                    utilityList[j].ID = sqlBase.GetMaxID("ID", " isdb_parautility");
                    utilityList[j].ParaID = QRST_CODE;

                    StringBuilder strSql2 = new StringBuilder();
                    strSql2.Append("insert into isdb_parautility(");
                    strSql2.Append("ID,Satellite,Sensor,Resolution,ParaID)");
                    strSql2.Append(" values (");
                    strSql2.Append(string.Format("{0},'{1}','{2}','{3}','{4}')", utilityList[j].ID,
                        utilityList[j].Satellite, utilityList[j].Sensor, utilityList[j].Resolution,
                        utilityList[j].ParaID));
     //               strSql2.Append("@ID,@Satellite,@Sensor,@Resolution,@ParaID)");
     //               MySqlParameter[] parameters2 = {
					//new MySqlParameter("@ID", MySqlDbType.Int32,7),
					//new MySqlParameter("@Satellite", MySqlDbType.VarChar,200),
					//new MySqlParameter("@Sensor", MySqlDbType.VarChar,50),
					//new MySqlParameter("@Resolution", MySqlDbType.Text),
					//new MySqlParameter("@ParaID", MySqlDbType.VarChar,50)};
     //               parameters2[0].Value = utilityList[j].ID;
     //               parameters2[1].Value = utilityList[j].Satellite;
     //               parameters2[2].Value = utilityList[j].Sensor;
     //               parameters2[3].Value = utilityList[j].Resolution;
     //               parameters2[4].Value = utilityList[j].ParaID;
                    //sqlBase.ExecuteSql(strSql2.ToString(), parameters2);
                    sqlBase.ExecuteSql(strSql2.ToString());
                    Constant.IdbOperating.UnlockTable("isdb_parautility",EnumDBType.MIDB);
                }
                //存储公共信息
                if(publicInfo != null)
                {
                    publicInfo.ImportData(sqlBase, QRST_CODE, tablecode);
                }
            }
            catch(Exception ex)
            {
                //撤销之前的操作，抛出异常
                sqlBase.ExecuteSql(string.Format("delete from isdb_useralgorithm where  QRST_CODE = '{0}'",QRST_CODE));
                throw new Exception("元数据导入失败"+ex.ToString());
            }
        }

        /// <summary>
        /// 获取算法组件元数据对象  zxw 20130624
        /// </summary>
        /// <param name="qrst_code"></param>
        /// <returns></returns>
        public override void GetModel(string qrst_code,IDbBaseUtilities sqlBase)
        {
            //根据qrst_code获取数据库连接对象
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,DataTransModel,Artificial,AlgorithmName,DllName,ChsName,AlgorithmDesc,Version,QRST_CODE from madb_algorithmcmp ");
            strSql.AppendFormat(" where QRST_CODE= '{0}' ",qrst_code);

            using (DataSet ds = sqlBase.GetDataSet(strSql.ToString()))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                    {
                        ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                    }
                    DataTransModel = ds.Tables[0].Rows[0]["DataTransModel"].ToString();
                    Artificial = ds.Tables[0].Rows[0]["Artificial"].ToString();
                    AlgorithmName = ds.Tables[0].Rows[0]["AlgorithmName"].ToString();
                    DllName = ds.Tables[0].Rows[0]["DllName"].ToString();
                    ChsName = ds.Tables[0].Rows[0]["ChsName"].ToString();
                    AlgorithmDesc = ds.Tables[0].Rows[0]["AlgorithmDesc"].ToString();
                    Version = ds.Tables[0].Rows[0]["Version"].ToString();
                    QRST_CODE = ds.Tables[0].Rows[0]["QRST_CODE"].ToString();
                    IsCreated = true;
                }
                else
                {
                    IsCreated = false;
                }
            }
        }
        #endregion
    }
}
