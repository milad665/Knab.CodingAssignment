# Answers to technical questions


# Q1

How long did you spend on the coding assignment? What would you add to your solution if you had
more time?

### Q1 Answer
Almost 6 Hours. I did it in three, 2-hour sessions because I was busy with my current job.

First and foremost, I would add a UI (front-end) to the code to complete the user experience.

The provided API is a actually developed as a backend to be used by a user facing application (i.e. Mobile App, Website), therefore it requires a means of authentication/authorization. I could develop a simple token based authentication for the service but I thought It would make the testing process more difficult for folks at Knab.
If the service was going to be used by other backend services it could also be secured by an API key.

To make it a little secure and eliminate brute force attacks, I developed a simple "ActionFilter Attribute" which blocks high frequency requests from the same IP-Address and increases the block time as the number of requests grows.

I'm using .Net Core's default memory cache to improve the overall performance of the system and help staying in the limits of the third-party API calls quota, but to enable the system to scale-out I would use a distributed cache. (e.g. Redis) if I could spend more time on the project.

To handle business logic validations and errors, I'm throwing custom exceptions and then handling the exceptions by a middleware working on top of the whole system, to standardize the way errors are communicated with the clients. For now I'm using a common "GeneralApplicationException" class, derived from "IApplicationServiceException" interface for all business logic errors, but with more time I would not only segregate business logic exceptions types per layer (e.g. using IDomainException, IPresentationException, etc.) but also would define more granular exception classes and also exception codes.

I would also add a means of persisting the data fetched from the third party APIs (e.g. Database)

Last but not least I would enable logging middleware, or used third party logging libraries to log all exceptions handled by the exception handling middleware.

# Q2
What was the most useful feature that was added to the latest version of your language of choice?

### Q2 Answer
Init-Only properties are introduced in C# 9.0 which enable developers to create immutable properties in the classes.
I have used it in the "CryptoPriceList" class where exchange rates for the crypto are calculated one in the constructor and should never change after.

        public double Usd { get; init; }
        public double Eur { get; init; }

# Q3
How would you track down a performance issue in production? Have you ever had to do this?

### Q3 Answer
I've done it many times actually.
Most of the times, performance issues are detected/reported by the client-side, either in load tests or in actual production by the users. When the issue is detected we first need to figure out what endpoint is actually causing the latency. For web applications and distributed systems, this is mostly done by client-side tools such as browser F12 tool or network monitoring tools like wireshark.

When the endpoint (or any other entry point to the slow code) is detected, the main part of the job starts.
We can use profiling tools to find the bottlenecks. 
The first step can always be a simple analysis of whether the result of the function/procedure causing the bottleneck is highly volatile (subject to high frequency changes) or not. If not, we can always utilize caching mechanisms as a first step to reduce the need of the low-performing code to run.
Next step can be checking the nature of the high-latency codes.
Bottlenecks can be CPU-bound or I/O-bound. For CPU-bound bottlenecks we need to optimize the code using better algorithms and data-structures, which can indeed lead to less-readable, but faster codes.
I/O-bound bottlenecks can vary from calls to slow third-party services to database queries. Depending on the situation we can use a combination of caching, query optimization or even change of tools and technologies to achieve better performance.

BUT ...

Big performance issues are better tackled with a big architectural decision. CAP (and PACELC) theorem is sometimes the key. It's an important fact that consistency and availability/latency are always moving in opposite directions. Increasing one, will reduce the other. Sometime we need to take bird-eye view of the whole problem and make the big decision between eventual consistency(low latency and highly available) and immediate consistency(higher latency and lower availability). 
Having the decisions made in favor of availability and latency, we can use messaging tools, and message oriented patterns such as CQRS to reduce latency and increase availability of the services.

# Q4
What was the latest technical book you have read or tech conference you have been to? What did you
learn?

### Q4 Answer
I attended ".Net Developer Days" conference in Warsaw last year (a little before COVID) and I learned a lot about microservices, docker and kubernetes and best practices of splitting monolithic software into microservices.

# Q5
What do you think about this technical assessment?

### Q5 Answer
It was good, At first it looked too easy that I started to get scared of the hidden requirements that I may have missed. But later, reading further down the assignment document, I started to realize it's not that easy and there are important stuff that need to be considered while developing the code. The code, combined with the answers to questions seems to be a good indicator of the developer abilities. 

# Q6
Please, describe yourself using JSON.

### Q6 Answer

	{
	   "name":{
		  "firstName":"Milad",
		  "lastName":"Soghrati"
	   },
	   "contact":{
		  "Mobile":"+989122438995",
		  "Home":"+9888109359",
		  "email":"milad665@gmail.com",
		  "website":"http://miladsoghrati.com",
		  "linkedin":"https://www.linkedin.com/in/milad665/",
		  "address":{
			 "country":"Iran",
			 "city":"Tehran",
			 "street":"11 St., Asadabadi St.",
			 "number":2,
			 "postalCode":"1433733491"
		  }
	   },
	   "occupation":"Software Engineer",
	   "interests":[
		  "Swimming",
		  "Traveling",
		  "History"
	   ],
	   "skills":[
		  {
			 "title":"C#",
			 "level":5
		  },
		  {
			 "title":".Net Core",
			 "level":5
		  },
		  {
			 "title":"OOP",
			 "level":5
		  },
		  {
			 "title":"SQL Server",
			 "level":5
		  },
		  {
			 "title":"Docker",
			 "level":4
		  },
		  {
			 "title":"Software Architecture",
			 "level":4
		  }
	   ]
	}
