using log4net;
using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace Server
{
	public class ServiceAuthenticator : UserNamePasswordValidator
	{
		private ILog log = LogManager.GetLogger(typeof(ServiceAuthenticator));

		public override void Validate(string userName, string password)
		{
			if (string.IsNullOrEmpty(userName) || string.IsNullOrWhiteSpace(userName))
			{
				log.Warn($"Invalid login credentials: {userName}");
				throw new FaultException("Username is invalid");
			}

			if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
			{
				log.Warn($"Invalid login credentials: {password}");
				throw new FaultException("Password is invalid");
			}

			using (var ctx = new ModelContext())
			{
				var user = ctx.GetUser(userName);
				if (user == null)
				{
					log.Warn($"Invalid login credentials: {userName} not found");
					throw new FaultException("Username not found");
				}

				if (user.Password != password)
				{
					log.Warn($"Invalid login credentials: {userName}|{password} does not match");
					throw new FaultException("Password does not match username");
				}
			}

			log.Info($"User {userName}|{password} authenticated");
		}
	}
}
