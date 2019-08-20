using System;
using System.Collections.Generic;

namespace DataAcess.Mapper
{
    public abstract class EntityMapper
    {      
        protected string GetStringValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is string)
                return (string) val;

            return "";
        }

        protected int GetIntValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && (val is int || val is decimal))
                return (int) dic[attName];

            return -1;
        }

        protected decimal GetDoubleValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is decimal)
                return (decimal)dic[attName];

            return -1;
        }

        protected decimal GetDecimalValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is decimal)
                return (decimal)dic[attName];

            return -1;
        }

        protected double GetRealValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is Single)
                return (Single)dic[attName];

            return -1;
        }

        protected DateTime GetDateValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && val is DateTime)
                return (DateTime)dic[attName];

            return DateTime.Now;
        }
        
    }
}