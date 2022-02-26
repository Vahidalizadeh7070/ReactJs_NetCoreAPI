# React js & .Net Core API
This repository contains a front end which is implemented by ReactJS and a back end section which is implemented by .Net core API.
This example includes three main sections.
- FrontEnd section which is going to display to user
- BackEnd section which is going to use in the Management and FrontEnd section
- Management section which is going to handle data in the FrontEnd

<h3>Frontend</h3>
The front end section is implemented by React js. We have used React-Router-Dom to handle the route of the single application and also use Axios and Fetch to connect the application with the API. Moreover, we use Bootstrap to design the template of this single web application

<hr/>
<h3>BackEnd</h3>
The back end section is implemented by .Net core api and we use Entity framework Core and Repository pattern to perform the CRUD opertaion in the API. In addition, we use Asp.net Identity and JWT to authenticate and authorize the users and determine the accessibility of users all over the application.

<hr/>
<h3>Management</h3>
The management section is a dashboard that admin can manage all informations in the database. for instance adming can add, modify or delete any records or manage the users in the database. It is implemented by .Net core mvc and <code>HttpClient</code> package to connect with the backend API.

<h5>Packages in used</h5>
All packages that we have used in our application is: 

- WebApi.Client : <code>Install-Package Microsoft.AspNet.WebApi.Client -Version 5.2.7</code>
- Asp.Net Identity : <code>Install-Package Microsoft.AspNet.Identity.Core -Version 2.2.3</code>
- JWT Bearer : <code>Install-Package Microsoft.AspNetCore.Authentication.JwtBearer</code>
- Newtonsoft : <code>Install-Package Newtonsoft.Json -Version 13.0.1</code>

<h3>Description of implementation of Authentication</h3>
For both section of Management and FrontEnd, it is important to store jwt token in the token and then we can pass this token to the api and check if it is a valid token you can have access to the API based on their roles. For more details, please refer to the following links.
<br/>
<a href="https://medium.com/c-sharp-progarmming/asp-net-core-5-jwt-authentication-tutorial-with-example-api-aa59e80d02da">JWT in .net core 5</a>
