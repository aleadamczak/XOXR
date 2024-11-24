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

<img width="500" alt="Screenshot 2024-11-24 at 13 25 47" src="https://github.com/user-attachments/assets/33a4d5a9-9249-4ce8-acf8-e420567fde94">

Then, when opening Unity, the first step was to put all the stations in place around the spawn point for the player! If you could please ignore the giant pizza cutter, below is an image of what that initial stage looked like as well:

![WhatsApp Image 2024-11-10 at 17 35 40](https://github.com/user-attachments/assets/ac1faac9-6ec2-4e53-bdda-466789413e82)

At a later stage, it came the time to actually have an environment other than the Unity infinite plane. For this, I searched for and downloaded a “beachside restaurant” model and removed some game objects to make space for our kitchen.

Original beach restaurant:

<img width="500" alt="Screenshot 2024-11-24 at 13 25 47" src="https://github.com/user-attachments/assets/48ff1f4a-c81b-4081-89ab-1fe436d99f7c">

So I downloaded that, removed the little house to make space for our kitchen, moved the tables around to make space for the customers to walk, extended the floor backwards so it wouldn’t feel like a floating island and also put a dark blue plane under it.

<img width="500" alt="Screenshot 2024-11-24 at 13 50 25" src="https://github.com/user-attachments/assets/9d1e75b8-b366-4b4e-b9d8-1797cb444ce1">

As for the lights, the lightning settings of our Pizzeria were terrible at the beginning, with really harsh lightning that made some of the game objects nearly invisible - see a poor example below:

<img width="372" alt="Screenshot 2024-11-24 at 13 52 03" src="https://github.com/user-attachments/assets/96269fb6-6f54-47f1-8277-3daff108cdbc">

So I changed the environment lightning so that everything would be more uniformly lit, but not only that, I also gave it a yellow hue to give the impression that the scene is in a sunrise setting, which made everything a lot more pleasant and visible:

<img width="500" alt="Screenshot 2024-11-24 at 13 54 35" src="https://github.com/user-attachments/assets/4504431d-9dbe-4b36-a319-2b48ae0adb65">

And that’s it for setting up the environment of the scene!


## Customer Instantiation

For us to make pizzas, we would need customers to serve them to. For that, a script called CustomerGenerator was made - it holds a list of four different Customer prefabs and puts them into the scene one by one - so that when one customer leaves after he has been served, another one comes straight after. The script also handled randomly picking a customer from the list and making sure you don’t repeat a customer before you’ve gone through all of them.
Below is a picture of all the customer prefabs.

<img width="300" alt="Screenshot 2024-11-24 at 14 18 32" src="https://github.com/user-attachments/assets/f9dff1a5-8fd2-492e-a10f-05381006dd57">

And below is the Customer Generator component, taking both the prefabs but also a list of Transforms.

<img width="300" alt="Customer Generator (Script)" src="https://github.com/user-attachments/assets/fba6d587-a2f6-4fee-b651-6fc34fe35818">

But what could those transforms be? Let’s look into the section below to find out.


## Customer Movement

Each Customer prefab actually has a Component called CustomerMovement that takes a list of positions. These positions will be used to move the customer around the scene! However, the Prefab itself couldn’t hold the actual Transforms from the scene (since it’s a Prefab), so they have to be “passed down” by the aforementioned Customer Generator in the moment he instantiates the Customers.

CustomerMovement also knows when to “block” the customer to a certain position until a certain function is called, such as when he’s ordering or when he’s waiting for his pizza.

Below is a gif of a customer walking into the restaurant, making his request, and then moving to the left when the dialogue finishes.

![ezgif-7-e418288e85](https://github.com/user-attachments/assets/44307ace-f0fb-4040-920f-45d109a01a5c)

And below is a gif of a customer being delivered his pizza, waiting for the player to see his score, and leaving afterwards.

![ezgif-7-d455e73d8c](https://github.com/user-attachments/assets/b02ef6ff-8654-4270-95d6-e2fc2364ae34)

I do not take credit for the customer models and animations - only their movement.


## Customer Request

There is a very central script called CustomerRequest that handles the flow of a customer making a request. So it’s responsible for:
- Starting the dialogue for the customer to “make his request” shortly after he has been instantiated,
- Updating the Customer Movement script to know which position to move him to,
- Awaiting his pizza delivery,
- Delegating the calculation of the score of the pizza delivered vs the pizza desired,
- And overall, just calling all the methods that need to be called to ensure that the game goes on (for example: showing the scores, leaving).

In the section above (Customer Movement), the two gifs also illustrate all of this in action.
It is quite a big script, but here’s an example of an indispensable method: DeliverPizza().

<img width="700" alt="Screenshot 2024-11-24 at 14 31 14" src="https://github.com/user-attachments/assets/3ff92929-fe08-476a-826e-9c30e4704966">

This method handles delegating the score calculation, displaying the score in front of the player, sending the player’s score to the script that will play a “reaction audio” depending on the player’s performance, and also leaving after having the pizza delivered.

So, to summarize, you could say that “Customer Request” is the component tying everything together in the flow of a customer.


## Tickets Display

One essential information that the player needs to receive is what pizza he should make. This was handled by displaying a “ticket” dynamically, depending on what pizza the customer wanted. This was developed in a separate scene, because despite looking simple, it was quite a hassle due to the sheer amount of information needing to be displayed.

Below is a screenshot of my solitary ticket in its own Scene.

<img width="554" alt="Screenshot 2024-11-24 at 14 38 46" src="https://github.com/user-attachments/assets/834207b5-8fa4-49d6-a410-a2f40a30a82a">

I will now go into detail about each part of the ticket:

### Text

My first attempt to have text on the ticket was to use TMP_Text, but that didn’t have shadows cast on it in a 3D space and it looked really out of place in the main scene, so I actually went into Blender to make a 3D object with the text that I wanted (“Mama’s Pizzeria”, the dashed lines as well). Example below:

![Screenshot 2024-11-24 at 14 40 57](https://github.com/user-attachments/assets/3c54c295-d85b-4701-857e-e770940d3eaa)

### Ingredients

Showing the ingredients included structuring the GameObject structure in the following way:
Each quarter has a slot for two types of ingredients - in each of these slots, the Ticket holds the transform for the ingredient prefab and a text that will say how much of that ingredient is desired. The ingredients are then, when needed, instantiated at that transform and the right text is edited.

<img width="260" alt="Screenshot 2024-11-24 at 14 44 34" src="https://github.com/user-attachments/assets/060b97d4-14b0-4ca4-83f3-7600f6924a7d"><img width="116" alt="Mama's Pizza" src="https://github.com/user-attachments/assets/4a697a0d-9860-4a9a-8a8e-bac7bc88d029">

### Time

To display the amount of time the customer wishes the pizza to be cooked, a Clock object was used - but more on that later in the Oven Section.

<img width="43" alt="Screenshot 2024-11-24 at 14 50 09" src="https://github.com/user-attachments/assets/87083f74-f1f5-47ef-8355-f6a16ea18c45">

### Cuts

To dynamically show the cuts, I had a script that referenced “each cut” so that I could then dynamically set them to active or inactive depending on what cut the customer wanted. So if the customer wanted all cuts, then all of them would show, and if he wanted just a couple, then only those would show, therein conveying that information to the player.

<img width="47" alt="Screenshot 2024-11-24 at 14 50 59" src="https://github.com/user-attachments/assets/cca6931a-c541-4fdd-b7f3-acb4649ff4b5">

### Final

In the end, this is what the ticket looks like when a dialogue is started:

![ezgif-7-66c9f30d9f](https://github.com/user-attachments/assets/8ed7b8b3-b201-4c7d-9058-72882ed23600)


## Speech Bubble Display

The Speech Bubble display reused A LOT of code from the Ticket since they essentially conveyed the same information and actually had the same GameObject structure! So they actually use the same script with a toggle that determines if it’s a ticket or a Speech Bubble. The ticket keeps all previous information visible while the Speech Bubble has to hide everything before showing something new.

It should also be noted that, to have the Speech Bubble base/shape, what I did was make a thin and flattened cylinder and extrude one of its vertices to have the part of the bubble that “points out”.

<img width="300" alt="Speech Bubble Blender" src="https://github.com/user-attachments/assets/4eb64c14-29ce-41e3-a4f8-d32df04f5bab">

It can also be mentioned that, to make the text saying “time” and “cuts”, I once again used Blender to have a 3d object that would receive shadows instead of an out-of-place TMP_Text.

Below is a gif of how the Speech Bubble looks in action:

![ezgif-7-bca25a1fc6](https://github.com/user-attachments/assets/b34abace-7747-405e-a227-22eb0bc8beca)

## Oven / Cooking The Pizza

The oven functionality was actually the first one I made and it was a lot of fun. It involved:
- Making a clock object that ticks and stores the time (throwback to one of the first GMD exercises)
<img width="500" alt="Screenshot 2024-11-24 at 15 41 04" src="https://github.com/user-attachments/assets/8cfd80a0-cc78-451e-a71b-8a7e21f3dfd0">

- Making an oven model in Blender with four holes for the pizzas, done by cutting the holes and then extruding them inwards
<img width="500" alt="Screenshot 2024-11-24 at 15 43 22" src="https://github.com/user-attachments/assets/f800f977-5351-4fbd-986c-038621bfbd5f">

- Adding colliders to the bottom of each oven hole so that they would actually hold the pizza
<img width="500" alt="Screenshot 2024-11-24 at 15 43 22" src="https://github.com/user-attachments/assets/cee43d86-18bd-4279-8fa9-f911e443031a">

- Making a script for each Oven Hole with a pointer to the corresponding clock, so that every time a Pizza enters an Oven Hole, the clock starts ticking, and when the pizza exits it, the Oven Hole stores in the pizza object how long it was cooked for.
<img width="500" alt="Screenshot 2024-11-24 at 15 44 13" src="https://github.com/user-attachments/assets/e810619c-4871-4230-8bc5-fe7ee6e881e0">

- Adding a low poly fire from the asset store to the bottom of the Oven.
![ezgif-7-68b4c6dc60](https://github.com/user-attachments/assets/ec7b1741-0ede-44da-ac5c-94af1d477965)


## Pizza Delivery
In order to “finish the basic flow of the game”, one has to deliver the pizza they prepared. And the question became - how to detect a pizza delivery? Fortunately, it was as simple as making a Trigger Collider on the pizza delivery station:

<img width="500" alt="Screenshot 2024-11-24 at 15 46 44" src="https://github.com/user-attachments/assets/4a811555-e818-4fb7-91fd-f5b7591d693b">

Then, the script that had access to that trigger would invoke the event “onPizzaDelivered” with the argument of the delivered pizza, to which the Customer is a listener to. This would tell him to calculate the scores based on what he got versus what he wanted.

<img width="500" alt="Debug  Log(message Pizza entered the delivery station );" src="https://github.com/user-attachments/assets/189a90bf-d168-4a6b-99f5-7a8d0c9b3f71">


## Scores Calculation

When the pizza is delivered, the score for it must be calculated. The Score Calculation script compares the pizza that the customer got versus the one he wanted. It checks for:
- How long the customer waited for,
- If the ingredients are in the right quantities in the right quarters,
- If the pizza was cooked for exactly how long the customer wanted, and
- If the pizza was cut how the customer wanted.

These calculations are mostly just math so I will not bore you with the details.


## Scores Display

The section above briefly described how scores are calculated - but that’s happening just behind the scenes. How can the player actually see what scores he got? Well, displaying the scores was a lot of fun. I made four thin cilinders, text over them to say what the score refers to and text in front of them them to hold the actual scores.

<img width="489" alt="Screenshot 2024-11-24 at 15 52 26" src="https://github.com/user-attachments/assets/a7ac929b-d879-4dde-8d12-0c9dda66a0a9">

But how? Well, the Parent of all these GameObjects has the script that can change their text - and it is called from the Customer himself, because he knows what we scored on the pizza he wanted.

<img width="600" alt="Screenshot 2024-11-24 at 15 53 42" src="https://github.com/user-attachments/assets/89bb1ab7-e60e-4bf0-ac4f-0fe467f6f890">

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
