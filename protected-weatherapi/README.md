This is continuation of [Duende Identity Server Setup](https://github.com/zeecorleone/duende-identityserver-basic). This solution contains following two projects in order to demonstrate the working of Duende Identity Server.

####  MVC Web App:
 This required Authentication itself (`Authorization Code`), and calls another protected Api to fetch data.
 
#### Web Api:
This is protected web api, which MVC Web App accesses (using `Client Credentials`) to fetch data.
