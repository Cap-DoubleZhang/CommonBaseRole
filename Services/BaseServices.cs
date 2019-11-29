using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using IServices;
using Model;
using SqlSugar;

namespace Services
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        public Task<ReturnModel> BatchSaveEntityInfo(List<TEntity> entities, BatchSave batchSave = BatchSave.BatchAdd)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnModel> DeleteTEntityById(object Id)
        {
            throw new NotImplementedException();
        }

        public Task<object> ExecSqlText(string sqlStr, Option option, params SugarParameter[] paras)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> ExecStoredProcedure(string procName, Model.PageModel pm)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetEntity()
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetEntityPage(Model.PageModel pm)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnModel> SaveEntityInfo(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
