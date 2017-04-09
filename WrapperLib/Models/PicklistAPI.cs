using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using WrapperLib.Autotask.Net.Webservices;

namespace WrapperLib.Models
{
    public class PicklistAPI : ApiBase
    {
        public PickListValue[] GetPickListLabelsByField(string entityType, string fieldName,
                                                out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                var fields = _atwsServices.GetFieldInfo(entityType);

                return PickListLabelsFromField(fields, fieldName);
            }

            catch (SoapException ex)
            {
                errorMsg = ex.Message;

                if (errorMsg.Contains("Object reference not set to an instance of an object"))
                {
                    errorMsg = string.Format("Field {0} of Entity {1} doesn't have a picklist.", fieldName, entityType);
                }

                // This is sort of fatal exception. The entity name or field name or 
                // both are incorrect and might not exist.
                return null;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;

                // This is sort of fatal exception. The entity name or field name or 
                // both are incorrect and might not exist.
                return null;
            }
        }

        /// <summary>
        /// Get pick list label given its value.
        /// </summary>
        /// <param name="entityType">e.g. Account</param>
        /// <param name="fieldName">e.g. AccountType which follows a picklist.</param>
        /// <param name="valueToSearch">e.g. 1 which should return label "Customer".</param>
        /// <param name="errorMsg">Error message from SOAP API, if any.</param>
        /// <returns>Label matching the passed value.</returns>
        public string GetPickListLabel(string entityType, string fieldName,
                                        string valueToSearch, out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                var fields = _atwsServices.GetFieldInfo(entityType);

                return PickListLabelFromValue(fields, fieldName, valueToSearch);
            }

            catch (SoapException ex)
            {
                errorMsg = ex.Message;

                // This is sort of fatal exception. The entity name or field name or 
                // both are incorrect and might not exist.
                return string.Empty;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;

                // This is sort of fatal exception. The entity name or field name or 
                // both are incorrect and might not exist.
                return string.Empty;
            }
        }

        /// <summary>
        /// Used to find a specific Field in an array based on the name
        /// </summary>
        /// <param name="field">array containing Fields to search from</param>
        /// <param name="name">contains the name of the Field to search for</param>
        /// <returns>Field match</returns>
        protected static Field FindField(Field[] field, string name)
        {
            return Array.Find(field, element => element.Name == name);
        }

        /// <summary>
        /// Returns the label of a picklist when the value is sent
        /// </summary>
        /// <param name="fields">entity fields</param>
        /// <param name="strField">picklist to choose from</param>
        /// <param name="strPickListValue">value ("id") of picklist</param>
        /// <returns>picklist label</returns>
        protected static string PickListLabelFromValue(Field[] fields, string strField, string strPickListValue)
        {
            string strRet = string.Empty;

            Field fldFieldToFind = FindField(fields, strField);
            if (fldFieldToFind == null)
            {
                throw new Exception("Could not get the " + strField + " field from the collection");
            }
            PickListValue plvValueToFind = FindPickListValue(fldFieldToFind.PicklistValues, strPickListValue);
            if (plvValueToFind != null)
            {
                strRet = plvValueToFind.Label;
            }

            return strRet;
        }

        /// <summary>
        /// Returns the labels of a picklist field
        /// </summary>
        /// <param name="fields">entity fields</param>
        /// <param name="strField">picklist to choose from</param>
        /// <returns>picklist label</returns>
        protected static PickListValue[] PickListLabelsFromField(Field[] fields, string strField)
        {
            string strRet = string.Empty;

            Field fldFieldToFind = FindField(fields, strField);

            if (fldFieldToFind == null)
            {
                throw new Exception("Could not get the " + strField + " field from the collection");
            }

            return fldFieldToFind.PicklistValues;
        }

        /// <summary>
        /// Used to find a specific value in a picklist
        /// </summary>
        /// <param name="pickListValue">array of PickListsValues to search from</param>
        /// <param name="valueID">contains the value of the PickListValue to search for</param>
        /// <returns>PickListValue match</returns>
        protected static PickListValue FindPickListValue(PickListValue[] pickListValue, string valueID)
        {
            return Array.Find(pickListValue, element => element.Value == valueID);
        }
    }
}
