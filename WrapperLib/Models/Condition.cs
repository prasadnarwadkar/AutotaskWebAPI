using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WrapperLib.Models
{
    /// <summary>
    /// Operator. OR, AND
    /// </summary>
    public enum LogicalOperator
    {
        OR = 1,
        AND = 2
    }

    /// <summary>
    /// Condition type:
    /// 1 = Field // Array of nested Conditions is null. Only a field is present.
    /// 2 = SimpleCondition // Array of nested conditions is null.
    /// Array of fields is not null.
    /// 3=  NestedConditions // nested conditions. Array of fields is null.
    /// </summary>
    public enum ConditionType
    {
        Field = 1,
        SimpleCondition = 2,
        NestedConditions = 3
    }

    /// <summary>
    /// Field operator.
    /// e.g. <expression op="equals"> {value}</expression>
    /// </summary>
    public enum FieldOperator
    {
        Equals = 1,
        NotEqual = 2,
        GreaterThan = 3,
        LessThan = 4,
        GreaterThanorEquals = 5,
        LessThanOrEquals = 6,
        BeginsWith = 7,
        EndsWith = 8,
        Contains = 9,
        IsNotNull = 10,
        IsNull = 11,
        IsThisDay = 12, 
        Like = 13,
        NotLike = 14,
        SoundsLike= 15
    }

    /// <summary>
    /// Describes simple field.
    /// e.g.
    /// <field>firstname
    /// <expression op="equals"> Larry</expression>
    /// </field>
    /// </summary>
    public class SimpleField
    {
        public string FieldName { get; set; }
        public string op { get; set; }
        public string ValueToUse { get; set; }
    }

    /// <summary>
    /// Describes a condition using an optional logical operator in a simple or
    /// complex query.
    /// e.g. 
    /// <condition operator="OR">
    /// <field>lastname
    /// <expression op="equals"> Brown</expression>
    /// </field>
    /// </condition>
    /// </summary>
    public class Condition
    {
        public ConditionType ConditionType { get; set; }
        public LogicalOperator OperatorVal { get; set; }
        public SimpleField[] Fields { get; set; }
        public Condition[] ChildConditions { get; set; }
    }

    public class ComplexQuery
    {
        public string EntityName { get; set; }
        public Condition[] Conditions { get; set; }
    }
}