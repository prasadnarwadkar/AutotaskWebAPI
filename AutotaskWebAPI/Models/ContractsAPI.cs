using AutotaskWebAPI.Autotask.Net.Webservices;
using System.Collections.Generic;
using System.Text;

namespace AutotaskWebAPI.Models
{
    public class ContractsAPI
    {
        private AutotaskAPI api = null;

        public ContractsAPI(AutotaskAPI apiInstance)
        {
            api = apiInstance;
        }

        public Contract GetContractById(long id, out string errorMsg)
        {
            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strContract = new StringBuilder();
            strContract.Append("<queryxml version=\"1.0\">");
            strContract.Append("<entity>Contract</entity>");
            strContract.Append("<query>");
            strContract.Append("<field>id<expression op=\"equals\">");
            strContract.Append(id);
            strContract.Append("</expression></field>");
            strContract.Append("</query></queryxml>");

            ATWSResponse respContract = api._atwsServices.query(strContract.ToString(), out errorMsg);

            if (respContract.ReturnCode > 0 && respContract.EntityResults.Length > 0)
            {
                foreach (Entity entity in respContract.EntityResults)
                {
                     return (Contract)respContract.EntityResults[0];
                }
            }
            else if (respContract.Errors != null &&
                    respContract.Errors.Length > 0)
            {
                errorMsg = respContract.Errors[0].Message;

                return null;
            }

            return null;
        }

        public List<Contract> GetContractByAccountId(long accountId, out string errorMsg)
        {
            List<Contract> list = new List<Contract>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strContract = new StringBuilder();
            strContract.Append("<queryxml version=\"1.0\">");
            strContract.Append("<entity>Contract</entity>");
            strContract.Append("<query>");
            strContract.Append("<field>AccountID<expression op=\"equals\">");
            strContract.Append(accountId);
            strContract.Append("</expression></field>");
            strContract.Append("</query></queryxml>");

            ATWSResponse respContract = api._atwsServices.query(strContract.ToString(), out errorMsg);

            if (respContract.ReturnCode > 0 && respContract.EntityResults.Length > 0)
            {
                foreach (Entity entity in respContract.EntityResults)
                {
                    list.Add((Contract)entity);
                }
            }
            else if (respContract.Errors != null &&
                    respContract.Errors.Length > 0)
            {
                errorMsg = respContract.Errors[0].Message;
            }

            return list;
        }

        public List<Contract> GetContractByContactId(long contactId, out string errorMsg)
        {
            List<Contract> list = new List<Contract>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strContract = new StringBuilder();
            strContract.Append("<queryxml version=\"1.0\">");
            strContract.Append("<entity>Contract</entity>");
            strContract.Append("<query>");
            strContract.Append("<field>ContactID<expression op=\"equals\">");
            strContract.Append(contactId);
            strContract.Append("</expression></field>");
            strContract.Append("</query></queryxml>");

            ATWSResponse respContract = api._atwsServices.query(strContract.ToString(), out errorMsg);

            if (respContract.ReturnCode > 0 && respContract.EntityResults.Length > 0)
            {
                foreach (Entity entity in respContract.EntityResults)
                {
                    list.Add((Contract)entity);
                }
            }
            else if (respContract.Errors != null &&
                    respContract.Errors.Length > 0)
            {
                errorMsg = respContract.Errors[0].Message;
            }

            return list;
        }

        public List<Contract> GetContractByOpportunityId(long opportunityId, out string errorMsg)
        {
            List<Contract> list = new List<Contract>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strContract = new StringBuilder();
            strContract.Append("<queryxml version=\"1.0\">");
            strContract.Append("<entity>Contract</entity>");
            strContract.Append("<query>");
            strContract.Append("<field>OpportunityID<expression op=\"equals\">");
            strContract.Append(opportunityId);
            strContract.Append("</expression></field>");
            strContract.Append("</query></queryxml>");

            ATWSResponse respContract = api._atwsServices.query(strContract.ToString(), out errorMsg);

            if (respContract.ReturnCode > 0 && respContract.EntityResults.Length > 0)
            {
                foreach (Entity entity in respContract.EntityResults)
                {
                    list.Add((Contract)entity);
                }
            }
            else if (respContract.Errors != null &&
                    respContract.Errors.Length > 0)
            {
                errorMsg = respContract.Errors[0].Message;
            }

            return list;
        }
    }
}