# WebApiTest

Coding exercise for Vaco Built.
The solution has been prepared before the coding test to set up a base code for a Web Api.
I choose to not create a database to simplify the configuration for other users.

# Security

You can find in the appsettings a parameter named 'api-key'. This parameter is used has authentication to call the endpoints.
You need to add a header named 'api-key' with the value defined in the appsettings.json when calling any endpoint.

# Data storage

Datas are simply stored in json files. No database has been set up for the project.
In the appsettings.json you can define the path where you want to store the data file. The parameter is called 'UserDatabaseLocation'.
Be careful the folder where you want to store the file should exists !!
