namespace Domain;
public static class EnvironmentFetcher
{
	public static string? IssuerSecretKey { get; set; }
	public static string? PostgresConnectionString {  get; set; }
	public static void Fetch()
	{
		IssuerSecretKey = Environment.GetEnvironmentVariable("ISSUER_SECRET_KEY");
		PostgresConnectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");

		if ( IssuerSecretKey == null )
		{
			throw new ApplicationException("ISSUER_SECRET_KEY was not supplied to the environment.");
		}
		if (PostgresConnectionString == null)
		{
			throw new ApplicationException("POSTGRES_CONNECTION_STRING was not supplied to the environment.");
		}
	}
}
