using System.Linq.Dynamic.Core;

namespace Demo.Common.Infrastructure;

public static class FilterParser
{
    public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, Dictionary<string, string> filters)
    {
        if (filters == null || filters.Count == 0)
            return query;

        var whereClauses = new List<string>();
        var parameters = new List<object>();

        int paramIndex = 0;

        foreach (var filter in filters)
        {
            var keyParts = filter.Key.Split('-');
            if (keyParts.Length != 2) continue;

            string columnName = keyParts[0];
            string operatorSymbol = keyParts[1];
            string value = filter.Value;

            string paramName = $"@{paramIndex}";

            switch (operatorSymbol)
            {
                case "eq": // Igualdade (ex: id-eq=5)
                    whereClauses.Add($"{columnName} == {paramName}");
                    parameters.Add(Convert.ChangeType(value, typeof(T).GetProperty(columnName)?.PropertyType ?? typeof(string)));
                    break;

                case "ne": // Diferente de (ex: id-ne=5)
                    whereClauses.Add($"{columnName} != {paramName}");
                    parameters.Add(Convert.ChangeType(value, typeof(T).GetProperty(columnName)?.PropertyType ?? typeof(string)));
                    break;

                case "gt": // Maior que (ex: price-gt=100)
                    whereClauses.Add($"{columnName} > {paramName}");
                    parameters.Add(Convert.ChangeType(value, typeof(T).GetProperty(columnName)?.PropertyType ?? typeof(string)));
                    break;

                case "lt": // Menor que (ex: price-lt=200)
                    whereClauses.Add($"{columnName} < {paramName}");
                    parameters.Add(Convert.ChangeType(value, typeof(T).GetProperty(columnName)?.PropertyType ?? typeof(string)));
                    break;

                case "ge": // Maior ou igual (ex: price-ge=150)
                    whereClauses.Add($"{columnName} >= {paramName}");
                    parameters.Add(Convert.ChangeType(value, typeof(T).GetProperty(columnName)?.PropertyType ?? typeof(string)));
                    break;

                case "le": // Menor ou igual (ex: price-le=500)
                    whereClauses.Add($"{columnName} <= {paramName}");
                    parameters.Add(Convert.ChangeType(value, typeof(T).GetProperty(columnName)?.PropertyType ?? typeof(string)));
                    break;

                case "like": // Contém (ex: title-like=Product)
                    whereClauses.Add($"{columnName}.Contains({paramName})");
                    parameters.Add(value);
                    break;

                case "startsWith": // Começa com (ex: title-startsWith=Test)
                    whereClauses.Add($"{columnName}.StartsWith({paramName})");
                    parameters.Add(value);
                    break;

                case "endsWith": // Termina com (ex: title-endsWith=Example)
                    whereClauses.Add($"{columnName}.EndsWith({paramName})");
                    parameters.Add(value);
                    break;

                case "isEmpty": // Verifica se está vazio (ex: description-isEmpty)
                    whereClauses.Add($"string.IsNullOrEmpty({columnName})");
                    break;

                case "isNotEmpty": // Verifica se não está vazio (ex: description-isNotEmpty)
                    whereClauses.Add($"!string.IsNullOrEmpty({columnName})");
                    break;

                case "null": // Verifica se é NULL (ex: category-null)
                    whereClauses.Add($"{columnName} == null");
                    break;

                case "notNull": // Verifica se NÃO é NULL (ex: category-notNull)
                    whereClauses.Add($"{columnName} != null");
                    break;

                default:
                    continue;
            }

            paramIndex++;
        }

        if (whereClauses.Any())
        {
            string whereExpression = string.Join(" AND ", whereClauses);
            return query.Where(whereExpression, parameters.ToArray());
        }

        return query;
    }
}
