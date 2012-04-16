// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateMembershipStatusDescriptions.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   A Dictionary that maps a <seealso cref="MembershipCreateStatus" /> to a status message
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Security
{
    using System.Collections.Generic;
    using System.Web.Security;

    /// <summary>
    /// A Dictionary that maps a <seealso cref="MembershipCreateStatus"/> to a status message
    /// </summary>
    public sealed class CreateMembershipStatusDescriptions : Dictionary<MembershipCreateStatus, string>
    {
        /// <summary>
        /// The single instance of the class
        /// </summary>
        private static readonly CreateMembershipStatusDescriptions MembershipStatusDescriptionInstance =
            new CreateMembershipStatusDescriptions();

        /// <summary>
        /// Prevents a default instance of the <see cref="CreateMembershipStatusDescriptions"/> class from being created
        /// and initializes the static instance.
        /// </summary>
        private CreateMembershipStatusDescriptions()
        {
            Add(
                MembershipCreateStatus.DuplicateUserName,
                "Username already exists. Please enter a different user name.");
            Add(
                MembershipCreateStatus.DuplicateEmail,
                "A username for that e-mail address already exists. Please enter a different e-mail address.");
            Add(
                MembershipCreateStatus.InvalidPassword,
                "The password provided is invalid. Please enter a valid password value.");
            Add(
                MembershipCreateStatus.InvalidEmail,
                "The e-mail address provided is invalid. Please check the value and try again.");
            Add(
                MembershipCreateStatus.InvalidAnswer,
                "The password retrieval answer provided is invalid. Please check the value and try again.");
            Add(
                MembershipCreateStatus.InvalidQuestion,
                "The password retrieval question provided is invalid. Please check the value and try again.");
            Add(
                MembershipCreateStatus.InvalidUserName,
                "The user name provided is invalid. Please check the value and try again.");
            Add(
                MembershipCreateStatus.ProviderError,
                "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.");
            Add(
                MembershipCreateStatus.UserRejected,
                "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.");
            Add(
                MembershipCreateStatus.DuplicateProviderUserKey,
                "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.");
            Add(
                MembershipCreateStatus.InvalidProviderUserKey,
                "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.");
        }

        /// <summary>
        /// Gets the instance as a Dictionary
        /// </summary>
        public static CreateMembershipStatusDescriptions Status
        {
            get
            {
                return MembershipStatusDescriptionInstance;
            }
        }

        /// <summary>
        /// Hides the Dictionary's add method to prevent the ability to add to the dictionary publicly
        /// </summary>
        /// <param name="createStatus">The <seealso cref="MembershipCreateStatus"/> that is the key</param>
        /// <param name="description">The status message string for the status key</param>
        private new void Add(MembershipCreateStatus createStatus, string description)
        {
            base.Add(createStatus, description);
        }
    }
}
