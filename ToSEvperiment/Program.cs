// See https://aka.ms/new-console-template for more information
using ToSEvperiment;

static async Task Main(string[] args)
{
    var auth = new Auth();
    //var (appKey, appSecret, csAuthUrl) = Auth.ConstructInitAuthUrl();
   await Auth.authorizer();

}


