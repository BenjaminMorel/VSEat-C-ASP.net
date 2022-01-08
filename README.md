# Guide - How To Use ValaisEat

Website Link : http://153.109.124.35:81/Vouillamoz_Morel_ValaisEat/Account/Login

## User stories : 

### (LOGIN) : 
- The default page of the website is the login page that ask you for your credentials, you can enter them or you can sign up if you haven't create an account yet. All other pages on the website check for the ID session before the display of the page, if the ID session is null the page will redirect you to the login page. 
- This security avoids the possibilty of redirecting yourself to a specific page, which you know the name, without entering valid credential first. This verification exist for type of connection, for example you can't show the main page of the staff if your ID session (ID_STAFF) is set to null. We used a different ID session for every different type of connecion (Staff, User, Restaurant and Admin) like that it's impossible for a user to see the content of an admin page. 

### (ORDER) : 
- A logged customer can see all diferent restaurants in the main page, he can choose to filter this view by region or by restaurant type (Burger, pizza or asiatique) after that he can choose a restaurant and display all dishes the restaurant propose. 
- He can add product to the cart (the cart verified that the product inside him, all came from the same restaurant otherwise it will not let you add it) when the user is done with his choice he can redirect himself to the confirmation page where all of his products are display and he need to choose the hours of the delivery (every 15min between 9:00 and 22:00) he also need to choose the delivery address. 

### (Delivery Management) : 
- When a user confirmed his order, the newly created order is send to the concerning restaurant, when the restaurant has finished to prepared it he will change the order status. At this moment we will look into the database to find a staff member that worked in the same region as the restaurant and next we will check if the staff member doesn't have than 5 order to delivery in the same 30 minutes as the new order. If it's not the case the method will assign the order to the first staff member who matched all restriction. 
- For the delivery address we checked it when the user try to valid the order. If the delivery address region is different from the restaurant region the page won't valid the order and it will display an error message asking the user to choose a delivery adress in the same region. 

### (Delivery Interface) : 
- A staff member can connect to his account from the login page, it will then set his Session ID (ID_STAFF) to his staff member and the website will let him see his main page. The main page of the delivery display two tables, one with the order that he still need to deliver and an other one that is his historic. 
- When the staff member delivered an order he can go to this page to change the status of the order from "To be delivered" to "Finished". The page allow him to see the details of every command he has made.  


## MAIN FEATURES : 

### USER - Credentials: EMAIL user@valaiseat.ch PASSWORD password
- You will be redirected to the main page which shows you all restaurant in the same region as the one where your post code is (in your case 1950 Sion will point to the region Sion). 
- On this first page you have on the top a search bar where you can choose an other region to display or you have the choice to check/uncheck three different restaurant types. 
- The search button will post your parameters and reload the page with the restaurant that correspond to your needs. Each restaurant has an image, his name and the ratio of his rates with the number of review/comment he has. 
- Now you can click on one restaurant and it will display all of his products. On the rigth you have your cart with all element that are inside (by default he is empty).
- Every products has an image, his name, a small description, an image if it's vegetarian, his price and a text box where you can enter the quantity. If a products his not disponible it will be impossible to click on the add buton and his image will be faded. 
- You can only add prodcuts from the same restaurant into your cart, if the cart is not empty and you are looking at product from another restaurant the add buton will be unclickable.
- On the bottom of this page you have the comment that have been left by customer about this restaurant and you have a link to go to the page where you can leave a rate (1-5) and a comment if you want.
- When you have finished to add products to your cart you can click on the confirm Order in the cart or on the cart image on the top right. You are now on the confirmed page.
- The first part of the page display all product that were in your cart with their price and quantity.
- From this page you can delete on product or remove all product of your cart. In the second part of the page you need to choose a delivery time (the page will force you to choose a time that is a quarter (15,30,45,00) and that is between) 9:00 and 22:00, the code will then check that the time has not already pass today and that it's in more than 30 minutes) you then need to choose a delivery address (the code will also check that this address is in the same region as the one where the restaurant is, if it's not the case the page will reload with an error message explaining you your mistake).
- Finally, if everything is correct the order will be added to the database and the concerning restaurant will now see it in his page).
- You also have a contact us button on the layout that let you send us a comment about the website or any other problem, you can enter your information (not mandatory to have a real email address, the one used for the account works fine) and send the form. We used a backend platform that will notify us that we received a new feedback and send it to our real address (you can try it and we will respond you ;) ) 


### Delivery staff - Credentials: EMAIL staff@valaiseat.ch PASSWORD password
- When you create an account (with the sign up option on login page) you won't be able to access to your account for the moment because it has not been validated. 
- An administrator is required to do it. When it is done, you will have access to your current orders (the ones that you will have to deliver) and also your old deliveries. 
- You can access to the details of the order to know where to take it and where to deliver it. At the top right corner you can access your account information and you can edit them.
- You can also change your working region. For example you can decide that this week you will work in the region of "Sierre" instead of "Sion".
- You can also disconnect to your account by using the "disconnect" red button.

### Administrator - Credentials: EMAIL admin@valaiseat.ch PASSWORD password
- The administrator is an employee of the valais eat company which can access his account to see all the delivery staff.
- The first list is used for registrations, all the staff that would like to join the company. You can see their information and the region where they would like to work. You can validate their account by clicking on the button. The delivery staff type will pass from "Inactive" to "Employable".
- The second list is used to see all the active staff. You can also see their information and the region where they work.
- If you decide to fire an employee you can do it by clicking on the button. The delivery staff type will pass from "Employable" to "Former".
- The third and last list is used to see all the former staff valais eat had. There are no interaction with them.

### Restaurant - Credentials: EMAIL Inglewood@gmail.com PASSWORD password 
- You will have two differents table, the first one show you all commands that need to be prepared, you can change the status on an order by pressing the button on the right of each line (READY TO BE DELIVERD). 
- The second show you the historic of all order you finished. You also have a link in the layout (Products) to display all products your restaurants has with all detials about every products. The other link in the layout shows you all the review that have been left for you, with the number of stars on the left and the comment on the right.


## IMPORTANT METHODS :

- WebApplication/Controllers/AccountController => [HttpPost] public IActionResult Login(Login myLogin)  => method that handle logins connections.

- WebApplication/Controllers/RestaurantController => [HttpPost] public IActionResult MainPageRestaurant(int IdOrder) => method that is called when a restaurant checks an order as ready to be send and a delivery need to be found, the method checks the number of orders a delivery staff has in the same 30 minutes as the new order.

- WebApplication/Controllers/OrderController => [HttpPost] public IActionResult ConfirmOrder(string DeliveryAddress, string city, int PostCode, int IdCartDetails,DateTime deliveryTime) => method that verify if the delivery address corresponds to the region of the restaurant, if the time is not already passed for today, and also if it is in more than 30 minutes.
