﻿using System;
using System.Text;
using System.Xml;
using QRST_DI_DS_Metadata.MetaDataDefiner.Dal;
using QRST_DI_Resources;
using QRST_DI_SS_Basis;
using QRST_DI_SS_Basis.MetaData;
using QRST_DI_SS_DBInterfaces.IDBEngine;

namespace QRST_DI_DS_Metadata.MetaDataCls
{
    public class MetaDataUserDocument:MetaData
    {
        public MetaDataUserDocument()
        {
            userDocumentValues = new string[userDocumentAttributes.Length];
        }

        public string[] userDocumentAttributes = new string[]
        {
            "documentname","author","filetime",
            "keywords","fileabstract","programname",
            "ispublic","gfb","FileName","filesize","documenttype"
        };

        public string[] userDocumentValues;
        public MetaDataPublicInfo publicInfo;  //公共属性值
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
        public string documentname
        {
            set { userDocumentValues[0] = value; }
            get { return userDocumentValues[0]; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string author
        {
            set { userDocumentValues[1]= value; }
            get { return userDocumentValues[1]; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime filetime
        {
            set { userDocumentValues[2] = value.ToString(); }
            get
            {
                    DateTime dt = DateTime.Parse(userDocumentValues[2]);
                    return dt;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string keywords
        {
            set { userDocumentValues[3] = value; }
            get { return userDocumentValues[3]; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fileabstract
        {
            set { userDocumentValues[4] = value; }
            get { return userDocumentValues[4]; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string programname
        {
            set { userDocumentValues[5] = value; }
            get { return userDocumentValues[5]; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ispublic
        {
            set { userDocumentValues[6] = value; }
            get { return userDocumentValues[6]; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int gfb
        {
            set { userDocumentValues[7] = value.ToString(); }
            get
            {
                try
                {
                    return int.Parse(userDocumentValues[7]);
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            set { userDocumentValues[8] = value; }
            get { return userDocumentValues[8]; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string filesize
        {
            set { userDocumentValues[9] = value; }
            get { return userDocumentValues[9]; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string documenttype
        {
            set { userDocumentValues[10] = value; }
            get { return userDocumentValues[10]; }
        }
        
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
                //读取一般信息
                for (int i = 0; i < userDocumentValues.Length; i++)
                {
                    node = root.GetElementsByTagName(userDocumentAttributes[i]).Item(0);
                    if (node == null)
                    {
                        userDocumentValues[i] = "";
                    }
                    else
                    {
                        userDocumentValues[i] = node.InnerText;
                    }
                }
                //读取公共信息
                XmlNodeList publicInfoArr = root.GetElementsByTagName("PublicInfo");
                if (publicInfoArr.Count > 0)
                {
                    publicInfo = new MetaDataPublicInfo();
                    publicInfo.ReadAttribute(publicInfoArr[0]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("读取元数据信息失败！" + ex.ToString());
            }
        }

        public override void ImportData(IDbBaseUtilities sqlBase)
        {
            try
            {
                //导入基本信息
                //TableLocker dblock = new TableLocker(sqlBase);
                Constant.IdbOperating.LockTable("isdb_document",EnumDBType.MIDB);
                tablecode_Dal tablecode = new tablecode_Dal(sqlBase);
                ID = sqlBase.GetMaxID("ID", "isdb_document");
                QRST_CODE = tablecode.GetDataQRSTCode("isdb_document", ID);

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into isdb_document(");
                strSql.Append("ID,documentname,author,filetime,keywords,fileabstract,programname,gfb,FileName,filesize,QRST_CODE,documenttype)");
                strSql.Append(" values (");
                strSql.Append("@ID,@documentname,@author,@filetime,@keywords,@fileabstract,@programname,@gfb,@FileName,@filesize,@QRST_CODE,@documenttype)");
                strSql.Append(String.Format(
                    "{0},'{1}','{2}','{3}','{4}','{5}','{6}',{7},'{8}','{9}','{10}','{11}')", ID, documentname, author,
                    filetime.ToString("yyyy-MM-dd HH:mm:ss"), keywords, fileabstract, programname, gfb, FileName, filesize, QRST_CODE, documenttype));
     //           MySqlParameter[] parameters = {
					//new MySqlParameter("@ID", MySqlDbType.Int32,7),
					//new MySqlParameter("@documentname", MySqlDbType.Text),
					//new MySqlParameter("@author", MySqlDbType.VarChar,200),
					//new MySqlParameter("@filetime", MySqlDbType.DateTime),
					//new MySqlParameter("@keywords", MySqlDbType.Text),
					//new MySqlParameter("@fileabstract", MySqlDbType.Text),
					//new MySqlParameter("@programname", MySqlDbType.Text),
					//new MySqlParameter("@gfb", MySqlDbType.Int32,7),
					//new MySqlParameter("@FileName", MySqlDbType.VarChar,45),
					//new MySqlParameter("@filesize", MySqlDbType.VarChar,45),
					//new MySqlParameter("@QRST_CODE", MySqlDbType.VarChar,45),
     //               new MySqlParameter("@documenttype", MySqlDbType.Text)};
     //           parameters[0].Value = ID;
     //           parameters[1].Value = documentname;
     //           parameters[2].Value = author;
     //           parameters[3].Value = filetime;
     //           parameters[4].Value = keywords;
     //           parameters[5].Value = fileabstract;
     //           parameters[6].Value = programname;
     //           parameters[7].Value = gfb;
     //           parameters[8].Value = FileName;
     //           parameters[9].Value = filesize;
     //           parameters[10].Value = QRST_CODE;
     //           parameters[11].Value = documenttype;
                sqlBase.ExecuteSql(strSql.ToString());
                Constant.IdbOperating.UnlockTable("isdb_document",EnumDBType.MIDB);

                //存储公共信息
                if (publicInfo != null)
                {
                    publicInfo.ImportData(sqlBase, QRST_CODE, tablecode);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("元数据导入失败" + ex.ToString());
            }
        }
    }
}
