# Read Me

Knab Senior Backend Developer Assignment

#### This project is developed using .Net 5.0

After running the project, call the following url in order to test the functionality:

    https://localhost:{port}/api/crypto/prices/latest?symbol={cryptocurrency-symbol e.g. BTC}

The provided API is a actually developed as a backend to be used by a user facing application (i.e. Mobile App, Website), therefore it requires a means of authentication/authorization. I could develop a simple token based authentication for the service but I thought It would make the testing process more difficult for folks at Knab. If the service was going to be used by other backend services it could also be secured by an API key.

To make it a little secure and eliminate brute force attacks, I developed a simple "ActionFilter Attribute" which blocks high frequency requests from the same IP-Address and increases the block time as the number of requests grows.

##### If you are running the code in debug mode, the custom error designed to eliminate brute-force attacks will break the code (after multiple calls to the API), you can continue running (>) in visual studio in order to see the result object on the client side.

### Architecture

I used "Onion Architecture" to develop the code, in which outer layers can depend on all inner layers but not the other way around.

#### Layers
From outside in:
Level 1: Presentation Layer, Infrastructure Layer
Level 2: Application Service Layer
Level 3: Domain Layer

