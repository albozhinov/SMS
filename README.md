# SMS
SMS (SuperMarket System) is an online supermarket for shopping
1.	Technological Requirements
•	Use the MyWebServer
•	Use Entity Framework Core – 5.0.7
The Technological Requirements are ABSOLUTE. If you do not follow them, you will NOT be scored for other Requirements. 
Now that you know the Technological Requirements, let us see what the Functional Requirements are.
2.	Database Requirements
The Database of SMS needs to support 3 entities:
User	
•	Has an Id – a string, Primary Key
•	Has a Username – a string with min length 5 and max length 20 (required)
•	Has an Email – a string, which holds only valid email (required)
•	Has a Password – a string with min length 6 and max length 20 - hashed in the database (required)
•	Has a Cart – a Cart object (required)
Product
•	Has an Id – a string, Primary Key
•	Has a Name – a string with min length 4 and max length 20 (required)
•	Has Price – a decimal (in range 0.05 – 1000)
•	Has a Cart – a Cart object
Cart
•	Has an Id – a string, Primary Key
•	Has User – a User object (required)
•	Has Products – a collection of Products 
Implement the entities with the correct datatypes and their relations.
3.	Page Requirements
Index Page (logged-out user)
/Users/Login (logged-out user)
/Users/Register (logged-out user)
NOTE: Upon successful registration, a Cart for the User is created automatically.
Home Page (logged-in user)
NOTE: The [Add to Cart] adds the Product to User's Cart.
Products/Create (logged-in user)
Carts/Details (logged-in user)
NOTE: All Products for the current logged in User's Cart are rendered below.
NOTE: The templates should look EXACTLY as shown above.
NOTE: The templates do NOT require additional CSS for you to write. Only bootstrap and the given css are enough.
NOTE: If any of the validations in the POST forms don't pass you can redirect to the same page (reload/refresh it) or visualize the Error page with appropriate error message.
4.	Functionality
The functionality of SMS Platform is very simple.
Users
Guests can Register, Login and view the Index Page. 
Users can Create Products and see All Products on the Home Page. From the Home Page they can also Add Products and view Details about their Cart and buy all Products.
Products
Products can be Created by Users. All created Products are visualized on the Home Page.
Products are visualized on the Home Page as a table with Name of the Product, Price and action Add to Cart.
Cart
Products added are stored in the User's Cart. In Cart Details Page, User can buy the products.
When User decides to buy the products in his cart, the Cart becomes empty (products are deleted).
5.	Security
The Security section mainly describes access requirements. Configurations about which users can access specific functionalities and pages:
•	Guest (not logged in) users can access Index page.
•	Guest (not logged in) users can access Login page.
•	Guest (not logged in) users can access Register page.
•	Users (logged in) cannot access Guest pages.
•	Users (logged in) cannot access Login pages.
•	Users (logged in) cannot access Register pages.
•	Users (logged in) can access Home page.
•	Users (logged in) can access Product Create page and functionality.
•	Users (logged in) can access Cart Details page and functionality.
•	Users (logged in) can access Logout functionality.
