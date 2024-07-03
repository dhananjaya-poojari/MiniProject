# TinyUrl

## Overview

TinyUrl is a URL shortening service deployed as an AWS Lambda function. This document provides the steps to configure AWS CLI and deploy the function using the .NET Lambda tools.

## Prerequisites

- .NET SDK
- AWS CLI
- AWS account with IAM permissions to create and deploy Lambda functions

## Step 1: Configure AWS CLI

Run the following command to configure your AWS credentials. This will authorize AWS to deploy your Lambda function.

```sh
aws configure
```
You will be prompted to enter your AWS Access Key ID, Secret Access Key, default region name, and default output format.

Step 2: Deploy the Lambda Function
Navigate to the TinyUrl project directory and run the deployment command:

sh
```
C:\Repos\TinyUrl\TinyUrl>dotnet lambda deploy-function
```
You will see the following output during the deployment process:

sh
```
Amazon Lambda Tools for .NET Core applications (5.10.6)   
Project Home: https://github.com/aws/aws-extensions-for-dotnet-cli,                                                                                                                                           
Enter Runtime: (The runtime environment for the Lambda function) dotnet8                                                                                                                 
Executing publish command                                                                                               
Deleted previous publish folder                                                                                         
... invoking 'dotnet publish', working folder 'C:\Repos\TinyUrl\TinyUrl\bin\Release\net8.0\publish'                     
... dotnet publish "C:\Repos\TinyUrl\TinyUrl" --output "C:\Repos\TinyUrl\TinyUrl\bin\Release\net8.0\publish" 
--configuration "Release" --framework "net8.0" /p:GenerateRuntimeConfigurationFiles=true --runtime linux-x64 --self-contained False                                                                                                                        ... publish:   Determining projects to restore...                                                                       
... publish:   All projects are up-to-date for restore.                                                                 
... publish: C:\Program Files\dotnet\sdk\9.0.100-preview.4.24267.66\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(314,5): message NETSDK1057: You are using a preview version of .NET. See: https://aka.ms/dotnet-support-policy [C:\Repos\TinyUrl\TinyUrl\TinyUrl.csproj]                                                            
... publish:   TinyUrl -> C:\Repos\TinyUrl\TinyUrl\bin\Release\net8.0\linux-x64\TinyUrl.dll                             
... publish:   TinyUrl -> C:\Repos\TinyUrl\TinyUrl\bin\Release\net8.0\publish\                                          
Zipping publish folder C:\Repos\TinyUrl\TinyUrl\bin\Release\net8.0\publish to C:\Repos\TinyUrl\TinyUrl\bin\Release\net8.0\TinyUrl.zip                                                                                                           
... zipping: Amazon.Lambda.APIGatewayEvents.dll                                                                         
... zipping: Amazon.Lambda.ApplicationLoadBalancerEvents.dll                                                            
... zipping: Amazon.Lambda.AspNetCoreServer.dll                                                                         
... zipping: Amazon.Lambda.AspNetCoreServer.Hosting.dll                                                                 
... zipping: Amazon.Lambda.Core.dll                                                                                     
... zipping: Amazon.Lambda.Logging.AspNetCore.dll                                                                       
... zipping: Amazon.Lambda.RuntimeSupport.dll                                                                           
... zipping: Amazon.Lambda.Serialization.SystemTextJson.dll                                                             
... zipping: appsettings.Development.json                                                                               
... zipping: appsettings.json                                                                                           
... zipping: AWSSDK.Core.dll                                                                                            
... zipping: AWSSDK.SecurityToken.dll                                                                                   
... zipping: DnsClient.dll                                                                                              
... zipping: libmongocrypt.so                                                                                           
... zipping: Microsoft.OpenApi.dll                                                                                      
... zipping: MongoDB.Bson.dll                                                                                           
... zipping: MongoDB.Driver.Core.dll                                                                                    
... zipping: MongoDB.Driver.dll                                                                                         
... zipping: MongoDB.Libmongocrypt.dll                                                                                  
... zipping: Newtonsoft.Json.dll                                                                                        
... zipping: SharpCompress.dll                                                                                          
... zipping: Snappier.dll                                                                                               
... zipping: Swashbuckle.AspNetCore.Swagger.dll                                                                         
... zipping: Swashbuckle.AspNetCore.SwaggerGen.dll                                                                      
... zipping: Swashbuckle.AspNetCore.SwaggerUI.dll                                                                       
... zipping: TinyUrl                                                                                                    
... zipping: TinyUrl.deps.json                                                                                          
... zipping: TinyUrl.dll                                                                                                
... zipping: TinyUrl.pdb                                                                                                
... zipping: TinyUrl.runtimeconfig.json                                                                                 
... zipping: TinyUrl.staticwebassets.endpoints.json                                                                     
... zipping: ZstdSharp.dll                                                                                              
Created publish archive (C:\Repos\TinyUrl\TinyUrl\bin\Release\net8.0\TinyUrl.zip).                                      
Enter Function Name: (AWS Lambda function name) urlshortner                                                                                                             
Creating new Lambda function                                                                                           
Select IAM Role that to provide AWS credentials to your code:                                                             
1) url-shortner                                                                                                          
2) *** Create new IAM Role ***                                                                                       
1                                                                                                                      
Enter Memory Size: (The amount of memory, in MB, your Lambda function is given) 128                                                                                                                    
Enter Timeout: (The function execution timeout in seconds) 5                                                                                                                    
Enter Handler: (Handler for the function <assembly>::<type>::<method>) TinyUrl                                                                                                                 
New Lambda function created
```