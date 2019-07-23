using System;
using System.IdentityModel.Selectors;

namespace Server
{
	public class ServiceAuthenticator : UserNamePasswordValidator
	{
		public override void Validate(string userName, string password)
		{
			if (string.IsNullOrEmpty(userName) || string.IsNullOrWhiteSpace(userName))
			{
				throw new ArgumentException("userName invalid");
			}

			if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
			{
				throw new ArgumentException("password invalid");
			}

			using (var ctx = new ModelContext())
			{
				var user = ctx.GetUser(userName);
				if (user == null)
				{
					throw new ArgumentException("userName not found");
				}

				if (user.Password != password)
				{
					throw new ArgumentException("password does not mach userName");
				}
			}
		}
	}
}
