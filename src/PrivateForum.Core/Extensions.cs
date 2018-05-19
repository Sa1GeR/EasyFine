using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PrivateForum.Core.Framework.Security;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Web;
using System.IO;
using PrivateForum.Core.Models;

namespace PrivateForum
{
    public static class Extensions
    {
        public static string AuditInsert(this IAuditContext auditContext)
        {
            return $", [Created], [Modified], [CreatedBy], [ModifiedBy]";
        }

        public static string AuditInsertValues(this IAuditContext auditContext)
        {
            return $", '{auditContext.ModificationDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{auditContext.ModificationDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{clearModifiedBy.Replace(auditContext.ModifiedBy, string.Empty)}', '{clearModifiedBy.Replace(auditContext.ModifiedBy, string.Empty)}'";
        }

        public static string AuditUpdateWithValues(this IAuditContext auditContext)
        {
            return $", [Modified] = '{auditContext.ModificationDate.ToString("yyyy-MM-dd HH:mm:ss.fff")}', [ModifiedBy] = '{clearModifiedBy.Replace(auditContext.ModifiedBy, string.Empty)}'";
        }

        public static DynamicParameters AuditInsert(this DynamicParameters parameters, IAuditContext auditContext)
        {
            parameters.Add("@Created", auditContext.ModificationDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            parameters.Add("@Modified", auditContext.ModificationDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            parameters.Add("@CreatedBy", clearModifiedBy.Replace(auditContext.ModifiedBy, string.Empty));
            parameters.Add("@ModifiedBy", clearModifiedBy.Replace(auditContext.ModifiedBy, string.Empty));
            return parameters;
        }

        public static DynamicParameters AuditUpdate(this DynamicParameters parameters, IAuditContext auditContext)
        {
            parameters.Add("@Modified", auditContext.ModificationDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            parameters.Add("@ModifiedBy", clearModifiedBy.Replace(auditContext.ModifiedBy, string.Empty));
            return parameters;
        }

        public static Regex clearModifiedBy = new Regex("[^\\w.]", RegexOptions.Compiled | RegexOptions.Singleline);

        public static SqlMapper.ICustomQueryParameter ToTvp<T>(this IEnumerable<T> enumerable, string typeName, params Expression<Func<T, object>>[] columnSelectors)
        {
            if (columnSelectors.Length == 0)
            {
                Expression<Func<T, object>> getSelf = x => x;
                columnSelectors = new[] { getSelf };
            }
            var result = new DataTable();
            foreach (var selector in columnSelectors)
            {
                if(selector.Body is ParameterExpression)
                {
                    //var member = (((ParameterExpression)selector.Body). as MemberExpression).Member;

                    //result.Columns.Add(member.Name, (member as System.Reflection.PropertyInfo).PropertyType);
                    result.Columns.Add();
                }
                else if(selector.Body is UnaryExpression)
                {
                    var operand = ((UnaryExpression)selector.Body).Operand;

                    if(operand is MemberExpression)
                    {
                        var member = (operand as MemberExpression).Member;

                        var propertyType = (member as System.Reflection.PropertyInfo).PropertyType;
                        result.Columns.Add(member.Name, Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                    }
                    else
                    {
                        result.Columns.Add();
                    }
                }
                else if (selector.Body is MemberExpression)
                {
                    var member = ((MemberExpression)selector.Body).Member;
                    var propertyType = (member as System.Reflection.PropertyInfo).PropertyType;
                    result.Columns.Add(member.Name, Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                }
                else
                {
                    result.Columns.Add();
                }
            }
            foreach (var item in enumerable)
            {
                var colValues = columnSelectors.Select(selector => selector.Compile()(item)).ToArray();
                result.Rows.Add(colValues);
            }
            return result.AsTableValuedParameter(typeName);
        }

        public static List<FileStreamModel> ToFileStreamModel(this List<HttpPostedFileBase> files)
        {
            var result = new List<FileStreamModel>();

            foreach (var file in files)
            {
                result.Add(new FileStreamModel
                {
                    FileName = file.FileName.Replace(" ", "_"),
                    InputStream = file.InputStream
                });
            }

            return result;
        }
    }
}
