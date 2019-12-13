using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IRepository;
using Model;
using SqlSugar;
using Repository.SQLSugarDB;
using Common;
using System.Data;

namespace Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private DBContext context;
        private SqlSugarClient db;
        private SimpleClient<TEntity> entityDB;

        public BaseRepository()
        {
            DBContext.Init(Consts.ConnectionStr);
            context = DBContext.GetDbContext();
            db = context.Db;
            entityDB = context.GetEntityDB<TEntity>(db);
        }

        /// <summary>
        /// 批量操作实体信息
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="batchSave"></param>
        /// <returns></returns>
        public async Task<ReturnModel> BatchSaveEntityInfo(List<TEntity> entities, BatchOption batchSave = BatchOption.BatchAdd)
        {
            ReturnModel rm = new ReturnModel();
            try
            {
                switch (batchSave)
                {
                    case BatchOption.BatchAdd:
                        db.Insertable(entities.ToArray()).ExecuteCommand();
                        rm.msg = "批量保存成功!";
                        break;
                    case BatchOption.BatchUpdate:
                        db.Updateable(entities).ExecuteCommand();
                        rm.msg = "批量更新成功!";
                        break;
                    case BatchOption.BatchLogicDelete:
                        db.Updateable(entities).ExecuteCommand();
                        rm.msg = "批量删除成功!";
                        break;
                    case BatchOption.BatchPhysicsDelete:
                        db.Deleteable<TEntity>().In(entities).ExecuteCommand();
                        rm.msg = "批量删除成功!";
                        break;
                }
                rm.Result = CommonSignal.Success;
            }
            catch (Exception ex)
            {
                rm.Result = CommonSignal.Fail;
                rm.msg = ex.Message;
            }
            return await Task.Run(() => rm);
        }

        /// <summary>
        /// 根据 主键 物理删除实体
        /// </summary>
        /// <param name="Id">主键ID</param>
        /// <returns></returns>
        public async Task<ReturnModel> DeleteTEntityById(object Id)
        {
            ReturnModel rm = new ReturnModel();
            try
            {
                bool isSuccess = entityDB.DeleteById(Id);
                rm.Result = CommonSignal.Success;
                rm.msg = "删除成功!";
            }
            catch (Exception ex)
            {
                rm.Result = CommonSignal.Fail;
                rm.msg = ex.Message;
            }
            return await Task.Run(() => rm);
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="option">SQL语句操作类型</param>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        public async Task<object> ExecSqlText(string sqlStr, Option option, params SugarParameter[] paras)
        {
            object result = new object();
            switch (option)
            {
                case Option.Select:
                default:
                    result = db.Ado.GetDataTable(sqlStr, paras);
                    break;
                case Option.Other:
                    result = db.Ado.GetInt(sqlStr, paras);
                    break;
            }

            return await Task.Run(() => result);
        }

        /// <summary>
        /// 执行查询存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="pm"></param>
        /// <returns></returns>
        public async Task<DataTable> ExecStoredProcedure(string procName, Model.PageModel pm)
        {
            SugarParameter[] sugarParameters = new SugarParameter[] {
                new SugarParameter("p_CurrentPage", pm.CurrentPage),
                new SugarParameter("p_PageSize", pm.PageSize),
                new SugarParameter("p_Condition", pm.ConditionString),
                new SugarParameter("p_Order", pm.OrderString),
                new SugarParameter("p_DataCount", pm.DataCount),
                new SugarParameter("p_MaxPage", pm.MaxPage),
            };
            sugarParameters[4].Direction = ParameterDirection.Output;
            sugarParameters[5].Direction = ParameterDirection.Output;
            DataTable dt = db.Ado.UseStoredProcedure().GetDataTable(procName, sugarParameters);
            pm.DataCount = Convert.IsDBNull(sugarParameters[4].Value) ? 0 : Convert.ToInt32(sugarParameters[4].Value);
            pm.MaxPage = Convert.IsDBNull(sugarParameters[5].Value) ? 0 : Convert.ToInt32(sugarParameters[5].Value);
            return await Task.Run(() => dt);
        }

        /// <summary>
        /// 执行查询特殊的存储过程（链表查询）
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="pm"></param>
        /// <returns></returns>
        public async Task<DataTable> ExecStoredProcedureTwo(string procName, Model.PageModel pm)
        {
            SugarParameter[] sugarParameters = new SugarParameter[] {
                new SugarParameter("p_CurrentPage", pm.CurrentPage),
                new SugarParameter("p_PageSize", pm.PageSize),
                new SugarParameter("p_Condition", pm.ConditionString),
                new SugarParameter("p_Order", pm.OrderString),
                new SugarParameter("p_DataCount", pm.DataCount),
                new SugarParameter("p_MaxPage", pm.MaxPage),
                new SugarParameter("p_ExtraCondition",pm.ExtraCondition),
            };
            sugarParameters[4].Direction = ParameterDirection.Output;
            sugarParameters[5].Direction = ParameterDirection.Output;
            DataTable dt = db.Ado.UseStoredProcedure().GetDataTable(procName, sugarParameters);
            pm.DataCount = Convert.IsDBNull(sugarParameters[4].Value) ? 0 : Convert.ToInt32(sugarParameters[4].Value);
            pm.MaxPage = Convert.IsDBNull(sugarParameters[5].Value) ? 0 : Convert.ToInt32(sugarParameters[5].Value);
            return await Task.Run(() => dt);
        }

        /// <summary>
        /// 获取单个实体列表（不分页）
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> GetEntity()
        {
            return await Task.Run(() => entityDB.GetList());
        }

        /// <summary>
        /// 获取单个实体列表（分页）
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetEntityPage(Model.PageModel pm)
        {
            SugarParameter[] sugarParameters = new SugarParameter[] {
                new SugarParameter("p_CurrentPage", pm.CurrentPage),
                new SugarParameter("p_PageSize", pm.PageSize),
                new SugarParameter("p_Condition", pm.ConditionString),
                new SugarParameter("p_Order", pm.OrderString),
                new SugarParameter("p_DataCount", pm.DataCount),
                new SugarParameter("p_MaxPage", pm.MaxPage),
                new SugarParameter("p_TableName", pm.TableName),
                new SugarParameter("p_KeyField", pm.KeyField),
            };
            sugarParameters[4].Direction = ParameterDirection.Output;
            sugarParameters[5].Direction = ParameterDirection.Output;
            List<TEntity> list = db.Ado.SqlQuery<TEntity>("exec sp_GetTablePageData @p_CurrentPage,@p_PageSize," +
                "@p_Condition,@p_Order,@p_DataCount output,@p_MaxPage output,@p_TableName,@p_KeyField", sugarParameters);
            pm.DataCount = Convert.IsDBNull(sugarParameters[4].Value) ? 0 : Convert.ToInt32(sugarParameters[4].Value);
            pm.MaxPage = Convert.IsDBNull(sugarParameters[5].Value) ? 0 : Convert.ToInt32(sugarParameters[5].Value);
            return await Task.Run(() => list);
        }

        /// <summary>
        /// 保存、添加、逻辑删除单个实体数据
        /// </summary>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public async Task<ReturnModel> SaveEntityInfo(TEntity entity)
        {
            ReturnModel rm = new ReturnModel();
            try
            {
                db.Saveable<TEntity>(entity).ExecuteReturnEntity();//会根据主键判断实体是否存在，存在则更新，反之新增
                rm.Result = CommonSignal.Success;
                rm.msg = "保存成功!";
            }
            catch (Exception ex)
            {
                rm.Result = CommonSignal.Fail;
                rm.msg = ex.Message;
            }
            return await Task.Run(() => rm);
        }

        /// <summary>
        /// 获取实体详情
        /// </summary>
        /// <param name="Id">主键ID</param>
        /// <returns></returns>
        public async Task<TEntity> TEntityInfo(string Id)
        {
            TEntity entity = db.Queryable<TEntity>().InSingle(Id);
            return await Task.Run(() => entity ?? new TEntity());
        }
    }
}
