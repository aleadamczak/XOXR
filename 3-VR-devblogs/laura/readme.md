# VR Project: Mama's Pizzeria - Laura 

### Intro
Hello and welcome to my Devblog for the VR Project! Below is a list of the features I took ownership of and that I'll be discussing in this mark-up file.
- Environment
- Customer:
    - Instantiation
    - Movement
    - Request
- Conveying Order Information to the Player:
    - Ticket Display
    - Speech Bubble Display
- Pizza:
    - Oven / Cooking the Pizza
    - Delivery
    - Score:
        - Calculation
        - Display
- Audio

Let's get right into it :).

## Environment

The first step, before even touching Unity, was something we all did together - we went to a classroom with a board with a pen and drew what we envisioned “spatially” for all the four stations of our Pizzeria: Customer, Assembly, Oven, and Cutting station. Below is an image of what that draft looked like by the end of the session.

Then, when opening Unity, the first step was to put all the stations in place around the spawn point for the player! If you could please ignore the giant pizza cutter, below is an image of what that initial stage looked like as well:

At a later stage, it came the time to actually have an environment other than the Unity infinite plane. For this, I searched for and downloaded a “beachside restaurant” model and removed some game objects to make space for our kitchen.
Original beach restaurant:

So I downloaded that, removed the little house to make space for our kitchen, moved the tables around to make space for the customers to walk, extended the floor backwards so it wouldn’t feel like a floating island and also put a dark blue plane under it.

As for the lights, the lightning settings of our Pizzeria were terrible at the beginning, with really harsh lightning that made some of the game objects nearly invisible - see a poor example below:

So I changed the environment lightning so that everything would be more uniformly lit, but not only that, I also gave it a yellow hue to give the impression that the scene is in a sunrise setting, which made everything a lot more pleasant and visible:

And that’s it for setting up the environment of the scene!


## Customer Instantiation

For us to make pizzas, we would need customers to serve them to. For that, a script called CustomerGenerator was made - it holds a list of four different Customer prefabs and puts them into the scene one by one - so that when one customer leaves after he has been served, another one comes straight after. The script also handled randomly picking a customer from the list and making sure you don’t repeat a customer before you’ve gone through all of them.
Below is a picture of all the customer prefabs.

And below is the Customer Generator component, taking both the prefabs but also a list of Transforms.

But what could those transforms be? Let’s look into the section below to find out.


## Customer Movement

Each Customer prefab actually has a Component called CustomerMovement that takes a list of positions. These positions will be used to move the customer around the scene! However, the Prefab itself couldn’t hold the actual Transforms from the scene (since it’s a Prefab), so they have to be “passed down” by the aforementioned Customer Generator in the moment he instantiates the Customers.

CustomerMovement also knows when to “block” the customer to a certain position until a certain function is called, such as when he’s ordering or when he’s waiting for his pizza.

Below is a gif of a customer walking into the restaurant, making his request, and then moving to the left when the dialogue finishes.
	⁃	customer-movement-wait

And below is a gif of a customer being delivered his pizza, waiting for the player to see his score, and leaving afterwards.
	⁃	customer-movement-deliver

I do not take credit for the customer animations - only their movement.


## Customer Request

There is a very central script called CustomerRequest that handles the flow of a customer making a request. So it’s responsible for:
- Starting the dialogue for the customer to “make his request” shortly after he has been instantiated,
- Updating the Customer Movement script to know which position to move him to,
- Awaiting his pizza delivery,
- Delegating the calculation of the score of the pizza delivered vs the pizza desired,
- And overall, just calling all the methods that need to be called to ensure that the game goes on (for example: showing the scores, leaving).

In the section above (Customer Movement), the two gifs also illustrate all of this in action.
It is quite a big script, but here’s an example of an indispensable method: DeliverPizza().

This method handles delegating the score calculation, displaying the score in front of the player, sending the player’s score to the script that will play a “reaction audio” depending on the player’s performance, and also leaving after having the pizza delivered.

So, to summarize, you could say that “Customer Request” is the component tying everything together in the flow of a customer.


## Tickets Display

One essential information that the player needs to receive is what pizza he should make. This was handled by displaying a “ticket” dynamically, depending on what pizza the customer wanted. This was developed in a separate scene, because despite looking simple, it was quite a hassle due to the sheer amount of information needing to be displayed.

Below is a screenshot of my solitary ticket in its own Scene.

I will now go into detail about each part of the ticket:

### Text

My first attempt to have text on the ticket was to use TMP_Text, but that didn’t have shadows cast on it in a 3D space and it looked really out of place in the main scene, so I actually went into Blender to make a 3D object with the text that I wanted (“Mama’s Pizzeria”, the dashed lines as well). Example below:

### Ingredients

Showing the ingredients included structuring the GameObject structure in the following way:
Each quarter has a slot for two types of ingredients - in each of these slots, the Ticket holds the transform for the ingredient prefab and a text that will say how much of that ingredient is desired. The ingredients are then, when needed, instantiated at that transform and the right text is edited.

### Time

To display the amount of time the customer wishes the pizza to be cooked, a Clock object was used - but more on that later in the Oven Section.

### Cuts

To dynamically show the cuts, I had a script that referenced “each cut” so that I could then dynamically set them to active or inactive depending on what cut the customer wanted. So if the customer wanted all cuts, then all of them would show, and if he wanted just a couple, then only those would show, therein conveying that information to the player.

In the end, this is what the ticket looks like when a dialogue is started:
	⁃	ticket-request

## Speech Bubble Display

The Speech Bubble display reused A LOT of code from the Ticket since they essentially conveyed the same information and actually had the same GameObject structure! So they actually use the same script with a toggle that determines if it’s a ticket or a Speech Bubble. The ticket keeps all previous information visible while the Speech Bubble has to hide everything before showing something new.

It should also be noted that, to have the Speech Bubble base/shape, what I did was make a thin and flattened cylinder and extrude one of its vertices to have the part of the bubble that “points out”.

It can also be mentioned that, to make the text saying “time” and “cuts”, I once again used Blender to have a 3d object that would receive shadows instead of an out-of-place TMP_Text.

Below is a gif of how the Speech Bubble looks in action:
	⁃	speech-bubble

## Oven / Cooking The Pizza

The oven functionality was actually the first one I made and it was a lot of fun. It involved:
- Making a clock object that ticks and stores the time (throwback to one of the first GMD exercises)
- Making an oven model in Blender with four holes for the pizzas, done by cutting the holes and then extruding them inwards
- Adding colliders to the bottom of each oven hole so that they would actually hold the pizza
- Making a script for each Oven Hole with a pointer to the corresponding clock, so that every time a Pizza enters an Oven Hole, the clock starts ticking, and when the pizza exits it, the Oven Hole stores in the pizza object how long it was cooked for.
- Adding a low poly fire from the asset store to the bottom of the Oven.

## Pizza Delivery
In order to “finish the basic flow of the game”, one has to deliver the pizza they prepared. And the question became - how to detect a pizza delivery? Fortunately, it was as simple as making a Trigger Collider on the pizza delivery station:

Then, the script that had access to that trigger would invoke the event “onPizzaDelivered” with the argument of the delivered pizza, to which the Customer is a listener to. This would tell him to calculate the scores based on what he got versus what he wanted.


## Scores Calculation

When the pizza is delivered, the score for it must be calculated. The Score Calculation script compares the pizza that the customer got versus the one he wanted. It checks for:
- How long the customer waited for,
- If the ingredients are in the right quantities in the right quarters,
- If the pizza was cooked for exactly how long the customer wanted, and
- If the pizza was cut how the customer wanted.

These calculations are mostly just math so I will not bore you with the details.


## Scores Display

The section above briefly described how scores are calculated - but that’s happening just behind the scenes. How can the player actually see what scores he got? Well, displaying the scores was a lot of fun. I made four thin cilinders, text over them to say what the score refers to and text in front of them them to hold the actual scores.

But how? Well, the Parent of all these GameObjects has the script that can change their text - and it is called from the Customer himself, because he knows what we scored on the pizza he wanted.

## Audio

As always, it wouldn’t be a proper game without audio, so it features background music that loops through four songs that are essentially variations of each other, and also a “reaction sound” when you deliver the pizza that is based on what total score you got on the pizza. The possible reactions are:
- Upset
- Decent
- Happy
- Overjoyed

### Conclusion 

And that’s it from my side! :) So to summarize, I would say I took ownership of:
- The Customer “flow” (instantiation, request and respective visual representation, movement, delivery, score)
- The Oven station
- The Environment and Audio
