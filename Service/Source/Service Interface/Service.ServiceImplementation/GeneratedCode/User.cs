//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using WCF = global::System.ServiceModel;

namespace Service.ServiceImplementation
{	
	/// <summary>
	/// Service Class - User
	/// </summary>
	[WCF::ServiceBehavior(Name = "User", 
		Namespace = "http://CareManagement.Model/2012/User", 
		InstanceContextMode = WCF::InstanceContextMode.PerSession, 
		ConcurrencyMode = WCF::ConcurrencyMode.Single )]
	public abstract class UserBase : Service.ServiceContracts.IUser
	{
		#region User Members

		public virtual Service.MessageContracts.AuthenticateResponse Authenticate(Service.MessageContracts.AuthenticateRequest request)
		{
			return null;
		}

		public virtual Service.MessageContracts.UserCreatedResponse Create(Service.MessageContracts.CreateUserRequest request)
		{
			return null;
		}

		#endregion		
		
	}
	
	public partial class User : UserBase
	{
	}
	
}

