using System.Collections.Generic;
using System.IO;
using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;

namespace AwscdkDemo01
{
    internal sealed class Program
    {
        public static void Main(string[] args)
        {
            App app = new();
            Stack stack = new(app, "RequestAuthorizerInteg");
            /*Function authorizerFn = new(stack, "MyAuthorizerFunction", new FunctionProps
            {
                Runtime = Runtime.NODEJS_14_X,
                Handler = "index.handler",
                //Code = AssetCode.FromAsset(Path.Join(__dirname, "integ.request-authorizer.handler"))
            });*/

            RestApi restapi = new(stack, "MyRestApi");
            
            /*
            RequestAuthorizer authorizer = new(stack, "MyAuthorizer", new RequestAuthorizerProps
            {
                Handler = authorizerFn,
                IdentitySources = new[] { IdentitySource.Header("Authorization"), IdentitySource.QueryString("allow") }
            });
            */
            
            restapi.Root.AddMethod("ANY", new MockIntegration(new IntegrationOptions
            {
                IntegrationResponses = new IIntegrationResponse[] { new IntegrationResponse { StatusCode = "200" } },
                PassthroughBehavior = PassthroughBehavior.NEVER,
                RequestTemplates = new Dictionary<string, string> {
                    { "application/json", "{ \"statusCode\": 200 }" }
                }
            }), new MethodOptions
            {
                MethodResponses = new[] { new MethodResponse { StatusCode = "200" } },
                //Authorizer = authorizer
            });
            /*new AwscdkDemo01Stack(app, "AwscdkDemo01Stack", new StackProps
            {
                // If you don't specify 'env', this stack will be environment-agnostic.
                // Account/Region-dependent features and context lookups will not work,
                // but a single synthesized template can be deployed anywhere.

                // Uncomment the next block to specialize this stack for the AWS Account
                // and Region that are implied by the current CLI configuration.
                /*
                Env = new Amazon.CDK.Environment
                {
                    Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                    Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
                }
                #1#

                // Uncomment the next block if you know exactly what Account and Region you
                // want to deploy the stack to.
                /*
                Env = new Amazon.CDK.Environment
                {
                    Account = "123456789012",
                    Region = "us-east-1",
                }
                #1#

                // For more information, see https://docs.aws.amazon.com/cdk/latest/guide/environments.html
            });*/
            app.Synth();
        }
    }
}