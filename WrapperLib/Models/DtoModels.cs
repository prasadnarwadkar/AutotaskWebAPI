using System;

namespace WrapperLib.Models
{
    public class ResourceRoleDto
    {
        public long ResourceId { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }

    public class RoleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Task details required to create it.
    /// </summary>
    public class TaskDetails
    {
        public long ProjectID { get; set; }
        public string Title { get; set; }
        public long id { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public long AssignedResourceID { get; set; }
        public long AssignedResourceRoleID { get; set; }
        public long CreatorResourceID { get; set; }
        public int TaskType { get; set; }
    }

    /// <summary>
    /// Ticket details required to update it.
    /// </summary>
    public class TicketUpdateDetails
    {
        public long ID
        { get; set; }

        public long AccountID
        {
            get;
            set;
        }

        public long AllocationCodeID
        {
            get;
            set;
        }

        public long ContactID
        {
            get;
            set;
        }

        public long ContractID
        {
            get;
            set;
        }

        public long AssignedResourceID
        {
            get;
            set;
        }

        public long AssignedResourceRoleID
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public DateTime DueDateTime
        {
            get;
            set;
        }

        public decimal EstimatedHours
        {
            get;
            set;
        }

        public long InstalledProductID
        {
            get;
            set;
        }
        
        public long IssueType
        {
            get;
            set;
        }

        public long Priority
        {
            get;
            set;
        }

        public long QueueID
        {
            get;
            set;
        }
        
        public long Source
        {
            get;
            set;
        }

        public long Status
        {
            get;
            set;
        }

        public long SubIssueType
        {
            get;
            set;
        }
        
        public string Title
        {
            get;
            set;
        }
        
        public long ServiceLevelAgreementID
        {
            get;
            set;
        }

        public string Resolution
        {
            get;
            set;
        }

        public string PurchaseOrderNumber
        {
            get;
            set;
        }
        
        public long TicketType
        {
            get;
            set;
        }

        public long ProblemTicketId
        {
            get;
            set;
        }
        
        public long OpportunityId
        {
            get;
            set;
        }

        public long ChangeApprovalBoard
        {
            get;
            set;
        }

        public long ChangeApprovalType
        {
            get;
            set;
        }
        
        public long ChangeApprovalStatus
        {
            get;
            set;
        }

        public string ChangeInfoField1
        {
            get;
            set;
        }

        public string ChangeInfoField2
        {
            get;
            set;
        }
        
        public string ChangeInfoField3
        {
            get;
            set;
        }
        
        public string ChangeInfoField4
        {
            get;
            set;
        }

        public string ChangeInfoField5
        {
            get;
            set;
        }
        
        public long ContractServiceID
        {
            get;
            set;
        }

        public long ContractServiceBundleID
        {
            get;
            set;
        }
    }
}