# Microblogging-Site-Instant-Messanger-

Microblogging is a combination of blogging and instant messaging that allows users to create short messages to be posted and shared with an audience online.
Tweeting is sending short messages to anyone who follows you, with the hope that your messages are useful and interesting to someone in your audience


#Business Story
We’ve been hired to implement a microblogging site. Microblogging is a combination of blogging and instant messaging that allows users to create short messages to be posted and shared with an audience online.
Tweeting is sending short messages to anyone who follows you, with the hope that your messages are useful and interesting to someone in your audience. 

#Requirements
The application being created should fulfill the following requirements. The validations are mentioned wherever relevant. Please refer mockups in the end for better understanding.

1.	Login and register user functionalities
a.	Login should work on providing registered email and password.
b.	Register new user should take following inputs
i.	Email
ii.	Password
iii.	First and Last Name (Alphabets and spaces only)
iv.	Profile Image (JPG or PNG)
v.	Contact number (10 digits, numeric only)
vi.	Country of Residence (dropdown)
c.	All fields are mandatory.
d.	E-Mail must be unique and valid as per standard format.
e.	Password minimum length is 8 and it must have at-least 1 special character, 1 number and 1 alphabet each.

![image](https://user-images.githubusercontent.com/44699205/121890833-67e73380-cd38-11eb-80da-30c0b53c6106.png)

 

2.	Playground
a.	After login user lands on this dashboard page.
b.	It only has tweets from people he/she is following along with his/her own tweets.
c.	The tweet messages are listed in reverse chronological order. i.e. Latest message comes on top.
d.	User has choice to like or dislike a tweet.
e.	Only user who created the tweet has option to Edit/Delete a tweet.

![image](https://user-images.githubusercontent.com/44699205/121890886-759cb900-cd38-11eb-8413-77b0c643d898.png)


 
3.	Compose new tweet
a.	Clicking on this button opens a popover with textbox.
b.	Messages can be of maximum 240 characters.
c.	Messages can contain Hashtags.
Hashtag is a word or phrase preceded by a hash sign (#) and used in searching messages.
d.	On clicking Done, the message gets saved and should be displayed on user Dashboard.

![image](https://user-images.githubusercontent.com/44699205/121890918-81887b00-cd38-11eb-9eee-d3da1c0610c2.png)


 
4.	Followers
a.	This tab shows all the followers of current logged in user.
b.	Total Number of followers are shown in tab’s heading.

 ![image](https://user-images.githubusercontent.com/44699205/121890959-8e0cd380-cd38-11eb-8272-2e509eb2f271.png)


5.	Following
a.	This tab shows all the users which current logged in user is following.
b.	Total Number of users being followed are shown in tab’s heading.
c.	User has option to UnFollow.

![image](https://user-images.githubusercontent.com/44699205/121891002-98c76880-cd38-11eb-813a-addaf490b3c4.png)


 
6.	Search: User can type any text and click search button. This will populate search results in both tabs (provided results are generated).
a.	People: Search result works on email, first-name and last-name.
“Follow”/ “Unfollow” option appears next to searched users.
On clicking these buttons respective action is done.
b.	Post: Search result works only on hashtags.

![image](https://user-images.githubusercontent.com/44699205/121891035-a0870d00-cd38-11eb-93fa-66e921d4a9aa.png)


 
Question 1										
Create an end to end functional application using n-tier architecture to achieve the business story discussed above. To aid your thinking process and to set expectations in terms of output required find listed below the various components that go into making such an application

1.	SQL database									
a.	Should contain tables (if you want procedures – not mandatory) required for the business case  			
i.	Consider good design – Normalization, Constraints etc.
ii.	Map complete information

2.	Data Access Layer 								
a.	Should contain classes to help interact with the database.
b.	Use Entity Framework for database interaction.
c.	Feel free to use procedures (if you created them above) or use entities alone to complete DAC operations.

3.	Business Layer								 
a.	Should contain classes to represent the business logic to cover all the requirements mentioned in Requirements section.

NOTE: Any common code – DTOs, constants etc., which can be used by any other layer can be made part of Shared Layer (optional).

4.	Presentation Layer: Highlight use of Asp.Net MVC 6.
5.	UI presentation layer using Angular 2, HTML, CSS, JS.
a.	This should take care of UI validations.
b.	Make RESTful calls to MVC controller for data.
The following diagram shows a high-level structure and data flow for the application.

![image](https://user-images.githubusercontent.com/44699205/121891083-ae3c9280-cd38-11eb-8f0b-8f259b247d70.png)

 
#Technology Stack
Following technologies should be used to create the application:
•	UI Layer – HTML5, JS, CSS3, Angular 2
•	Presentation Layer– Asp.Net MVC 6, Razor, Asp.Net 5
•	Business and Data Layer – .NET, Entity Framework, SQL Server

#Bonus Question | Analytics
1.	Identify most trending hashtag. (Logic should include both most searched keyword and most hashed keyword)
2.	Total Tweets today by all users.
3.	Most tweets by which person till date.
4.	Most liked tweet till date.
  
![image](https://user-images.githubusercontent.com/44699205/121891128-b8f72780-cd38-11eb-958c-1ef9cda19fe3.png)


#Evaluation Criteria
The application will be evaluated against following criteria:
1.	Basic application design and data structures usage.
2.	Appropriate usage of OOP’s and SOLID principals.
3.	Functional completeness.
4.	Adhering to coding standards and guidelines.
5.	Additional creativity and innovation will help to score better.
6.	Extra marks for attempting the Bonus Question.

