# ServiceLogger
This is a API which loggs the service requests to txt file, to database and sends emails to the users. It includes spaggetti code all over.
<h1>What is this and what does this work for?</h1>
<p>This is a API which has only '/products' endpoint. When this endpoint took a request, API will send a request to a specisif service which you want to test it.
Then API will take response and analyze it. It has file logics so it will create or use the txt file with related path and log the details to this txt.
API also will connect to SQLServer and log the details there. And finally, it will send emails to the users according to the statuscode of response.</p>
