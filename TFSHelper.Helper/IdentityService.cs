﻿using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using TFSHelper.Helper.Model;

namespace TFSHelper.Helper
{
    public class IdentityService
    {
        public IIdentityManagementService IdentityManagementService { get; set; }
        private readonly TfsTeamProjectCollection _projectCollection;

        public IdentityService(TfsTeamProjectCollection projectCollection)
        {
            _projectCollection = projectCollection;
            // GetFieldValue the TFS Identity Management Service
            IdentityManagementService =
               projectCollection.GetService<IIdentityManagementService>();
        }
        public TeamFoundationIdentity GetTFSIdentity(string searchValue, IdentitySearchFactor identitySearchFactor)
        {
            // Look up the user that we want to impersonate
            return IdentityManagementService.ReadIdentity(identitySearchFactor,
                                                       searchValue, MembershipQuery.None, ReadIdentityOptions.None);
        }
        public AlertRecipient GetUserRecipient(TeamFoundationIdentity teamFoundationIdentity)
        {
            return new AlertRecipient
            {
                EmailAddress = teamFoundationIdentity.GetProperty("Mail").ToString(),
                DisplayName = teamFoundationIdentity.DisplayName,
                DomainName = teamFoundationIdentity.UniqueName,
                TeamFoundationId = teamFoundationIdentity.TeamFoundationId
            };

        }
    }
}
