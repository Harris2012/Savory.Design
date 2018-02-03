using Newtonsoft.Json;
using Savory.Design.Api.Vo;
using Savory.Design.Repository;
using Savory.Repository.DesignDB.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Savory.Design.Api.Controllers
{
    public class DatabaseController : ApiController
    {
        public List<DatabaseVo> DBList()
        {
            List<DatabaseVo> returnValue = new List<DatabaseVo>();

            List<DesignDatabaseEntity> entityList = null;
            using (var savoryDesignDB = new SavoryDesignDBContext())
            {
                entityList = savoryDesignDB.DesignDatabase.ToList();
            }

            if (entityList != null && entityList.Count > 0)
            {
                foreach (var entity in entityList)
                {
                    var db = new DatabaseVo();

                    db.Id = entity.Id;
                    db.Name = entity.DBName;

                    returnValue.Add(db);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 查看数据库所有表
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResultModel TableList(string dbName)
        {
            JsonResultModel response = new JsonResultModel();

            List<DesignTableEntity> tableEntityList = null;
            using (var savoryDesignDB = new SavoryDesignDBContext())
            {
                tableEntityList = (from table in savoryDesignDB.DesignTable where table.DBName.Equals(dbName) select table).ToList();
            }

            if (tableEntityList != null && tableEntityList.Count > 0)
            {
                List<TableVo> tableVoList = new List<TableVo>();

                foreach (var tableEntity in tableEntityList)
                {
                    var tableVo = new TableVo();

                    tableVo.TableName = tableEntity.TableName;
                    tableVo.Description = tableEntity.Description;
                    tableVo.SchemaVersion = tableEntity.SchemaVersion;
                    tableVo.CreateTime = tableEntity.CreateTime;
                    tableVo.LastUpdateTime = tableEntity.LastUpdateTime;

                    tableVoList.Add(tableVo);
                }

                response.Data = tableVoList;
            }

            return response;
        }

        /// <summary>
        /// 查看表所有字段
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResultModel FieldList(string dbName, string tableName)
        {
            JsonResultModel response = new JsonResultModel();

            using (var savoryDesignDB = new SavoryDesignDBContext())
            {
                var items = (from table in savoryDesignDB.DesignTable
                             join schema in savoryDesignDB.TableSchema on table.SchemaVersion equals schema.SchemaVersion
                             where table.DBName.Equals(dbName) && table.TableName.Equals(tableName)
                             select schema).ToList();

                if (items != null && items.Count > 0)
                {
                    var schemaContent = items[0].SchemaContent;

                    response.Data = JsonConvert.DeserializeObject<List<FieldVo>>(schemaContent);
                }

                savoryDesignDB.TableSchema.Where(v => v.TableName.Equals(tableName)).ToList();
            }

            return response;
        }

        [HttpPost]
        public UpdateTableSchemaResponse UpdateTableSchema(UpdateTableSchemaRequest request)
        {
            if (request == null) return UpdateTableSchemaResult.RequestBodyRequired;

            if (string.IsNullOrEmpty(request.DBName)) return UpdateTableSchemaResult.DBNameRequired;

            if (string.IsNullOrEmpty(request.TableName)) return UpdateTableSchemaResult.TableNameRequired;

            if (request.FieldList == null || request.FieldList.Count == 0) return UpdateTableSchemaResult.FieldListRequired;

            using (var savoryDesignDB = new SavoryDesignDBContext())
            {
                var currentTableList = (from table in savoryDesignDB.DesignTable
                                        where table.DBName.Equals(request.DBName) && table.TableName.Equals(request.TableName) && table.DataStatus == 1
                                        select table).ToList();

                if (currentTableList == null || currentTableList.Count == 0) return UpdateTableSchemaResult.TableNotFound;

                var tableToUpdate = currentTableList[0];

                if (tableToUpdate.SchemaVersion != request.SchemaVersion) return UpdateTableSchemaResult.CurrentVersionNotMatch;

                DateTime now = DateTime.Now;
                long newSchemaVersion = long.Parse(now.ToString("yyyyMMddHHmmss"));

                var schemaEntity = new TableSchemaEntity();
                schemaEntity.DBName = request.DBName;
                schemaEntity.TableName = request.TableName;
                schemaEntity.SchemaContent = JsonConvert.SerializeObject(request.FieldList);
                schemaEntity.SchemaVersion = newSchemaVersion;
                schemaEntity.DataStatus = 1;
                schemaEntity.Description = "this is a test";
                schemaEntity.CreateUser = "zhang";
                schemaEntity.CreateTime = now;
                schemaEntity.LastUpdateUser = "zhang";
                schemaEntity.LastUpdateTime = now;

                savoryDesignDB.TableSchema.Add(schemaEntity);

                tableToUpdate.SchemaVersion = newSchemaVersion;
                tableToUpdate.LastUpdateTime = now;

                savoryDesignDB.SaveChanges();
            }

            return UpdateTableSchemaResult.Success;
        }

        [HttpPost]
        public JsonResultModel CreateDB(string dbName)
        {
            JsonResultModel response = new JsonResultModel();

            System.Threading.Thread.Sleep(15 * 1000);

            try
            {
                using (var savoryDesignDB = new SavoryDesignDBContext())
                {
                    var designDB = new DesignDatabaseEntity();
                    designDB.DBName = dbName;
                    designDB.DBType = 1;
                    designDB.Description = "this is a test";
                    designDB.DataStatus = 1;
                    designDB.CreateUser = "zhang";
                    designDB.CreateTime = DateTime.Now;
                    designDB.LastUpdateUser = "zhang";
                    designDB.LastUpdateTime = DateTime.Now;
                    savoryDesignDB.DesignDatabase.Add(designDB);

                    savoryDesignDB.SaveChanges();
                }

                response.Status = 1;
            }
            catch (Exception)
            {
                response.Message = "操作失败";
            }

            return response;
        }

        /// <summary>
        /// 新建表
        /// </summary>
        /// <param name="tableVo"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResultModel CreateTable(string dbName, TableVo tableVo)
        {
            JsonResultModel response = new JsonResultModel();

            CreateTableResult returnValue = RealCreateTable(dbName, tableVo);

            response.Status = (int)returnValue;
            response.Message = EnumExtension.GetDescription(returnValue);

            return response;
        }

        private CreateTableResult RealCreateTable(string dbName, TableVo tableVo)
        {
            if (string.IsNullOrWhiteSpace(dbName))
            {
                return CreateTableResult.DBNameRequired;
            }

            if (tableVo == null)
            {
                return CreateTableResult.RequestBodyRequired;
            }

            if (string.IsNullOrWhiteSpace(tableVo.TableName))
            {
                return CreateTableResult.TableNameRequired;
            }

            if (string.IsNullOrWhiteSpace(tableVo.Description))
            {
                return CreateTableResult.DescriptionRequired;
            }

            using (var savoryDesignDB = new SavoryDesignDBContext())
            {
                var existingTable = savoryDesignDB.DesignTable.FirstOrDefault(v => v.DBName.Equals(dbName, StringComparison.OrdinalIgnoreCase) && v.TableName.Equals(tableVo.TableName, StringComparison.OrdinalIgnoreCase) && v.DataStatus == 1);

                if (existingTable != null)
                {
                    return CreateTableResult.TableExisted;
                }

                var designTable = new DesignTableEntity();
                designTable.DBName = "dbname";
                designTable.DBName = dbName;
                designTable.TableName = tableVo.TableName;
                designTable.Description = tableVo.Description;
                designTable.SchemaVersion = 0;
                designTable.DataStatus = 1;
                designTable.CreateUser = "zhang";
                designTable.CreateTime = DateTime.Now;
                designTable.LastUpdateUser = "zhang";
                designTable.LastUpdateTime = DateTime.Now;
                savoryDesignDB.DesignTable.Add(designTable);

                savoryDesignDB.SaveChanges();
            }

            return CreateTableResult.Success;
        }
    }
}
