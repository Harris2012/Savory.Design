using Savory.Repository;
using Savory.Repository.DesignDB.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.Design.Repository
{
    public class SavoryDesignDBContext : DbContext
    {
        public SavoryDesignDBContext() : base(ConnStringProvider.GetConnString(DBNameConst.SavoryDesignDB))
        {
        }

        /// <summary>
        /// 数据库
        /// </summary>
        public virtual DbSet<DesignDatabaseEntity> DesignDatabase { get; set; }

        /// <summary>
        /// 表
        /// </summary>
        public virtual DbSet<DesignTableEntity> DesignTable { get; set; }

        /// <summary>
        /// 字段
        /// </summary>
        public virtual DbSet<TableSchemaEntity> TableSchema { get; set; }
    }
}
