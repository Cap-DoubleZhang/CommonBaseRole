using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using IServices;
using Model;
using SqlSugar;
using IRepository;

namespace Services
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        public IBaseRepository<TEntity> baseRepository;

        /// <summary>
        /// 批量操作数据实体
        /// </summary>
        /// <param name="entities">数据实体</param>
        /// <param name="batchSave">操作类型</param>
        /// <returns></returns>
        public async Task<ReturnModel> BatchSaveEntityInfo(List<TEntity> entities, BatchOption batchSave = BatchOption.BatchAdd)
        {
            return await baseRepository.BatchSaveEntityInfo(entities, batchSave);
        }

        /// <summary>
        /// 根据 主键ID 删除实体数据
        /// </summary>
        /// <param name="Id">主键ID</param>
        /// <returns></returns>
        public async Task<ReturnModel> DeleteTEntityById(object Id)
        {
            return await baseRepository.DeleteTEntityById(Id);
        }

        /// <summary>
        /// 获取实体详情
        /// </summary>
        /// <param name="Id">主键ID</param>
        /// <returns></returns>
        public async Task<TEntity> TEntityInfo(string Id)
        {
            return await baseRepository.TEntityInfo(Id);
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="option">操作类型</param>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        public async Task<object> ExecSqlText(string sqlStr, Option option, params SugarParameter[] paras)
        {
            return await baseRepository.ExecSqlText(sqlStr, option, paras);
        }

        /// <summary>
        /// 执行查询存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="pm">参数</param>
        /// <returns></returns>
        public async Task<DataTable> ExecStoredProcedure(string procName, Model.PageModel pm)
        {
            return await baseRepository.ExecStoredProcedure(procName, pm);
        }

        /// <summary>
        /// 获取实体列表（不分页）
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> GetEntity()
        {
            return await baseRepository.GetEntity();
        }

        /// <summary>
        /// 获取实体列表（分页），本质为执行存储过程
        /// </summary>
        /// <param name="pm">参数</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetEntityPage(Model.PageModel pm)
        {
            return await baseRepository.GetEntityPage(pm);
        }

        /// <summary>
        /// 保存、更改、逻辑删除实体数据
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public async Task<ReturnModel> SaveEntityInfo(TEntity entity)
        {
            return await baseRepository.SaveEntityInfo(entity);
        }
    }
}
