using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PageModel
    {
        public PageModel()
        {
            Condition = new List<SearchCondition>();
            OrCondition = new List<SearchCondition>();
            lstOrder = new List<OrderModel>();
            ExtraCondition = "";
            ExtraCondition2 = "";
            CurrentPage = 1;
        }
        /// <summary>
        /// 传入id
        /// </summary>
        public int proid { get; set; }
        /// <summary>
        /// 当前页号
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 记录数
        /// </summary>
        public int DataCount { get; set; }

        /// <summary>
        /// 页面数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 最大页面数
        /// </summary>
        public int MaxPage { get; set; }

        /// <summary>
        /// 与条件对象
        /// </summary>
        public List<SearchCondition> Condition { get; set; }

        /// <summary>
        /// 或条件对象
        /// </summary>
        public List<SearchCondition> OrCondition { get; set; }

        public string TableName { get; set; }

        public string KeyField { get; set; }

        /// <summary>
        /// 条件对象
        /// </summary>
        public string ConditionString
        {
            get
            {
                return AndConditionString + OrConditionString;
            }
        }

        /// <summary>
        /// 条件对象
        /// </summary>
        public string AndConditionString
        {
            get
            {
                StringBuilder condition = new StringBuilder();
                foreach (SearchCondition sc in Condition)
                {
                    switch (sc.SearchType)
                    {
                        case SearchType.NotEqual:
                            condition.AppendFormat(" and {0} <> '{1}'", sc.ConditionField, sc.ConditionValue1);
                            break;

                        case SearchType.Equal:
                            condition.AppendFormat(" and {0} = '{1}'", sc.ConditionField, sc.ConditionValue1);
                            break;

                        case SearchType.GreaterThan:
                            condition.AppendFormat(" and {0} > {1}", sc.ConditionField, sc.ConditionValue1);
                            break;

                        case SearchType.LessThan:
                            condition.AppendFormat(" and {0} < {1}", sc.ConditionField, sc.ConditionValue1);
                            break;

                        case SearchType.Like:
                            condition.AppendFormat(" and {0} like '%{1}%'", sc.ConditionField, sc.ConditionValue1);
                            break;

                        case SearchType.Between:
                            condition.AppendFormat(" and {0} between {1} and {2} ", sc.ConditionField, sc.ConditionValue1, sc.ConditionValue2);
                            break;

                        case SearchType.GreaterThanOrEqual:
                            condition.AppendFormat(" and {0} >= {1}", sc.ConditionField, sc.ConditionValue1);
                            break;

                        case SearchType.LessThanOrEqual:
                            condition.AppendFormat(" and {0} <= {1}", sc.ConditionField, sc.ConditionValue1);
                            break;

                        case SearchType.In:
                            condition.AppendFormat(" and {0} in ({1})", sc.ConditionField, sc.ConditionValue1);
                            break;

                        case SearchType.NotIn:
                            condition.AppendFormat(" and {0} not in ({1}) ", sc.ConditionField, sc.ConditionValue1);
                            break;

                        case SearchType.IsNull:
                            condition.AppendFormat(" and ({0} is null) ", sc.ConditionField);
                            break;

                        case SearchType.IsNotNull:
                            condition.AppendFormat(" and ({0} is not null) ", sc.ConditionField);
                            break;

                        case SearchType.DateGreaterThan:
                            condition.AppendFormat(" and {0} > {1} ", sc.ConditionField,
                                FormOracleDateStr(Convert.ToDateTime(sc.ConditionValue1)));
                            break;

                        case SearchType.DateLessThan:
                            condition.AppendFormat(" and {0} < {1} ", sc.ConditionField,
                                FormOracleDateStr(Convert.ToDateTime(sc.ConditionValue1)));
                            break;

                        case SearchType.DateBetween:
                            condition.AppendFormat(" and {0} between {1} and {2} ", sc.ConditionField,
                                FormOracleDateStr(Convert.ToDateTime(sc.ConditionValue1)),
                                FormOracleDateStr(Convert.ToDateTime(sc.ConditionValue2)));
                            break;

                        case SearchType.DateGreaterThanOrEqual:
                            condition.AppendFormat(" and {0} >= {1} ", sc.ConditionField,
                                FormOracleDateStr(Convert.ToDateTime(sc.ConditionValue1)));
                            break;

                        case SearchType.DateLessThanOrEqual:
                            condition.AppendFormat(" and {0} <= {1} ", sc.ConditionField,
                                FormOracleDateStr(Convert.ToDateTime(sc.ConditionValue1)));
                            break;

                        case SearchType.Exists:
                            condition.AppendFormat(" and exists({0}) ", sc.ConditionValue1);
                            break;

                        case SearchType.NotExists:
                            condition.AppendFormat(" and not exists({0}) ", sc.ConditionValue1);
                            break;

                        case SearchType.SQLServerDateGreaterThan:
                            condition.AppendFormat(" and {0} > '{1}' ", sc.ConditionField, sc.ConditionValue1);
                            break;

                        case SearchType.SQLServerDateLessThan:
                            condition.AppendFormat(" and {0} < '{1}' ", sc.ConditionField, sc.ConditionValue1);
                            break;

                        case SearchType.SQLServerDateBetween:
                            condition.AppendFormat(" and {0} between '{1}' and '{2}' ", sc.ConditionField,
                                sc.ConditionValue1, sc.ConditionValue2);
                            break;

                        case SearchType.SQLServerDateGreaterThanOrEqual:
                            condition.AppendFormat(" and {0} >= '{1}' ", sc.ConditionField, sc.ConditionValue1);
                            break;

                        case SearchType.SQLServerDateLessThanOrEqual:
                            condition.AppendFormat(" and {0} <= '{1}' ", sc.ConditionField, sc.ConditionValue1);
                            break;

                        default:
                            break;
                    }
                }

                condition.AppendFormat("{0}", ExtraCondition);

                return condition.ToString();
            }
        }

        /// <summary>
        /// 条件对象
        /// </summary>
        public string OrConditionString
        {
            get
            {
                StringBuilder condition = new StringBuilder();
                condition.AppendFormat(" and (0 > 1");
                foreach (SearchCondition sc in OrCondition)
                {
                    if (sc.SubConditions.Count == 0)
                    {
                        switch (sc.SearchType)
                        {
                            case SearchType.NotEqual:
                                condition.AppendFormat(" or {0} <> '{1}'", sc.ConditionField, sc.ConditionValue1);
                                break;

                            case SearchType.Equal:
                                condition.AppendFormat(" or {0} = '{1}'", sc.ConditionField, sc.ConditionValue1);
                                break;

                            case SearchType.GreaterThan:
                                condition.AppendFormat(" or {0} > {1}", sc.ConditionField, sc.ConditionValue1);
                                break;

                            case SearchType.LessThan:
                                condition.AppendFormat(" or {0} < {1}", sc.ConditionField, sc.ConditionValue1);
                                break;

                            case SearchType.Like:
                                condition.AppendFormat(" or {0} like '%{1}%'", sc.ConditionField, sc.ConditionValue1);
                                break;

                            case SearchType.Between:
                                condition.AppendFormat(" or {0} between {1} and {2} ", sc.ConditionField, sc.ConditionValue1, sc.ConditionValue2);
                                break;

                            case SearchType.GreaterThanOrEqual:
                                condition.AppendFormat(" or {0} >= {1}", sc.ConditionField, sc.ConditionValue1);
                                break;

                            case SearchType.LessThanOrEqual:
                                condition.AppendFormat(" or {0} <= {1}", sc.ConditionField, sc.ConditionValue1);
                                break;

                            case SearchType.In:
                                condition.AppendFormat(" or {0} in ({1})", sc.ConditionField, sc.ConditionValue1);
                                break;

                            case SearchType.NotIn:
                                condition.AppendFormat(" or {0} not in ({1}) ", sc.ConditionField, sc.ConditionValue1);
                                break;

                            case SearchType.IsNull:
                                condition.AppendFormat(" or ({0} is null) ", sc.ConditionField);
                                break;

                            case SearchType.IsNotNull:
                                condition.AppendFormat(" or ({0} is not null) ", sc.ConditionField);
                                break;

                            case SearchType.DateGreaterThan:
                                condition.AppendFormat(" or {0} > {1} ", sc.ConditionField,
                                    FormOracleDateStr(Convert.ToDateTime(sc.ConditionValue1)));
                                break;

                            case SearchType.DateLessThan:
                                condition.AppendFormat(" or {0} < {1} ", sc.ConditionField,
                                    FormOracleDateStr(Convert.ToDateTime(sc.ConditionValue1)));
                                break;

                            case SearchType.DateBetween:
                                condition.AppendFormat(" or {0} between {1} and {2} ", sc.ConditionField,
                                    FormOracleDateStr(Convert.ToDateTime(sc.ConditionValue1)),
                                    FormOracleDateStr(Convert.ToDateTime(sc.ConditionValue2)));
                                break;

                            case SearchType.DateGreaterThanOrEqual:
                                condition.AppendFormat(" or {0} >= {1} ", sc.ConditionField,
                                    FormOracleDateStr(Convert.ToDateTime(sc.ConditionValue1)));
                                break;

                            case SearchType.DateLessThanOrEqual:
                                condition.AppendFormat(" or {0} <= {1} ", sc.ConditionField,
                                    FormOracleDateStr(Convert.ToDateTime(sc.ConditionValue1)));
                                break;

                            case SearchType.SQLServerDateGreaterThan:
                                condition.AppendFormat(" and {0} > '{1}' ", sc.ConditionField, sc.ConditionValue1);
                                break;

                            case SearchType.SQLServerDateLessThan:
                                condition.AppendFormat(" and {0} < '{1}' ", sc.ConditionField, sc.ConditionValue1);
                                break;

                            case SearchType.SQLServerDateBetween:
                                condition.AppendFormat(" and {0} between '{1}' and '{2}' ", sc.ConditionField,
                                    sc.ConditionValue1, sc.ConditionValue2);
                                break;

                            case SearchType.SQLServerDateGreaterThanOrEqual:
                                condition.AppendFormat(" and {0} >= '{1}' ", sc.ConditionField, sc.ConditionValue1);
                                break;

                            case SearchType.SQLServerDateLessThanOrEqual:
                                condition.AppendFormat(" and {0} <= '{1}' ", sc.ConditionField, sc.ConditionValue1);
                                break;

                            case SearchType.Exists:
                                condition.AppendFormat(" or exists({0}) ", sc.ConditionValue1);
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        condition.Append(" or (1 > 0");
                        foreach (SubSearchCondition bc in sc.SubConditions)
                        {
                            switch (bc.SearchType)
                            {
                                case SearchType.NotEqual:
                                    condition.AppendFormat(" and {0} <> '{1}'", bc.ConditionField, bc.ConditionValue1);
                                    break;

                                case SearchType.Equal:
                                    condition.AppendFormat(" and {0} = '{1}'", bc.ConditionField, bc.ConditionValue1);
                                    break;

                                case SearchType.GreaterThan:
                                    condition.AppendFormat(" and {0} > {1}", bc.ConditionField, bc.ConditionValue1);
                                    break;

                                case SearchType.LessThan:
                                    condition.AppendFormat(" and {0} < {1}", bc.ConditionField, bc.ConditionValue1);
                                    break;

                                case SearchType.Like:
                                    condition.AppendFormat(" and {0} like '%{1}%'", bc.ConditionField, bc.ConditionValue1);
                                    break;

                                case SearchType.Between:
                                    condition.AppendFormat(" and {0} between {1} and {2} ", bc.ConditionField, bc.ConditionValue1, sc.ConditionValue2);
                                    break;

                                case SearchType.GreaterThanOrEqual:
                                    condition.AppendFormat(" and {0} >= {1}", bc.ConditionField, bc.ConditionValue1);
                                    break;

                                case SearchType.LessThanOrEqual:
                                    condition.AppendFormat(" and {0} <= {1}", bc.ConditionField, bc.ConditionValue1);
                                    break;

                                case SearchType.In:
                                    condition.AppendFormat(" and {0} in ({1})", bc.ConditionField, bc.ConditionValue1);
                                    break;

                                case SearchType.NotIn:
                                    condition.AppendFormat(" and {0} not in ({1}) ", bc.ConditionField, bc.ConditionValue1);
                                    break;

                                case SearchType.IsNull:
                                    condition.AppendFormat(" and ({0} is null) ", bc.ConditionField);
                                    break;

                                case SearchType.IsNotNull:
                                    condition.AppendFormat(" and ({0} is not null) ", bc.ConditionField);
                                    break;

                                case SearchType.DateGreaterThan:
                                    condition.AppendFormat(" and {0} > {1} ", bc.ConditionField,
                                        FormOracleDateStr(Convert.ToDateTime(bc.ConditionValue1)));
                                    break;

                                case SearchType.DateLessThan:
                                    condition.AppendFormat(" and {0} < {1} ", bc.ConditionField,
                                        FormOracleDateStr(Convert.ToDateTime(bc.ConditionValue1)));
                                    break;

                                case SearchType.DateBetween:
                                    condition.AppendFormat(" and {0} between {1} and {2} ", bc.ConditionField,
                                        FormOracleDateStr(Convert.ToDateTime(bc.ConditionValue1)),
                                        FormOracleDateStr(Convert.ToDateTime(bc.ConditionValue2)));
                                    break;

                                case SearchType.DateGreaterThanOrEqual:
                                    condition.AppendFormat(" and {0} >= {1} ", bc.ConditionField,
                                        FormOracleDateStr(Convert.ToDateTime(bc.ConditionValue1)));
                                    break;

                                case SearchType.DateLessThanOrEqual:
                                    condition.AppendFormat(" and {0} <= {1} ", bc.ConditionField,
                                        FormOracleDateStr(Convert.ToDateTime(bc.ConditionValue1)));
                                    break;

                                case SearchType.Exists:
                                    condition.AppendFormat(" and exists({0}) ", bc.ConditionValue1);
                                    break;

                                case SearchType.NotExists:
                                    condition.AppendFormat(" and not exists({0}) ", bc.ConditionValue1);
                                    break;

                                case SearchType.SQLServerDateGreaterThan:
                                    condition.AppendFormat(" and {0} > '{1}' ", bc.ConditionField, bc.ConditionValue1);
                                    break;

                                case SearchType.SQLServerDateLessThan:
                                    condition.AppendFormat(" and {0} < '{1}' ", bc.ConditionField, bc.ConditionValue1);
                                    break;

                                case SearchType.SQLServerDateBetween:
                                    condition.AppendFormat(" and {0} between '{1}' and '{2}' ", bc.ConditionField,
                                        sc.ConditionValue1, sc.ConditionValue2);
                                    break;

                                case SearchType.SQLServerDateGreaterThanOrEqual:
                                    condition.AppendFormat(" and {0} >= '{1}' ", bc.ConditionField, bc.ConditionValue1);
                                    break;

                                case SearchType.SQLServerDateLessThanOrEqual:
                                    condition.AppendFormat(" and {0} <= '{1}' ", bc.ConditionField, bc.ConditionValue1);
                                    break;

                                default:
                                    break;
                            }
                        }
                        condition.Append(")");
                    }
                }

                condition.AppendFormat(")");

                condition = OrCondition.Count > 0 ? condition : new StringBuilder();

                return condition.ToString();
            }
        }

        public string FormOracleDateStr(DateTime d)
        {
            return string.Format("to_date('{0}','yyyy/mm/dd hh24:mi:ss')", d.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        /// <summary>
        /// 结果排序
        /// </summary>
        public List<OrderModel> lstOrder { get; set; }

        /// <summary>
        /// 如果有特殊的查询条件的话那就直接写在这里
        /// </summary>
        public string ExtraCondition { get; set; }

        /// <summary>
        /// 额外条件2
        /// </summary>
        public string ExtraCondition2 { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderString
        {
            get
            {
                string orderstr = " ";

                foreach (OrderModel o in lstOrder)
                {
                    orderstr += string.Format("{0} {1},", o.FieldName, o.Order.ToString());
                }

                return orderstr;
            }
        }
    }

    public class SearchCondition
    {
        public SearchCondition()
        {
            SubConditions = new List<SubSearchCondition>();
        }

        /// <summary>
        /// 条件字段
        /// </summary>
        public string ConditionField { get; set; }

        /// <summary>
        /// 值1
        /// </summary>
        public string ConditionValue1 { get; set; }

        /// <summary>
        /// 值2
        /// </summary>
        public string ConditionValue2 { get; set; }

        /// <summary>
        /// 参数类型；0-整形；1-字符串；2-日期；
        /// </summary>
        public int ValueType { get; set; }

        /// <summary>
        /// 查询类型，1-等于；0-不等于；2-大于；3-小于；4-like；5-between；6-大于等于；7-小于等于； 8-in；9-not in,11-is null,12-is not null;
        /// </summary>
        public SearchType SearchType { get; set; }

        public List<SubSearchCondition> SubConditions { get; set; }
    }

    public class SubSearchCondition
    {
        /// <summary>
        /// 条件字段
        /// </summary>
        public string ConditionField { get; set; }

        /// <summary>
        /// 值1
        /// </summary>
        public string ConditionValue1 { get; set; }

        /// <summary>
        /// 值2
        /// </summary>
        public string ConditionValue2 { get; set; }

        /// <summary>
        /// 参数类型；0-整形；1-字符串；2-日期；
        /// </summary>
        public int ValueType { get; set; }

        /// <summary>
        /// 查询类型，1-等于；0-不等于；2-大于；3-小于；4-like；5-between；6-大于等于；7-小于等于； 8-in；9-not in,11-is null,12-is not null;
        /// </summary>
        public SearchType SearchType { get; set; }
    }

    public enum SearchType
    {
        Equal = 1,
        NotEqual = 0,
        GreaterThan = 2,
        LessThan = 3,
        Like = 4,
        Between = 5,
        GreaterThanOrEqual = 6,
        LessThanOrEqual = 7,
        In = 8,
        NotIn = 9,
        IsNull = 11,
        IsNotNull = 12,
        DateGreaterThan = 13,
        DateLessThan = 14,
        DateBetween = 15,
        DateGreaterThanOrEqual = 16,
        DateLessThanOrEqual = 17,
        Exists = 18,
        NotExists = 19,
        SQLServerDateGreaterThan = 20,
        SQLServerDateLessThan = 21,
        SQLServerDateBetween = 22,
        SQLServerDateGreaterThanOrEqual = 23,
        SQLServerDateLessThanOrEqual = 24,
    }


    public class ReturnModel
    {
        public ReturnModel()
        {
            Result = CommonSignal.Success;
            msg = "";
            EtcInfo = "";
        }

        public CommonSignal Result { get; set; }

        public string ResultString
        {
            get
            {
                string ret = "";
                switch (Result)
                {
                    case CommonSignal.Fail:
                    case CommonSignal.Exists:
                    case CommonSignal.Expired:
                        ret = "false";
                        break;

                    case CommonSignal.Success:
                    default:
                        ret = "true";
                        break;
                }

                return ret;
            }
        }

        public bool BooleanResult
        {
            get
            {
                bool ret = false;
                switch (Result)
                {
                    case CommonSignal.Fail:
                    case CommonSignal.Exists:
                    case CommonSignal.Expired:
                        ret = false;
                        break;

                    case CommonSignal.Success:
                    default:
                        ret = true;
                        break;
                }

                return ret;
            }
        }

        public string msg { get; set; }

        public string EtcInfo { get; set; }

        public long code { get; set; }
    }

    public enum CommonSignal
    {
        Success, Fail, Exists, Expired
    }

    public class OrderModel
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 升序或降序
        /// </summary>
        public PMSortOrder Order { get; set; }
    }

    public enum PMSortOrder
    {
        asc, desc
    }

    /// <summary>
    /// 批量保存操作
    /// </summary>
    public enum BatchSave
    {
        BatchAdd = 0,//批量添加
        BatchUpdate = 1,//批量更新
        BatchLogicDelete = 2,//批量逻辑删除
        BatchPhysicsDelete = 3,//批量物理删除
    }

    /// <summary>
    /// 数据库操作
    /// </summary>
    public enum Option
    {
        Select = 0,//查询
        Other = 1,//除查询之外，其他操作（增、删、改）
    }
}

