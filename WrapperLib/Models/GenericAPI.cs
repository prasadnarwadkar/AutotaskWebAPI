using WrapperLib.Autotask.Net.Webservices;
using System;
using System.Collections.Generic;
using System.Text;

namespace WrapperLib.Models
{
    /// <summary>
    /// API for any entity (generic)
    /// </summary>
    public class GenericAPI : ApiBase
    {
        public List<Entity> GetEntityByListOfConditions(string entityName, Condition[] conditions, out string errorMsg)
        {
            List<Entity> list = new List<Entity>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            if (string.IsNullOrEmpty(entityName))
            {
                errorMsg = "Entity Name is empty.";
                return list;
            }

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append(string.Format("<entity>{0}</entity>", entityName));
            strResource.Append("<query>");
            
            if (conditions == null)
            {
                errorMsg = "Bad Request. Condition(s) are not sent.";
                return new List<Entity>(); 
            }

            foreach (Condition condition in conditions)
            {
                if (condition == null)
                {
                    errorMsg = "Bad Request. One of the conditions is null or not well-formed.";
                    return new List<Entity>();
                }

                switch (condition.ConditionType)
                {
                    case ConditionType.SimpleCondition:
                        if (condition.Fields == null)
                        {
                            errorMsg = "One of the conditions has null field(s).";
                            return new List<Entity>();
                        }

                        switch (condition.OperatorVal)
                        {
                            case LogicalOperator.AND:
                                strResource.Append("<condition>");
                                break;
                            case LogicalOperator.OR:
                                strResource.Append(string.Format("<condition operator=\"{0}\">", condition.OperatorVal));
                                break;
                            default:
                                errorMsg = "One of the conditions has invalid logical operator. Valid values are 1 (OR) and 2 (AND).";
                                return new List<Entity>();
                        }

                        foreach (SimpleField field in condition.Fields)
                        {
                            if (field == null)
                            {
                                errorMsg = "One of the conditions has at least one invalid field.";
                                return new List<Entity>();
                            }

                            if (!string.IsNullOrEmpty(field.FieldName)
                                && !string.IsNullOrEmpty(field.op))
                            {
                                strResource.Append(string.Format("<field>{0}<expression op=\"{1}\">", field.FieldName, field.op));
                                strResource.Append(field.ValueToUse);
                                strResource.Append("</expression></field>");
                            }
                            
                        }
                        strResource.Append("</condition>");
                        break;
                    case ConditionType.Field:
                        if (condition.Fields == null)
                        {
                            errorMsg = "One of the conditions has null field(s).";
                            continue;
                        }

                        if (condition.Fields.Length > 1)
                        {
                            errorMsg = "Only one field is allowed in condition type 'Field'.";
                            return new List<Entity>();
                        }
                        else
                        {
                            SimpleField field = condition.Fields[0];

                            if (field != null)
                            {
                                if (!string.IsNullOrEmpty(field.FieldName)
                                    && !string.IsNullOrEmpty(field.op))
                                {
                                    strResource.Append(string.Format("<field>{0}<expression op=\"{1}\">", field.FieldName, field.op));
                                    strResource.Append(field.ValueToUse);
                                    strResource.Append("</expression></field>");
                                }
                            }
                            else
                            {
                                errorMsg = "One of the conditions has invalid field.";
                                return new List<Entity>();
                            }
                        }
                        break;
                    case ConditionType.NestedConditions:
                        // nested conditions. allowed depth = 1.
                        switch (condition.OperatorVal)
                        {
                            case LogicalOperator.AND:
                                strResource.Append("<condition>");
                                break;
                            case LogicalOperator.OR:
                                strResource.Append(string.Format("<condition operator=\"{0}\">", condition.OperatorVal));
                                break;
                            default:
                                errorMsg = "One of the conditions has invalid logical operator. Valid values are 1 (OR) and 2 (AND).";
                                return new List<Entity>();
                        }

                        // add nested conditions.
                        if (condition.ChildConditions == null)
                        {
                            errorMsg = "Nested child conditions are invalid or not well-formed.";
                            return new List<Entity>();
                        }

                        foreach (Condition childCondition in condition.ChildConditions)
                        {
                            if (childCondition.Fields == null)
                            {
                                errorMsg = "One of the conditions has null field(s).";
                                return new List<Entity>();
                            }

                            switch (childCondition.OperatorVal)
                            {
                                case LogicalOperator.AND:
                                    strResource.Append("<condition>");
                                    break;
                                case LogicalOperator.OR:
                                    strResource.Append(string.Format("<condition operator=\"{0}\">", childCondition.OperatorVal));
                                    break;
                                default:
                                    errorMsg = "One of the conditions has invalid logical operator. Valid values are 1 (OR) and 2 (AND).";
                                    return new List<Entity>();
                            }

                            foreach (SimpleField field in childCondition.Fields)
                            {
                                if (field == null)
                                {
                                    errorMsg = "One of the conditions has at least one invalid field.";
                                    return new List<Entity>();
                                }

                                if (!string.IsNullOrEmpty(field.FieldName)
                                    && !string.IsNullOrEmpty(field.op))
                                {
                                    strResource.Append(string.Format("<field>{0}<expression op=\"{1}\">", field.FieldName, field.op));
                                    strResource.Append(field.ValueToUse);
                                    strResource.Append("</expression></field>");
                                }
                            }
                            strResource.Append("</condition>");
                        }
                        strResource.Append("</condition>");
                        break;
                    default:
                        errorMsg = "One of the conditions has invalid condition type or no condition type. Condition type is one of the following: Field = 1, SimpleCondition = 2, NestedConditions = 3";
                        return new List<Entity>();
                }
            }

            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = _atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add(entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        /// <summary>
        /// Get any entity by simple entity name and a field name
        /// and field value. e.g. Ticket entity, field id and value 12345.
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Entity> GetEntityByFieldEquals(string entityName, string fieldName,
                                                   string fieldValue, out string errorMsg)
        {
            List<Entity> list = new List<Entity>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append(string.Format("<entity>{0}</entity>", entityName));
            strResource.Append("<query>");
            strResource.Append(string.Format("<field>{0}<expression op=\"equals\">", fieldName));
            strResource.Append(fieldValue);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = _atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add(entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public Entity UpdateEntity(Entity entityToUpdate, out string errorMsg)
        {
            try
            {
                errorMsg = string.Empty;

                if (entityToUpdate == null)
                {
                    throw new ArgumentNullException("Entity to Update is null.");
                }

                if (entityToUpdate.id < 1)
                {
                    throw new ArgumentException("Id of the entity to update is invalid.");
                }

                if (errorMsg.Length == 0)
                {
                    Entity[] entityArray = new Entity[] { entityToUpdate };
                    ATWSResponse respUpdate = _atwsServices.update(entityArray, out errorMsg);

                    if (respUpdate.ReturnCode > 0 && respUpdate.EntityResults.Length > 0)
                    {
                        return respUpdate.EntityResults[0];
                    }
                    else if (respUpdate.Errors != null &&
                            respUpdate.Errors.Length > 0)
                    {
                        errorMsg = respUpdate.Errors[0].Message;

                        return null;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return null;
            }
        }

        public Entity CreateEntity(Entity entityToCreate, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (entityToCreate == null)
            {
                throw new ArgumentNullException("Entity to create is null.");
            }

            Entity[] entityArray = new Entity[] { entityToCreate };

            ATWSResponse response = _atwsServices.create(entityArray, out errorMsg);

            if (response.ReturnCode > 0 && response.EntityResults.Length > 0)
            {
                return response.EntityResults[0];
            }
            else
            {
                if (response != null && response.Errors != null
                    && response.Errors.Length > 0)
                {
                    errorMsg = response.Errors[0].Message;
                    return null;
                }
            }

            return null;
        }
    }
}