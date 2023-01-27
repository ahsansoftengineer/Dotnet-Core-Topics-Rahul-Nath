## DynamoDB

### What is DynamoDB?
1. Amazon DynamoDB is a fully managed NoSQL database service provided by Amazon Web Services (AWS). It allows developers to create, update, and query highly scalable and high-performance tables, with support for both document and key-value data models. 
2. DynamoDB automatically handles tasks such as scaling, data replication, and hardware provisioning, allowing developers to focus on building their applications. 
3. It is also highly available and durable, with the ability to automatically replicate data across multiple availability zones.

### What is Primary Key in DynamoDB?
1. In Amazon DynamoDB, a primary key is a unique identifier for an item in a table. Each table in DynamoDB has a primary key, which can be either a single attribute (known as a partition key) or a combination of two attributes (known as a partition key and sort key).
2. A partition key is used to identify an individual item within a table. All items with the same partition key are stored together, and are sorted in ascending order by sort key.
3. A sort key is an optional attribute that can be used to further sort items within a partition.
When you create a table, you must specify the primary key, which determines how the data in the table is stored and retrieved. The primary key for a table can be either a simple primary key (just partition key) or composite key (partition key and sort key)
4. For example, you could create a table to store information about books, with the primary key being the ISBN (International Standard Book Number) of the book (partition key) and the publication date(sort key).

