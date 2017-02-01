using AutotaskWebAPI.Autotask.Net.Webservices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web.Services.Protocols;

namespace AutotaskWebAPI.Models
{
    public class TicketDetails
    {
        public long AccountID { get; set; }
        public string Title { get; set; }
        public long id { get;  set; }
        public string Description { get;  set; }
        public int Status { get;  set; }
        public string DueDateTime { get; set; }
        public long AssignedResourceID  { get; set; }
        public long AssignedResourceRoleID { get; set; }
        public long CreatorResourceID { get; set; }
        public int Priority { get; set; }
    }

    /// <summary>
    /// Public Class AutotaskAPI.
    /// </summary>
    public class AutotaskAPI
	{
		public ATWS _atwsServices = null;
        private int utcOffsetInMins = Convert.ToInt32(ConfigurationManager.AppSettings["utcOffsetInMins"]);
        private string _webServiceBaseAPIURL = ConfigurationManager.AppSettings["APIServiceURLZoneInfo"];
        
		/// <summary>
		/// Public Constuctor.
		/// </summary>
		public AutotaskAPI(string user, string pass)
		{
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("Autotask API username is blank");
            }
            else
            {
                if (string.IsNullOrEmpty(pass))
                {
                    throw new ArgumentException("Autotask API password is blank");
                }
            }

            // Initialize db context.
			string zoneURL = string.Empty;

			this._atwsServices = new ATWS();
			this._atwsServices.Url = this._webServiceBaseAPIURL;

			CredentialCache cache = new CredentialCache();
			cache.Add(new Uri(this._atwsServices.Url), "BASIC", new NetworkCredential(user, pass));
			this._atwsServices.Credentials = cache;

            try
            {
                ATWSZoneInfo zoneInfo = new ATWSZoneInfo();
                zoneInfo = this._atwsServices.getZoneInfo(user);
                if (zoneInfo.ErrorCode >= 0)
                {
                    zoneURL = zoneInfo.URL;
                    this._atwsServices = new ATWS();
                    this._atwsServices.Url = zoneInfo.URL;
                    cache = new CredentialCache();
                    cache.Add(new Uri(this._atwsServices.Url), "BASIC", new NetworkCredential(user, pass));
                    this._atwsServices.Credentials = cache;
                }
                else
                {
                    //throw new Exception("Error with getZoneInfo()");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error with getZoneInfo()- error: " + ex.Message);
            }
        }       

        /// <summary>
        /// Get pick list label given its value.
        /// </summary>
        /// <param name="entityType">e.g. Account</param>
        /// <param name="fieldName">e.g. AccountType which follows a picklist.</param>
        /// <param name="valueToSearch">e.g. 1 which should return label "Customer".</param>
        /// <returns>Label matching the passed value.</returns>
        public string GetPickListLabel(string entityType, string fieldName,
                                        string valueToSearch, out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                var fields = this._atwsServices.GetFieldInfo(entityType);

                return AutotaskAPI.PickListLabelFromValue(fields, fieldName, valueToSearch);
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

        public PickListValue[] GetPickListLabelsByField(string entityType, string fieldName,
                                                out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                var fields = this._atwsServices.GetFieldInfo(entityType);

                return AutotaskAPI.PickListLabelsFromField(fields, fieldName);
            }

            catch (SoapException ex)
            {
                errorMsg = ex.Message;

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
        /// <param name="strField">picklick to choose from</param>
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
        /// <param name="strField">picklick to choose from</param>
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
