using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace Server
{
	public class ServiceAuthenticator : UserNamePasswordValidator
	{
		public override void Validate(string userName, string password)
		{
			if (string.IsNullOrEmpty(userName) || string.IsNullOrWhiteSpace(userName))
			{
				throw new FaultException("Username is invalid");
			}

			if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
			{
				throw new FaultException("Password is invalid");
			}

			using (var ctx = new ModelContext())
			{
				var user = ctx.GetUser(userName);
				if (user == null)
				{
					throw new FaultException("Username not found");
				}

				if (user.Password != password)
				{
					throw new FaultException("Password does not match username");
				}
			}
		}
	}
}
