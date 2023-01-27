## AZURE KEY VAULT

### What is Azure Key Vault?
Azure Key Vault is a cloud service for securely storing and accessing secrets. A secret is anything that you want to tightly control access to, such as API keys, passwords, certificates, or cryptographic keys. Key Vault service supports two types of containers: vaults and managed hardware security module(HSM) pools.

### Azure Key Vault
- Azure Key Vault is one of several key management solutions in Azure, and helps solve the following problems:

1. Secrets Management - Azure Key Vault can be used to Securely store and tightly control access to tokens, passwords, certificates, API keys, and other secrets
2. Key Management - Azure Key Vault can be used as a Key Management solution. Azure Key Vault makes it easy to create and control the encryption keys used to encrypt your data.
3. Certificate Management - Azure Key Vault lets you easily provision, manage, and deploy public and private Transport Layer Security/Secure Sockets Layer (TLS/SSL) certificates for use with Azure and your internal connected resources.
4. Azure Key Vault has two service tiers: Standard, which encrypts with a software key, and a Premium tier, which includes hardware security module(HSM)-protected keys. To see a comparison between the Standard and Premium tiers, see the Azure Key Vault pricing page.

### Azure Key Vault VS Azure Storage Service?
1. In general, you should use Azure Key Vault when you need to store and manage sensitive information such as keys and secrets, and you should use Azure Storage when you need to store and manage data that needs to be accessed and processed by applications or other Azure services.


### How to Create Azure Key Vault Accout For Free?
1. Follow the Instruction on the following Link
2. To Create a Resource group
```c#
az storage account create 
	--resource-group learn-7830b601-5de1-44c8-b234-b1117f13b7e7 
	--location westus 
	--sku Standard_LRS 
	--name ahsanaccount
```
3. To Add Azure & Key Vault Storage Package
```c#
dotnet add package Azure.Storage.Blobs
dotnet add package Microsoft.Extensions.Configuration.AzureKeyVault
```
4. To See the Connection String
```c#
	az storage account show-connection-string 
	--resource-group learn-7830b601-5de1-44c8-b234-b1117f13b7e7 
	--query connectionString 
	--name ahsanaccount
```
5. ConnectionString
```json
"DefaultEndpointsProtocol=https;
	EndpointSuffix=core.windows.net;
	AccountName=ahsanaccount;
	AccountKey=UvNkayBMh4uf4W90GZM0y7QsQFcIHzq3Vsbc3w6W9aLWX2XS8XziyhFLtltCFWHBVGSjEc3c5MDC+ASt3s7lQg==;
	BlobEndpoint=https://ahsanaccount.blob.core.windows.net/;
	FileEndpoint=https://ahsanaccount.file.core.windows.net/;
	QueueEndpoint=https://ahsanaccount.queue.core.windows.net/;
	TableEndpoint=https://ahsanaccount.table.core.windows.net/"
```
