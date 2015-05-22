Demo for EF7 Non-Relational Support
Part of Julie Lerman's "EF7: Who are you and what have you done to my ORM?" presentation.
This talk was given at Boston Code Camp (March 21, 2015), Techorama (May 2015) and DevSum15 (May 2015).

This solution,EF7NoSQL, uses EF7.0.0-beta 1 along with the "proof of concept" API for the Azure Table Storage EF provider.
You'll need to rebuild to retrieve the relevant packages from Nuget (and you need to be online).

The live demo used a connection string pointing to Julie Lerman's demo Azure account and a particular Azure Table Storage store.

The Test project's app.config file has a connection string to point to an Azure account. You'll need to use your own Azure account (or a trial) via portal.azure.com, create a Storage Account in there and then create an Azure Table in there. Then you will have an account name and account key to plug into the app.config to test for yourself.

If you don't want to run the tests but want to see this in action, you can see it in Julie's Pluralsight course: "Looking Ahead to Entity Framework 7" at bit.ly/PS-EF7Alpha.


Julie Lerman
thedatafarm.com
@julielerman

